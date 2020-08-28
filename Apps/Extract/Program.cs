using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

using OXGaming.TibiaAPI;
using OXGaming.TibiaAPI.Constants;
using OXGaming.TibiaAPI.Network;
using OXGaming.TibiaAPI.Network.ClientPackets;
using OXGaming.TibiaAPI.Network.ServerPackets;
using OXGaming.TibiaAPI.Utilities;

namespace Extract
{
    class Program
    {
        private const int MapSizeW = 10;

        private static readonly HashSet<ulong> _knownPositions = new HashSet<ulong>();
        private static readonly HashSet<uint> _knownMonsterIds = new HashSet<uint>();
        private static readonly HashSet<uint> _knownNpcIds = new HashSet<uint>();
        private static readonly HashSet<uint> _knownSpawnIds = new HashSet<uint>();

        private static readonly HashSet<uint> _ignoreIds = new HashSet<uint>();

        private static readonly Dictionary<uint, uint> _replaceIds = new Dictionary<uint, uint>();
        private static readonly Dictionary<ushort, ushort> _clientToServerIds = new Dictionary<ushort, ushort>();

        private static Client _client;

        private static FileStream _otbmFile;

        private static Look _lastLookItemPacket;

        private static StreamWriter _itemFile;
        private static StreamWriter _lookItemTextFile;
        private static StreamWriter _monsterFile;
        private static StreamWriter _npcFile;

        private static XmlWriter _spawnsXml;

        private static Logger.LogLevel _logLevel = Logger.LogLevel.Error;

        private static Logger.LogOutput _logOutput = Logger.LogOutput.Console;

        private static string _currentFilename;
        private static string _otbFilename;
        private static string _outDirectory;
        private static string _recording;
        private static string _tibiaDirectory = string.Empty;

        private static ulong _timestamp = ulong.MaxValue;

        private static bool _extractItemData = false;
        private static bool _extractLookItemText = false;
        private static bool _extractMapData = false;
        private static bool _extractMonsterData = false;
        private static bool _extractNpcData = false;
        private static bool _extractSpawns = false;

