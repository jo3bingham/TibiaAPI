using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

using OXGaming.TibiaAPI;
using OXGaming.TibiaAPI.Constants;
using OXGaming.TibiaAPI.Network;
using OXGaming.TibiaAPI.Network.ServerPackets;
using OXGaming.TibiaAPI.Utilities;
using OXGaming.TibiaAPI.WorldMap;

namespace Extract
{
    class Program
    {
        private const int MapSizeW = 10;

        private static readonly HashSet<ulong> _knownPositions = new HashSet<ulong>();
        private static readonly HashSet<uint> _knownMonsterIds = new HashSet<uint>();
        private static readonly HashSet<uint> _knownNpcIds = new HashSet<uint>();

        private static readonly HashSet<uint> _ignoreIds = new HashSet<uint>();

        private static readonly Dictionary<uint, uint> _replaceIds = new Dictionary<uint, uint>();

        private static Client _client;

        private static FileStream _otbmFile;

        private static StreamWriter _itemFile;
        private static StreamWriter _monsterFile;
        private static StreamWriter _npcFile;

        private static string _outDirectory;
        private static string _recording;
        private static string _tibiaDirectory = string.Empty;

        private static bool _convertToNewFormat = false;
        private static bool _extractItemData = false;
        private static bool _extractMapData = false;
        private static bool _extractMonsterData = false;
        private static bool _extractNpcData = false;

