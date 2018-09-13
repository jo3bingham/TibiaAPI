using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

using OXGaming.TibiaAPI;
using OXGaming.TibiaAPI.Network;
using OXGaming.TibiaAPI.Network.ServerPackets;
using OXGaming.TibiaAPI.Utilities;
using OXGaming.TibiaAPI.WorldMap;

namespace Extract
{
    class Program
    {
        static readonly byte[] ReverseFluidMap =
        {
            (byte)Enums.FluidColors.FLUID_EMPTY,
            (byte)Enums.FluidTypes.FLUID_WATER,
            (byte)Enums.FluidTypes.FLUID_MANA,
            (byte)Enums.FluidTypes.FLUID_BEER,
            (byte)Enums.FluidColors.FLUID_EMPTY,
            (byte)Enums.FluidTypes.FLUID_BLOOD,
            (byte)Enums.FluidTypes.FLUID_SLIME,
            (byte)Enums.FluidColors.FLUID_EMPTY,
            (byte)Enums.FluidTypes.FLUID_LEMONADE,
            (byte)Enums.FluidTypes.FLUID_MILK
        };

        static HashSet<ulong> _knownPositions = new HashSet<ulong>();

        static HashSet<uint> _ignoreIds = new HashSet<uint>();

        static Dictionary<uint, uint> _replaceIds = new Dictionary<uint, uint>();