        static bool ParseArgs(string[] args)
        {
            foreach (var arg in args)
            {
                var splitArg = arg.Split('=');
                if (splitArg.Length == 1)
                {
                    switch (splitArg[0])
                    {
                        case "--items":
                            {
                                _extractItemData = true;
                            }
                            break;
                        case "--lookitemtext":
                            {
                                _extractLookItemText = true;
                            }
                            break;
                        case "--map":
                            {
                                _extractMapData = true;
                            }
                            break;
                        case "--monsters":
                            {
                                _extractMonsterData = true;
                            }
                            break;
                        case "--npcs":
                            {
                                _extractNpcData = true;
                            }
                            break;
                        case "--spawns":
                            {
                                _extractSpawns = true;
                            }
                            break;
                        case "-h":
                        case "--help":
                            {
                                Console.WriteLine("[required] -r=<path>, --recording=<path>, or --recordings=<path>: " +
                                    "<path> can either be a recording file or directory of recording files.\n");
                                Console.WriteLine("[optional] -o=<path> or --outdirectory=<path>:" +
                                    "<path> is the directory you want the OTBM file to be written to. " +
                                    "If the directory does not exist, it will be created. " +
                                    "If not supplied, the OTBM file will be written to the current directory.\n");
                                Console.WriteLine("[optional] -t=<path> or --tibiadirectory=<path>: " +
                                    "<path> is the package directory of the Tibia client to target. " +
                                    "If this parameter is not specified, and an OXR file is being used, " +
                                    "the Extract app will first try to find the equivalent client version in the ClientData folder. " +
                                    "Otherwise, it will use the default path CipSoft uses upon installation.\n");
                                Console.WriteLine("[optional] --time=<seconds> or --timestamp=<seconds>: " +
                                    "<seconds> is the number of seconds from the start of the recording to stop extraction." +
                                    "If this is not specified, extraction will run until the end of the recording.");
                                Console.WriteLine("[optional] --otb=<path>: " +
                                    "<path> is the path to your items.otb file. +" +
                                    "By default, the OTBM file is created using client IDs for items. +" +
                                    "By specifying an OTB file, the OTBM file will be created with server IDs.");

                                Console.WriteLine("[optional] --loglevel=[debug,info,warning,error,disabled]: " +
                                    "Sets the log level within the API. Default: error");
                                Console.WriteLine("[optional] --logoutput=[console,file]: " +
                                    "Sets the preferred output for logging from the API. " +
                                    "file log is in the format: day_month_year__hour_minute_second.log. Default: console");

                                Console.WriteLine("The following options can be combined to extract multiple data sets at once, or individually, " +
                                    "but at least one option must be specified or the extraction process won't proceed.\n");
                                Console.WriteLine("--items: Used for extracting item information to items.txt.\n");
                                Console.WriteLine("--lookitemtext: Used for extracting text from `Look` messages for items to lookitemtext.txt.\n");
                                Console.WriteLine("--map: Used for extracting map data to the OTBM format.\n");
                                Console.WriteLine("--monsters: Used for extracting monster information to monsters.txt.\n");
                                Console.WriteLine("--npcs: Used for extracting npc information to npcs.txt.\n");
                                Console.WriteLine("--spawns: Used for extracting spawns to spawns.xml.\n");
                            }
                            return false;
                        default:
                            break;
                    }
                }
                else if (splitArg.Length == 2)
                {
                    switch (splitArg[0])
                    {
                        case "-r":
                        case "--recording":
                        case "--recordings":
                            {
                                _recording = splitArg[1].Replace("\"", "");
                            }
                            break;
                        case "-o":
                        case "--outdirectory":
                            {
                                _outDirectory = splitArg[1].Replace("\"", "");
                            }
                            break;
                        case "-t":
                        case "--tibiadirectory":
                            {
                                _tibiaDirectory = splitArg[1].Replace("\"", "");
                            }
                            break;
                        case "--time":
                        case "--timestamp":
                            {
                                if (!ulong.TryParse(splitArg[1], out _timestamp))
                                {
                                    Console.WriteLine($"{splitArg[1]} is not a valid timestamp!");
                                    return false;
                                }
                            }
                            break;
                        case "--otb":
                            {
                                _otbFilename = splitArg[1].Replace("\"", "");
                            }
                            break;
                        case "--loglevel":
                            {
                                _logLevel = Logger.ConvertToLogLevel(splitArg[1]);
                            }
                            break;
                        case "--logoutput":
                            {
                                _logOutput = Logger.ConvertToLogOutput(splitArg[1]);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            return true;
        }

        static void Main(string[] args)
        {
            try
            {
                if (args.Length <= 0)
                {
                    Console.WriteLine("Invalid argument.");
                    return;
                }

                if (!ParseArgs(args))
                {
                    return;
                }

                if (string.IsNullOrEmpty(_recording))
                {
                    Console.WriteLine("A recording, or directory of recordings, was not specified.");
                    Console.WriteLine("Use -h, or --help, for help.");
                    return;
                }

                if (!_extractItemData && !_extractLookItemText && !_extractMapData &&
                    !_extractMonsterData && !_extractNpcData && !_extractSpawns)
                {
                    Console.WriteLine("You must specificy at least one extraction option.");
                    Console.WriteLine("Use -h, or --help, for help.");
                    return;
                }

                var isDirectory = !_recording.EndsWith(".oxr", StringComparison.CurrentCultureIgnoreCase);
                if (isDirectory && !Directory.Exists(_recording))
                {
                    Console.WriteLine($"Directory does not exist: {_recording}");
                    return;
                }

                if (!isDirectory && !File.Exists(_recording))
                {
                    Console.WriteLine($"File does not exist: {_recording}");
                    return;
                }

                var filenames = new List<string>();
                if (isDirectory)
                {
                    filenames.AddRange(Directory.GetFiles(_recording, "*.oxr", SearchOption.AllDirectories));
                }
                else
                {
                    filenames.Add(_recording);
                }

                if (!string.IsNullOrEmpty(_outDirectory))
                {
                    if (!Directory.Exists(_outDirectory))
                    {
                        Directory.CreateDirectory(_outDirectory);
                    }
                }

                if (!string.IsNullOrEmpty(_otbFilename))
                {
                    LoadOtb();
                }

                if (_extractMapData)
                {
                    try
                    {
                        LoadXML("ItemsIgnore.xml");
                        LoadXML("ItemsReplace.xml");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }

                if (_extractItemData)
                {
                    Console.WriteLine("Extracting item data...");
                    _itemFile = new StreamWriter("items.txt", true);
                }

                if (_extractLookItemText)
                {
                    Console.WriteLine("Extracting `Look` message text for items...");
                    _lookItemTextFile = new StreamWriter("lookitemtext.txt", true);
                }

                if (_extractMonsterData)
                {
                    Console.WriteLine("Extracting monster data...");
                    _monsterFile = new StreamWriter("monsters.txt", true);
                }

                if (_extractNpcData)
                {
                    Console.WriteLine("Extracting npc data...");
                    _npcFile = new StreamWriter("npcs.txt", true);
                }

                if (_extractSpawns)
                {
                    Console.WriteLine("Extracting spawns...");
                    var settings = new XmlWriterSettings
                    {
                        Indent = true
                    };
                    _spawnsXml = XmlWriter.Create("spawns.xml", settings);
                    _spawnsXml.WriteStartDocument();
                    _spawnsXml.WriteStartElement("spawns");
                }

                ExtractRecordings(filenames);

                if (_itemFile != null)
                {
                    _itemFile.Close();
                }

                if (_lookItemTextFile != null)
                {
                    _lookItemTextFile.Close();
                }

                if (_monsterFile != null)
                {
                    _monsterFile.Close();
                }

                if (_npcFile != null)
                {
                    _npcFile.Close();
                }

                if (_spawnsXml != null)
                {
                    _spawnsXml.WriteEndDocument();
                    _spawnsXml.Close();
                }

                Console.WriteLine("Extraction complete");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static void LoadOtb()
        {
            Console.WriteLine("Loading OTB file...");
            using var fileStream = new BinaryReader(File.OpenRead(_otbFilename));
            while (fileStream.ReadByte() != 0xFE) { } // skip to root node
            while (fileStream.ReadByte() != 0x01) { } // skip to version attribute
            var skipBytes = fileStream.ReadUInt16();
            fileStream.BaseStream.Seek(skipBytes, SeekOrigin.Current); // skip version info
            while (fileStream.BaseStream.Position < fileStream.BaseStream.Length)
            {
                var serverId = ushort.MinValue;
                var clientId = ushort.MinValue;

                // We've reached the end of the file.
                if (fileStream.ReadByte() == 0xFF)
                {
                    break;
                }

                // The OTB format is really, really stupid (no offense).
                using var ms = new MemoryStream();
                while (true)
                {
                    var value = fileStream.ReadByte();
                    if (value == 0xFE || value == 0xFF)
                    {
                        break;
                    }
                    else if (value == 0xFD)
                    {
                        value = fileStream.ReadByte();
                    }
                    ms.WriteByte(value);
                }

                ms.Position = 0;
                using var bs = new BinaryReader(ms);

                bs.ReadByte(); // item group
                bs.ReadUInt32(); // item flags

                while (bs.BaseStream.Position < bs.BaseStream.Length)
                {
                    var attribute = bs.ReadByte();
                    var dataLen = bs.ReadUInt16();
                    if (attribute == 0x10)
                    {
                        serverId = bs.ReadUInt16();
                    }
                    else if (attribute == 0x11)
                    {
                        clientId = bs.ReadUInt16();
                    }
                    else
                    {
                        bs.BaseStream.Seek(dataLen, SeekOrigin.Current);
                    }

                    if (serverId != 0 && clientId != 0)
                    {
                        if (_clientToServerIds.ContainsKey(clientId))
                        {
                            var value = _clientToServerIds[clientId];
                            Console.WriteLine($"Failed to map Server ID `{serverId}` to Client ID `{clientId}`. " +
                                $"Client ID `{clientId}` is already mapped to Server ID `{value}`.");
                        }
                        else
                        {
                            _clientToServerIds[clientId] = serverId;
                        }
                        serverId = 0;
                        clientId = 0;
                    }
                }
            }
            Console.WriteLine("Done");
        }

        static void LoadXML(string filename)
        {
            using (var file = File.OpenRead(filename))
            {
                Console.WriteLine($"Loading {filename}...");
                using (var reader = XmlReader.Create(file))
                {
                    while (reader.Read())
                    {
                        if (!reader.IsStartElement())
                        {
                            continue;
                        }

                        if (!reader.Name.Equals("item", StringComparison.CurrentCultureIgnoreCase))
                        {
                            continue;
                        }

                        if (uint.TryParse(reader["id"], out var id))
                        {
                            _ignoreIds.Add(id);
                        }
                        else if (uint.TryParse(reader["fromid"], out var fromId) &&
                            uint.TryParse(reader["toid"], out var toId))
                        {
                            _replaceIds.Add(fromId, toId);
                        }
                    }
                }
                Console.WriteLine("Done");
            }
        }

        private static void AddSpawnEntry(OXGaming.TibiaAPI.Creatures.Creature creature)
        {
            if ((creature.Type != OXGaming.TibiaAPI.Constants.CreatureType.Monster &&
                creature.Type != OXGaming.TibiaAPI.Constants.CreatureType.Npc) ||
                _knownSpawnIds.Contains(creature.Id))
            {
                return;
            }

            _knownSpawnIds.Add(creature.Id);

            _spawnsXml.WriteStartElement("spawn");
            _spawnsXml.WriteAttributeString("centerx", creature.Position.X.ToString());
            _spawnsXml.WriteAttributeString("centery", creature.Position.Y.ToString());
            _spawnsXml.WriteAttributeString("centerz", creature.Position.Z.ToString());
            _spawnsXml.WriteAttributeString("radius", "5");
            _spawnsXml.WriteStartElement(creature.Type == OXGaming.TibiaAPI.Constants.CreatureType.Monster ? "monster" : "npc");
            _spawnsXml.WriteAttributeString("name", creature.Name);
            _spawnsXml.WriteAttributeString("x", "0");
            _spawnsXml.WriteAttributeString("y", "0");
            _spawnsXml.WriteAttributeString("z", "0");
            _spawnsXml.WriteAttributeString("spawntime", "60");
            _spawnsXml.WriteEndElement();
            _spawnsXml.WriteEndElement();
        }

        private static void ExtractRecordings(List<string> filenames)
        {
            Console.WriteLine($"Extracting data from {filenames.Count} recording(s)...");
            foreach (var filename in filenames)
            {
                Console.WriteLine($"Extracting data from {filename}...");
                using (var reader = new BinaryReader(File.OpenRead(filename)))
                {
                    var isOxRecording = filename.EndsWith(".oxr", StringComparison.CurrentCultureIgnoreCase);
                    if (!isOxRecording)
                    {
                        Console.WriteLine($"Skipping invalid recording file: {filename}");
                        continue;
                    }

                    _currentFilename = filename;

                    // OXR files begin with the client version they were recorded with.
                    // This allows us to easily parse recordings from older client versions.
                    var version = reader.ReadString();
                    Console.WriteLine($"Client version: {version}");
                    if (int.TryParse(version.Replace(".", ""), out var versionNumber))
                    {
                        if (!string.IsNullOrEmpty(_tibiaDirectory))
                        {
                            if (!Directory.Exists(_tibiaDirectory))
                            {
                                Console.WriteLine($"[Error] The provided directory does not exist: ${_tibiaDirectory}");
                                return;
                            }
                        }
                        else
                        {
                            var clientDataDirectory = $"ClientData/{versionNumber}";
                            if (!Directory.Exists(clientDataDirectory))
                            {
                                Console.WriteLine($"ClientData directory for version {version} doesn't exist. Falling back to default Tibia directory.");
                            }
                            else
                            {
                                _tibiaDirectory = clientDataDirectory;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Invalid client version at beginning of recording: {version}");
                    }

                    _otbmFile = null;
                    _client = new Client(_tibiaDirectory);
                    _client.Logger.Level = _logLevel;
                    _client.Logger.Output = _logOutput;

                    _client.Connection.OnReceivedClientLookPacket += Connection_OnReceivedClientLookPacket;

                    _client.Connection.OnReceivedServerCreatureDataPacket += Connection_OnReceivedServerCreatureDataPacket;
                    _client.Connection.OnReceivedServerChangeOnMapPacket += Connection_OnReceivedServerChangeOnMapPacket;
                    _client.Connection.OnReceivedServerCreateOnMapPacket += Connection_OnReceivedServerCreateOnMapPacket;
                    _client.Connection.OnReceivedServerBottomFloorPacket += Connection_OnReceivedMapPacket;
                    _client.Connection.OnReceivedServerBottomRowPacket += Connection_OnReceivedMapPacket;
                    _client.Connection.OnReceivedServerTopFloorPacket += Connection_OnReceivedMapPacket;
                    _client.Connection.OnReceivedServerTopRowPacket += Connection_OnReceivedMapPacket;
                    _client.Connection.OnReceivedServerLeftColumnPacket += Connection_OnReceivedMapPacket;
                    _client.Connection.OnReceivedServerRightColumnPacket += Connection_OnReceivedMapPacket;
                    _client.Connection.OnReceivedServerFieldDataPacket += Connection_OnReceivedMapPacket;
                    _client.Connection.OnReceivedServerFullMapPacket += Connection_OnReceivedServerFullMapPacket;
                    _client.Connection.OnReceivedServerMessagePacket += Connection_OnReceivedServerMessagePacket;

                    var packetCount = 0;
                    var startTimestamp = ulong.MinValue;
                    while (reader.BaseStream.Position < reader.BaseStream.Length)
                    {
                        var packetType = PacketType.Server;
                        if (isOxRecording)
                        {
                            packetType = (PacketType)reader.ReadByte();
                            var timestamp = reader.ReadInt64();
                            // Converted recordings lack a timestamp, so we need to make an artificial one.
                            if (timestamp == 0)
                            {
                                packetCount++;
                                timestamp = packetCount * 100;
                            }
                            else if (startTimestamp == ulong.MinValue)
                            {
                                startTimestamp = (ulong)timestamp;
                            }
                            var elapsed = ((ulong)timestamp - startTimestamp) / 1000;
                            if (elapsed >= _timestamp)
                            {
                                break;
                            }
                        }

                        var size = reader.ReadUInt32();
                        if ((reader.BaseStream.Length - reader.BaseStream.Position) < size)
                        {
                            break;
                        }

                        var wholeSize = reader.ReadUInt16();
                        var sequenceNumber = reader.ReadUInt32();
                        var packetSize = reader.ReadUInt16();
                        var outMessage = new NetworkMessage(_client);
                        var message = new NetworkMessage(_client)
                        {
                            Size = size
                        };

                        // Tibia10 recordings seem to contain login data (worlds, characters, etc.)
                        // in their first packet. We don't parse this, and we don't need to, so skip it.
                        if (_client.VersionNumber <= 11405409 && sequenceNumber == 0 && reader.PeekChar() == 0x28)
                        {
                            reader.BaseStream.Position += size - 8;
                            continue;
                        }

                        reader.BaseStream.Position -= 8;

                        Array.Copy(reader.ReadBytes((int)message.Size), message.GetBuffer(), message.Size);

                        if (packetType == PacketType.Server)
                        {
                            _client.Connection.ParseServerMessage(_client, message, outMessage);
                        }
                        else
                        {
                            _client.Connection.ParseClientMessage(_client, message, outMessage);
                        }
                    }

                    _client.Dispose();
                    _knownMonsterIds.Clear();

                    if (_otbmFile != null)
                    {
                        // node towns
                        _otbmFile.WriteByte(254);
                        _otbmFile.WriteByte(6);

                        // end towns node
                        _otbmFile.WriteByte(255);

                        // end map data node
                        _otbmFile.WriteByte(255);

                        // end root node
                        _otbmFile.WriteByte(255);

                        _otbmFile.Close();
                    }

                    _knownPositions.Clear();
                }

                Console.WriteLine("Done");
            }
        }

        private static bool Connection_OnReceivedServerMessagePacket(Packet packet)
        {
            var p = (Message)packet;
            if (_extractLookItemText && p.MessageMode == MessageModeType.Look && _lastLookItemPacket != null)
            {
                // To perserve the format of the file, replace newlines with literals.
                // For example, instead of a multiline text looking like this:
                /* Test Line One
                   Test Line Two*/
                // It would look like the following in the file instead:
                // Test Line One\nText Line Two
                var text = p.Text.Replace("\n", "\\n");
                _lookItemTextFile.WriteLine($"{_lastLookItemPacket.ObjectId}::{_lastLookItemPacket.Position}::{text}");

                // Clear the last `Look` packet so that a `Look` message from something
                // other than an item doesn't get used.
                _lastLookItemPacket = null;
            }
            return true;
        }

        private static bool Connection_OnReceivedClientLookPacket(Packet packet)
        {
            if (_extractLookItemText)
            {
                var p = (Look)packet;
                // This packet should only be used for items (`LookAtCreature` should be used for creatures),
                // but check the ID to ensure this is an item being looked at.
                if (p.ObjectId >= 100)
                {
                    _lastLookItemPacket = p;
                }
            }
            return true;
        }

        private static bool Connection_OnReceivedServerFullMapPacket(Packet packet)
        {
            var p = (FullMap)packet;

            // Because the API supports relogging and switching characters,
            // or the player could have teleported during their session, there's
            // a chance the recording could contain multiple FullMap packets.
            // In this case, use the same .otbm file for outputting.
            if (_extractMapData && _otbmFile == null)
            {
                Console.WriteLine($"Extracting map data... ");
                var pos = p.Position;
                var currentDate = DateTime.UtcNow;
                var fileNameData = new object[]
                {
                    Path.GetFileNameWithoutExtension(_currentFilename), pos.X, pos.Y, pos.Z, currentDate.Day, currentDate.Month, currentDate.Year, currentDate.Hour, currentDate.Minute, currentDate.Second
                };

                var otbmName = string.Format("{0}__{1}_{2}_{3}__{4}_{5}_{6}__{7}_{8}_{9}", fileNameData);
                var outputPath = $"{otbmName}.otbm";
                if (!string.IsNullOrEmpty(_outDirectory))
                {
                    outputPath = Path.Combine(_outDirectory, outputPath);
                }

                _otbmFile = InitializeMapFile(otbmName, outputPath);
            }

            foreach (var (_, _, position) in p.Fields)
            {
                if (_extractMapData)
                {
                    ParseField(position);
                }

                var mapPosition = _client.WorldMapStorage.ToMap(position);
                var field = _client.WorldMapStorage.GetField(mapPosition.X, mapPosition.Y, mapPosition.Z);
                if (field != null)
                {
                    for (var i = 0; i < MapSizeW; ++i)
                    {
                        var obj = field.GetObject(i);
                        if (obj == null)
                        {
                            continue;
                        }

                        if (_extractItemData && obj.Id > (int)CreatureInstanceType.Creature)
                        {
                            _itemFile.WriteLine($"{obj.Id} {position.ToString()}");
                        }
                        else if (obj.Id <= (int)CreatureInstanceType.Creature)
                        {
                            var creature = _client.CreatureStorage.GetCreature(obj.Data);
                            if (creature == null)
                            {
                                continue;
                            }

                            if (_extractSpawns)
                            {
                                AddSpawnEntry(creature);
                            }

                            if (_extractMonsterData && creature.Type == OXGaming.TibiaAPI.Constants.CreatureType.Monster && !_knownMonsterIds.Contains(creature.Id))
                            {
                                _monsterFile.WriteLine($"{creature.Name} {creature.Position}");
                                _knownMonsterIds.Add(creature.Id);
                            }
                            else if (_extractNpcData && creature.Type == OXGaming.TibiaAPI.Constants.CreatureType.Npc && !_knownNpcIds.Contains(creature.Id))
                            {
                                _npcFile.WriteLine($"{creature.Name} {creature.Position}");
                                _knownNpcIds.Add(creature.Id);
                            }
                        }
                    }
                }
            }
            return true;
        }

        private static bool Connection_OnReceivedServerCreatureDataPacket(Packet packet)
        {
            var p = (CreatureData)packet;
            if (p.Creature != null)
            {
                if (_extractSpawns)
                {
                    AddSpawnEntry(p.Creature);
                }

                if (_extractMonsterData && p.Creature.Type == OXGaming.TibiaAPI.Constants.CreatureType.Monster && !_knownMonsterIds.Contains(p.Creature.Id))
                {
                    _monsterFile.WriteLine($"{p.Creature.Name} {p.Creature.Position}");
                    _knownMonsterIds.Add(p.Creature.Id);
                }
                else if (_extractNpcData && p.Creature.Type == OXGaming.TibiaAPI.Constants.CreatureType.Npc && !_knownNpcIds.Contains(p.Creature.Id))
                {
                    _npcFile.WriteLine($"{p.Creature.Name} {p.Creature.Position}");
                    _knownNpcIds.Add(p.Creature.Id);
                }
            }
            return true;
        }

        private static bool Connection_OnReceivedServerChangeOnMapPacket(Packet packet)
        {
            var p = (ChangeOnMap)packet;
            if (_extractItemData && p.ObjectInstance != null && p.Id > (int)CreatureInstanceType.Creature)
            {
                _itemFile.WriteLine($"{p.ObjectInstance.Id} {p.Position.ToString()}");
            }
            else if (p.Creature != null)
            {
                if (_extractSpawns)
                {
                    AddSpawnEntry(p.Creature);
                }

                if (_extractMonsterData && p.Creature.Type == OXGaming.TibiaAPI.Constants.CreatureType.Monster && !_knownMonsterIds.Contains(p.Creature.Id))
                {
                    _monsterFile.WriteLine($"{p.Creature.Name} {p.Creature.Position}");
                    _knownMonsterIds.Add(p.Creature.Id);
                }
                else if (_extractNpcData && p.Creature.Type == OXGaming.TibiaAPI.Constants.CreatureType.Npc && !_knownNpcIds.Contains(p.Creature.Id))
                {
                    _npcFile.WriteLine($"{p.Creature.Name} {p.Creature.Position}");
                    _knownNpcIds.Add(p.Creature.Id);
                }
            }
            return true;
        }

        private static bool Connection_OnReceivedServerCreateOnMapPacket(Packet packet)
        {
            var p = (CreateOnMap)packet;
            if (_extractItemData && p.ObjectInstance != null && p.Id > (int)CreatureInstanceType.Creature)
            {
                _itemFile.WriteLine($"{p.ObjectInstance.Id} {p.Position.ToString()}");
            }
            else if (p.Creature != null)
            {
                if (_extractSpawns)
                {
                    AddSpawnEntry(p.Creature);
                }

                if (_extractMonsterData && p.Creature.Type == OXGaming.TibiaAPI.Constants.CreatureType.Monster && !_knownMonsterIds.Contains(p.Creature.Id))
                {
                    _monsterFile.WriteLine($"{p.Creature.Name} {p.Creature.Position}");
                    _knownMonsterIds.Add(p.Creature.Id);
                }
                else if (_extractNpcData && p.Creature.Type == OXGaming.TibiaAPI.Constants.CreatureType.Npc && !_knownNpcIds.Contains(p.Creature.Id))
                {
                    _npcFile.WriteLine($"{p.Creature.Name} {p.Creature.Position}");
                    _knownNpcIds.Add(p.Creature.Id);
                }
            }
            return true;
        }

        static bool Connection_OnReceivedMapPacket(Packet packet)
        {
            var p = (Map)packet;
            foreach (var (_, _, position) in p.Fields)
            {
                if (_extractMapData)
                {
                    ParseField(position);
                }

                var field = _client.WorldMapStorage.GetField(position.X, position.Y, position.Z);
                if (field != null)
                {
                    for (var i = 0; i < MapSizeW; ++i)
                    {
                        var obj = field.GetObject(i);
                        if (obj == null)
                        {
                            continue;
                        }

                        if (_extractItemData && obj.Id > (int)CreatureInstanceType.Creature)
                        {
                            _itemFile.WriteLine($"{obj.Id} {position.ToString()}");
                        }
                        else if (obj.Id <= (int)CreatureInstanceType.Creature)
                        {
                            var creature = _client.CreatureStorage.GetCreature(obj.Data);
                            if (creature == null)
                            {
                                continue;
                            }

                            if (_extractSpawns)
                            {
                                AddSpawnEntry(creature);
                            }

                            if (_extractMonsterData && creature.Type == OXGaming.TibiaAPI.Constants.CreatureType.Monster && !_knownMonsterIds.Contains(creature.Id))
                            {
                                _monsterFile.WriteLine($"{creature.Name} {creature.Position}");
                                _knownMonsterIds.Add(creature.Id);
                            }
                            else if (_extractNpcData && creature.Type == OXGaming.TibiaAPI.Constants.CreatureType.Npc && !_knownNpcIds.Contains(creature.Id))
                            {
                                _npcFile.WriteLine($"{creature.Name} {creature.Position}");
                                _knownNpcIds.Add(creature.Id);
                            }
                        }
                    }
                }
            }
            return true;
        }

        static FileStream InitializeMapFile(string filename, string outputPath)
        {
            var file = new FileStream(outputPath, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.ReadWrite, 4096, true);

            // header
            WriteData(file, BitConverter.GetBytes((uint)0));

            // node root
            file.WriteByte(254);
            file.WriteByte(0);
            {
                // otbm version
                WriteData(file, BitConverter.GetBytes((uint)2));

                // map width
                WriteData(file, BitConverter.GetBytes((ushort)65000));
                // map height
                WriteData(file, BitConverter.GetBytes((ushort)65000));

                // otb major version
                WriteData(file, BitConverter.GetBytes((uint)3));
                // otb minor version
                WriteData(file, BitConverter.GetBytes((uint)56));

                // node map data
                file.WriteByte(254);
                file.WriteByte(2);
                {
                    // description
                    var description = "Created via jo3bingham's TibiaAPI";
                    file.WriteByte(1);
                    WriteData(file, BitConverter.GetBytes((ushort)description.Length));
                    WriteData(file, Encoding.ASCII.GetBytes(description));

                    // spawn file
                    var spawnFile = filename + "-spawn.xml";
                    file.WriteByte(11);
                    WriteData(file, BitConverter.GetBytes((ushort)spawnFile.Length));
                    WriteData(file, Encoding.ASCII.GetBytes(spawnFile));

                    // house file
                    var houseFile = filename + "-house.xml";
                    file.WriteByte(13);
                    WriteData(file, BitConverter.GetBytes((ushort)houseFile.Length));
                    WriteData(file, Encoding.ASCII.GetBytes(houseFile));
                }
            }

            return file;
        }

        static void WriteData(FileStream file, byte[] data)
        {
            for (int i = 0; i < data.Length; ++i)
            {
                if (data[i] == 253 || data[i] == 254 || data[i] == 255)
                {
                    file.WriteByte(253);
                }

                file.WriteByte(data[i]);
            }
        }

        static void ParseField(Position position)
        {
            var index = (ulong)((position.Z * 40959 * 40959) + (position.Y * 40959) + position.X);
            if (_knownPositions.Contains(index))
            {
                return;
            }

            _knownPositions.Add(index);

            // node tile area
            _otbmFile.WriteByte(254);
            _otbmFile.WriteByte(4);

            // position
            WriteData(_otbmFile, BitConverter.GetBytes((ushort)(position.X & 0xFF00)));
            WriteData(_otbmFile, BitConverter.GetBytes((ushort)(position.Y & 0xFF00)));
            WriteData(_otbmFile, new byte[] { (byte)position.Z });

            // node tile
            _otbmFile.WriteByte(254);
            _otbmFile.WriteByte(5);

            // x/y
            WriteData(_otbmFile, new byte[] { (byte)(position.X & 0xFF) });
            WriteData(_otbmFile, new byte[] { (byte)(position.Y & 0xFF) });

            var mapPosition = _client.WorldMapStorage.ToMap(position);
            var field = _client.WorldMapStorage.GetField(mapPosition.X, mapPosition.Y, mapPosition.Z);
            if (field != null)
            {
                for (int i = 0; i < MapSizeW; ++i)
                {
                    var item = field.GetObject(i);
                    if (item == null || item.Id < 100 || _ignoreIds.Contains(item.Id))
                    {
                        continue;
                    }

                    // node item
                    _otbmFile.WriteByte(254);
                    _otbmFile.WriteByte(6);

                    // item id
                    if (!_replaceIds.TryGetValue(item.Id, out uint id))
                    {
                        id = item.Id;
                    }
                    if (_clientToServerIds.TryGetValue((ushort)id, out var serverId))
                    {
                        id = serverId;
                    }
                    WriteData(_otbmFile, BitConverter.GetBytes((ushort)id));

                    // item data
                    if (item.Type != null && item.Type.Flags.Cumulative)
                    {
                        _otbmFile.WriteByte(15);
                        WriteData(_otbmFile, new byte[] { (byte)item.Data });
                    }

                    // item sub type
                    if (item.Type != null && (item.Type.Flags.Liquidcontainer || item.Type.Flags.Liquidpool))
                    {
                        _otbmFile.WriteByte(15);
                        byte subType = 0;
                        if (Enum.IsDefined(typeof(FluidType), (int)item.Data))
                        {
                            subType = (byte)item.Data;
                        }
                        WriteData(_otbmFile, new byte[] { subType });
                    }

                    //end item node
                    _otbmFile.WriteByte(255);
                }
            }

            // end tile node
            _otbmFile.WriteByte(255);

            // end tile area node
            _otbmFile.WriteByte(255);
        }
    }
}