        static bool ParseArgs(string[] args)
        {
            foreach (var arg in args)
            {
                var splitArg = arg.Split('=');
                if (splitArg.Length == 1)
                {
                    switch (splitArg[0])
                    {
                        case "--convert":
                            {
                                _convertToNewFormat = true;
                            }
                            break;
                        case "--items":
                            {
                                _extractItemData = true;
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

                                Console.WriteLine("The following options can be combined to extract multiple data sets at once, or individually, " +
                                    "but at least one option must be specified or the extraction process won't proceed.\n");
                                Console.WriteLine("--convert: Used for converting an old recording (.dat) to the new format (.oxr).\n");
                                Console.WriteLine("--items: Used for extracting item information to items.txt.\n");
                                Console.WriteLine("--map: Used for extracting map data to the OTBM format.\n");
                                Console.WriteLine("--monsters: Used for extracting monster information to monsters.txt.\n");
                                Console.WriteLine("--npcs: Used for extracting npc information to npcs.txt.\n");
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

                if (!_convertToNewFormat && !_extractItemData && !_extractMapData && !_extractMonsterData && !_extractNpcData)
                {
                    Console.WriteLine("You must specificy at least one extraction option.");
                    Console.WriteLine("Use -h, or --help, for help.");
                    return;
                }

                var isDirectory = !(_recording.EndsWith(".dat", StringComparison.CurrentCultureIgnoreCase) ||
                                    _recording.EndsWith(".oxr", StringComparison.CurrentCultureIgnoreCase));
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

                if (_extractMapData)
                {
                    LoadXML("ItemsIgnore.xml");
                    LoadXML("ItemsReplace.xml");
                }

                if (_extractItemData)
                {
                    Console.WriteLine("Extracting item data...");
                    _itemFile = new StreamWriter("items.txt", true);
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

                Console.WriteLine($"Extracting data from {filenames.Count} recordings...");
                foreach (var filename in filenames)
                {
                    Console.WriteLine($"Extracting data from {filename}...");
                    using (var reader = new BinaryReader(File.OpenRead(filename)))
                    {
                        var isOxRecording = filename.EndsWith(".oxr", StringComparison.CurrentCultureIgnoreCase);
                        if (isOxRecording)
                        {
                            // OXR files begin with the client version they were recorded with.
                            // This allows us to easily parse recordings from older client versions.
                            var version = reader.ReadString();
                            Console.WriteLine($"Client version: {version}");
                            if (int.TryParse(version.Replace(".", ""), out var versionNumber))
                            {
                                var clientDataDirectory = $"ClientData/{versionNumber}";
                                if (!Directory.Exists(clientDataDirectory))
                                {
                                    Console.WriteLine($"ClientData directory for version {version} doesn't exist. Falling back to Tibia directory.");
                                }
                                else
                                {
                                    _tibiaDirectory = clientDataDirectory;
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Invalid client version at beginning of recording: {version}");
                            }
                        }

                        _otbmFile = null;
                        _client = new Client(_tibiaDirectory);
                        var oxrFile = (isOxRecording || !_convertToNewFormat) ? null :
                            new BinaryWriter(File.Open(Path.Combine(Path.GetDirectoryName(filename), Path.GetFileNameWithoutExtension(filename) + ".oxr"), FileMode.Create));
                        if (oxrFile != null)
                        {
                            Console.WriteLine("Converting to .oxr format...");
                            oxrFile.Write(_client.Version);
                        }

                        _client.Connection.OnReceivedServerChangeOnMapPacket += (packet) =>
                        {
                            var p = (ChangeOnMap)packet;
                            if (_extractItemData && p.ObjectInstance != null)
                            {
                                _itemFile.WriteLine($"{p.ObjectInstance.Id} {p.Position.ToString()}");
                            }
                            else if (p.Creature != null)
                            {
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
                        };

                        _client.Connection.OnReceivedServerCreateOnMapPacket += (packet) =>
                        {
                            var p = (CreateOnMap)packet;
                            if (_extractItemData && p.ObjectInstance != null)
                            {
                                _itemFile.WriteLine($"{p.ObjectInstance.Id} {p.Position.ToString()}");
                            }
                            else if (p.Creature != null)
                            {
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
                        };

                        _client.Connection.OnReceivedServerBottomFloorPacket += Proxy_OnReceivedMapPacket;
                        _client.Connection.OnReceivedServerBottomRowPacket += Proxy_OnReceivedMapPacket;
                        _client.Connection.OnReceivedServerTopFloorPacket += Proxy_OnReceivedMapPacket;
                        _client.Connection.OnReceivedServerTopRowPacket += Proxy_OnReceivedMapPacket;
                        _client.Connection.OnReceivedServerLeftColumnPacket += Proxy_OnReceivedMapPacket;
                        _client.Connection.OnReceivedServerRightColumnPacket += Proxy_OnReceivedMapPacket;
                        _client.Connection.OnReceivedServerFieldDataPacket += Proxy_OnReceivedMapPacket;

                        _client.Connection.OnReceivedServerFullMapPacket += (packet) =>
                        {
                            var p = (FullMap)packet;

                            if (_extractMapData && _otbmFile == null)
                            {
                                Console.WriteLine($"Extracting map data... ");
                                var pos = p.Position;
                                var currentDate = DateTime.UtcNow;
                                var fileNameData = new object[]
                                {
                                Path.GetFileNameWithoutExtension(filename), pos.X, pos.Y, pos.Z, currentDate.Day, currentDate.Month, currentDate.Year, currentDate.Hour, currentDate.Minute, currentDate.Second
                                };

                                var otbmName = string.Format("{0}__{1}_{2}_{3}__{4}_{5}_{6}__{7}_{8}_{9}", fileNameData);
                                var outputPath = $"{otbmName}.otbm";
                                if (!string.IsNullOrEmpty(_outDirectory))
                                {
                                    outputPath = Path.Combine(_outDirectory, outputPath);
                                }

                                _otbmFile = InitializeMapFile(otbmName, outputPath);
                            }

                            foreach (var field in p.Fields)
                            {
                                if (_extractMapData)
                                {
                                    ParseField(field);
                                }

                                for (var i = 0; i < MapSizeW; ++i)
                                {
                                    var obj = field.Item1.GetObject(i);
                                    if (obj == null)
                                    {
                                        continue;
                                    }

                                    if (_extractItemData && obj.Id > (int)CreatureInstanceType.Creature)
                                    {
                                        _itemFile.WriteLine($"{obj.Id} {field.Item2.ToString()}");
                                    }
                                    else if (obj.Id <= (int)CreatureInstanceType.Creature)
                                    {
                                        var creature = _client.CreatureStorage.GetCreature(obj.Data);
                                        if (creature == null)
                                        {
                                            continue;
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
                            return true;
                        };

                        while (reader.BaseStream.Position < reader.BaseStream.Length)
                        {
                            var packetType = PacketType.Server;
                            if (isOxRecording)
                            {
                                packetType = (PacketType)reader.ReadByte();
                                var timestamp = reader.ReadInt64();
                            }

                            var size = reader.ReadUInt32();
                            if ((reader.BaseStream.Length - reader.BaseStream.Position) < size)
                            {
                                break;
                            }

                            if (oxrFile != null)
                            {
                                var currentPosition = reader.BaseStream.Position;
                                oxrFile.Write((byte)PacketType.Server);
                                // Unfortunately, we don't know the timestamp of each packet.
                                oxrFile.Write(0L);
                                oxrFile.Write(size);
                                oxrFile.Write(reader.ReadBytes((int)size));
                                reader.BaseStream.Position = currentPosition;
                            }

                            // We don't care about client packets right now, so skip them.
                            if (packetType == PacketType.Client)
                            {
                                reader.BaseStream.Position += size;
                                continue;
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

                        if (oxrFile != null)
                        {
                            oxrFile.Close();
                        }

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

                if (_itemFile != null)
                {
                    _itemFile.Close();
                }

                if (_monsterFile != null)
                {
                    _monsterFile.Close();
                }

                if (_npcFile != null)
                {
                    _npcFile.Close();
                }

                Console.WriteLine("Extraction complete");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        static bool Proxy_OnReceivedMapPacket(Packet packet)
        {
            var p = (Map)packet;
            foreach (var field in p.Fields)
            {
                if (_extractMapData)
                {
                    ParseField(field);
                }

                for (var i = 0; i < MapSizeW; ++i)
                {
                    var obj = field.Item1.GetObject(i);
                    if (obj == null)
                    {
                        continue;
                    }

                    if (_extractItemData && obj.Id > (int)CreatureInstanceType.Creature)
                    {
                        _itemFile.WriteLine($"{obj.Id} {field.Item2.ToString()}");
                    }
                    else if (obj.Id <= (int)CreatureInstanceType.Creature)
                    {
                        var creature = _client.CreatureStorage.GetCreature(obj.Data);
                        if (creature == null)
                        {
                            continue;
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
            return true;
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
                    var description = "Saved with jo3bingham's tracker.";
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

        static void ParseField((Field, Position) field)
        {
            var position = field.Item2;

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

            for (int i = 0; i < 10; ++i)
            {
                var item = field.Item1.GetObject(i);
                if (item == null || item.Id == 97 || item.Id == 98 || item.Id == 99 || _ignoreIds.Contains(item.Id))
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

            // end tile node
            _otbmFile.WriteByte(255);

            // end tile area node
            _otbmFile.WriteByte(255);
        }
    }
}