        static void Main(string[] args)
        {
            try
            {
                if (args.Length <= 0)
                {
                    Console.WriteLine("Invalid argument.");
                    return;
                }

                var isDirectory = true;
                if (args[0].EndsWith(".dat", StringComparison.CurrentCultureIgnoreCase))
                {
                    isDirectory = false;
                }

                if (isDirectory && !Directory.Exists(args[0]))
                {
                    Console.WriteLine($"Directory does not exist: {args[0]}");
                    return;
                }
                else if (!isDirectory && !File.Exists(args[0]))
                {
                    Console.WriteLine($"File does not exist: {args[0]}");
                    return;
                }

                var filenames = new List<string>();
                if (isDirectory)
                {
                    filenames.AddRange(Directory.GetFiles(args[0]).Where(f => f.EndsWith(".dat", StringComparison.CurrentCultureIgnoreCase)));
                }
                else
                {
                    filenames.Add(args[0]);
                }

                var outputDirectory = string.Empty;
                if (args.Length >= 2)
                {
                    outputDirectory = args[1];
                    if (!Directory.Exists(outputDirectory))
                    {
                        Directory.CreateDirectory(outputDirectory);
                    }
                }

                LoadXML("ignore.xml");
                LoadXML("replace.xml");

                Console.WriteLine($"Converting {filenames.Count} recordings...");
                foreach (var filename in filenames)
                {
                    Console.WriteLine($"Converting {filename} to OTBM file... ");
                    using (var reader = new BinaryReader(File.OpenRead(filename)))
                    {
                        FileStream file = null;
                        var client = new Client("Tibia11.dat");

                        client.Proxy.OnReceivedServerLoginChallengePacket += (packet) =>
                        {
                            client.WorldMapStorage.ResetMap();
                            return true;
                        };

                        client.Proxy.OnReceivedServerBottomFloorPacket += (packet) =>
                        {
                            var p = (BottomFloor)packet;
                            foreach (var field in p.Fields)
                            {
                                ParseField(file, field);
                            }
                            return true;
                        };

                        client.Proxy.OnReceivedServerBottomRowPacket += (packet) =>
                        {
                            var p = (BottomRow)packet;
                            foreach (var field in p.Fields)
                            {
                                ParseField(file, field);
                            }
                            return true;
                        };

                        client.Proxy.OnReceivedServerTopFloorPacket += (packet) =>
                        {
                            var p = (TopFloor)packet;
                            foreach (var field in p.Fields)
                            {
                                ParseField(file, field);
                            }
                            return true;
                        };

                        client.Proxy.OnReceivedServerTopRowPacket += (packet) =>
                        {
                            var p = (TopRow)packet;
                            foreach (var field in p.Fields)
                            {
                                ParseField(file, field);
                            }
                            return true;
                        };

                        client.Proxy.OnReceivedServerLeftColumnPacket += (packet) =>
                        {
                            var p = (LeftColumn)packet;
                            foreach (var field in p.Fields)
                            {
                                ParseField(file, field);
                            }
                            return true;
                        };

                        client.Proxy.OnReceivedServerRightColumnPacket += (packet) =>
                        {
                            var p = (RightColumn)packet;
                            foreach (var field in p.Fields)
                            {
                                ParseField(file, field);
                            }
                            return true;
                        };

                        client.Proxy.OnReceivedServerFieldDataPacket += (packet) =>
                        {
                            var p = (FieldData)packet;
                            ParseField(file, p.Field);
                            return true;
                        };

                        client.Proxy.OnReceivedServerFullMapPacket += (packet) =>
                        {
                            if (file != null)
                            {
                                return true;
                            }

                            var p = (FullMap)packet;
                            var pos = p.Position;
                            var currentDate = DateTime.UtcNow;
                            var fileNameData = new object[]
                            {
                                pos.X, pos.Y, pos.Z, currentDate.Day, currentDate.Month, currentDate.Year, currentDate.Hour, currentDate.Minute, currentDate.Second
                            };

                            var otbmName = string.Format("{0}_{1}_{2}__{3}_{4}_{5}__{6}_{7}_{8}", fileNameData);
                            var outputPath = $"{otbmName}.otbm";
                            if (!string.IsNullOrEmpty(outputDirectory))
                            {
                                outputPath = Path.Combine(outputDirectory, outputPath);
                            }

                            file = InitializeMapFile(otbmName, outputPath);

                            foreach (var field in p.Fields)
                            {
                                ParseField(file, field);
                            }
                            return true;
                        };

                        while (reader.BaseStream.Position < reader.BaseStream.Length)
                        {
                            var size = reader.ReadUInt32();
                            var wholeSize = reader.ReadUInt16();
                            var sequenceNumber = reader.ReadUInt32();
                            var packetSize = reader.ReadUInt16();
                            var outMessage = new NetworkMessage();
                            var message = new NetworkMessage()
                            {
                                Size = size
                            };

                            reader.BaseStream.Position -= 8;
                            Array.Copy(reader.ReadBytes((int)message.Size), message.GetBuffer(), message.Size);

                            client.Proxy.ParseServerMessage(client, message, outMessage);
                        }

                        if (file != null)
                        {
                            // node towns
                            file.WriteByte(254);
                            file.WriteByte(6);

                            // end towns node
                            file.WriteByte(255);

                            // end map data node
                            file.WriteByte(255);

                            // end root node
                            file.WriteByte(255);

                            file.Close();
                        }

                        _knownPositions.Clear();
                    }

                    Console.WriteLine("Done");
                }

                Console.WriteLine("Conversion complete");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
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
                WriteData(file, BitConverter.GetBytes((ushort)64764));
                // map height
                WriteData(file, BitConverter.GetBytes((ushort)64764));

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

        static void ParseField(FileStream file, (Field, Position) field)
        {
            var position = field.Item2;

            var index = (ulong)((position.Z * 40959 * 40959) + (position.Y * 40959) + position.X);
            if (_knownPositions.Contains(index))
            {
                return;
            }

            _knownPositions.Add(index);

            // node tile area
            file.WriteByte(254);
            file.WriteByte(4);

            // position
            WriteData(file, BitConverter.GetBytes((ushort)(position.X & 0xFF00)));
            WriteData(file, BitConverter.GetBytes((ushort)(position.Y & 0xFF00)));
            WriteData(file, new byte[] { (byte)position.Z });

            // node tile
            file.WriteByte(254);
            file.WriteByte(5);

            // x/y
            WriteData(file, new byte[] { (byte)(position.X & 0xFF) });
            WriteData(file, new byte[] { (byte)(position.Y & 0xFF) });

            for (int i = 0; i < 10; ++i)
            {
                var item = field.Item1.GetObject(i);
                if (item == null || item.Id == 97 || item.Id == 98 || item.Id == 99 || _ignoreIds.Contains(item.Id))
                {
                    continue;
                }

                // node item
                file.WriteByte(254);
                file.WriteByte(6);

                // item id
                if (!_replaceIds.TryGetValue(item.Id, out uint id))
                {
                    id = item.Id;
                }
                WriteData(file, BitConverter.GetBytes((ushort)id));

                // item data
                if (item.Type != null && item.Type.Flags.Cumulative)
                {
                    file.WriteByte(15);
                    WriteData(file, new byte[] { (byte)item.Data });
                }

                // item sub type
                if (item.Type != null && (item.Type.Flags.Liquidcontainer || item.Type.Flags.Liquidpool))
                {
                    file.WriteByte(15);
                    byte subType = 0;
                    if (item.Data >= 0 && item.Data < ReverseFluidMap.Length)
                    {
                        subType = ReverseFluidMap[item.Data];
                    }
                    WriteData(file, new byte[] { subType });
                }

                //end item node
                file.WriteByte(255);
            }

            // end tile node
            file.WriteByte(255);

            // end tile area node
            file.WriteByte(255);
        }
    }
}
