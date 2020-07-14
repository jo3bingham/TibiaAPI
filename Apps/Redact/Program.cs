using System;
using System.IO;
using System.Text.RegularExpressions;

using OXGaming.TibiaAPI;
using OXGaming.TibiaAPI.Constants;
using OXGaming.TibiaAPI.Network;

namespace Redact
{
    class Program
    {
        static Regex _lookPlayerRx = new Regex(@"you see .*\(level \d+\).*(she|he) is", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        static string _recordingName;

        static bool _keepClientPackets;

        static void ParseArgs(string[] args)
        {
            foreach (var arg in args)
            {
                if (!arg.Contains('=', StringComparison.CurrentCultureIgnoreCase))
                {
                    continue;
                }

                var splitArg = arg.Split('=');
                if (splitArg.Length == 1)
                {
                    switch (splitArg[0])
                    {
                        case "--keepclient":
                            {
                                _keepClientPackets = true;
                            }
                            break;
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
                            {
                                _recordingName = splitArg[1].Replace("\"", "");
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            if (args.Length <= 0)
            {
                Console.WriteLine($"Invalid number of arguments: {args.Length}");
                return;
            }

            ParseArgs(args);

            if (string.IsNullOrEmpty(_recordingName) || !_recordingName.EndsWith(".oxr", StringComparison.CurrentCultureIgnoreCase))
            {
                Console.WriteLine($"Invalid recording file: {_recordingName ?? "null"}");
                return;
            }

            if (!File.Exists(_recordingName))
            {
                Console.WriteLine($"File does not exist: {_recordingName}");
                return;
            }

            Redact();
        }

        static void Redact()
        {
            Console.WriteLine($"Redacting data from {_recordingName}...");

            var redactedFile = Path.GetFileNameWithoutExtension(_recordingName) + "_redacted.oxr";
            using var reader = new BinaryReader(File.OpenRead(_recordingName));
            using var writer = new BinaryWriter(File.OpenWrite(redactedFile));

            // OXR files begin with the client version they were recorded with.
            // This allows us to easily parse recordings from older client versions.
            var tibiaDirectory = string.Empty;
            var version = reader.ReadString();
            Console.WriteLine($"Client version: {version}");
            if (int.TryParse(version.Replace(".", ""), out var versionNumber))
            {
                var clientDataDirectory = $"ClientData/{versionNumber}";
                if (!Directory.Exists(clientDataDirectory))
                {
                    Console.WriteLine($"ClientData directory for version {version} doesn't exist. Falling back to default Tibia directory.");
                }
                else
                {
                    tibiaDirectory = clientDataDirectory;
                }
            }
            else
            {
                Console.WriteLine($"Invalid client version at beginning of recording: {version}");
            }

            var client = new Client(tibiaDirectory);

            // We don't want to completely block these packets.
            if (_keepClientPackets)
            {
                client.Connection.OnReceivedClientTalkPacket += Connection_OnReceivedClientTalkPacket;
            }
            client.Connection.OnReceivedServerFullMapPacket += Connection_OnReceivedServerMapPacket;
            client.Connection.OnReceivedServerBottomFloorPacket += Connection_OnReceivedServerMapPacket;
            client.Connection.OnReceivedServerBottomRowPacket += Connection_OnReceivedServerMapPacket;
            client.Connection.OnReceivedServerTopFloorPacket += Connection_OnReceivedServerMapPacket;
            client.Connection.OnReceivedServerTopRowPacket += Connection_OnReceivedServerMapPacket;
            client.Connection.OnReceivedServerLeftColumnPacket += Connection_OnReceivedServerMapPacket;
            client.Connection.OnReceivedServerRightColumnPacket += Connection_OnReceivedServerMapPacket;
            client.Connection.OnReceivedServerFieldDataPacket += Connection_OnReceivedServerMapPacket;
            client.Connection.OnReceivedServerCreateOnMapPacket += Connection_OnReceivedServerCreateOnMapPacket;
            client.Connection.OnReceivedServerCreatureUpdatePacket += Connection_OnReceivedServerCreatureUpdatePacket;
            client.Connection.OnReceivedServerTalkPacket += Connection_OnReceivedServerTalkPacket;
            client.Connection.OnReceivedServerMessagePacket += Connection_OnReceivedServerMessagePacket;

            // These packets aren't necessary in a redacted recording.
            if (_keepClientPackets)
            {
                client.Connection.OnReceivedClientAddBuddyPacket += Connection_OnReceivedUselessPacket;
                client.Connection.OnReceivedClientBuddyGroupPacket += Connection_OnReceivedUselessPacket;
                client.Connection.OnReceivedClientBugReportPacket += Connection_OnReceivedUselessPacket;
                client.Connection.OnReceivedClientEditBuddyPacket += Connection_OnReceivedUselessPacket;
                client.Connection.OnReceivedClientExcludeFromChannelPacket += Connection_OnReceivedUselessPacket;
                client.Connection.OnReceivedClientFriendSystemActionPacket += Connection_OnReceivedUselessPacket;
                client.Connection.OnReceivedClientLoginPacket += Connection_OnReceivedUselessPacket;
                client.Connection.OnReceivedClientPrivateChannelPacket += Connection_OnReceivedUselessPacket;
                client.Connection.OnReceivedClientRuleViolationReportPacket += Connection_OnReceivedUselessPacket;
            }
            client.Connection.OnReceivedServerBuddyDataPacket += Connection_OnReceivedUselessPacket;
            client.Connection.OnReceivedServerBuddyGroupDataPacket += Connection_OnReceivedUselessPacket;
            client.Connection.OnReceivedServerBuddyStatusChangePacket += Connection_OnReceivedUselessPacket;
            client.Connection.OnReceivedServerChannelEventPacket += Connection_OnReceivedUselessPacket;
            client.Connection.OnReceivedServerChannelsPacket += Connection_OnReceivedUselessPacket;
            client.Connection.OnReceivedServerCyclopediaCharacterInfoPacket += Connection_OnReceivedUselessPacket;
            client.Connection.OnReceivedServerCyclopediaCurrentHouseDataPacket += Connection_OnReceivedUselessPacket;
            client.Connection.OnReceivedServerCyclopediaStaticHouseDataPacket += Connection_OnReceivedUselessPacket;
            client.Connection.OnReceivedServerFriendSystemDataPacket += Connection_OnReceivedUselessPacket;
            client.Connection.OnReceivedServerInspectionListPacket += Connection_OnReceivedUselessPacket;
            client.Connection.OnReceivedServerOpenChannelPacket += Connection_OnReceivedUselessPacket;
            client.Connection.OnReceivedServerOpenOwnChannelPacket += Connection_OnReceivedUselessPacket;
            client.Connection.OnReceivedServerPrivateChannelPacket += Connection_OnReceivedUselessPacket;
            client.Connection.OnReceivedServerRequestPurchaseDataPacket += Connection_OnReceivedUselessPacket;
            client.Connection.OnReceivedServerScreenshotEventPacket += Connection_OnReceivedUselessPacket;
            client.Connection.OnReceivedServerUpdateExivaOptionsPacket += Connection_OnReceivedUselessPacket;

            // Packet modification isn't enabled by default when parsing packets.
            // Enabling it allows the `outMessage` parameter of the parsing methods
            // to reflect our changes so we can then write them to our new file.
            client.Connection.IsClientPacketModificationEnabled = true;
            client.Connection.IsServerPacketModificationEnabled = true;

            writer.Write(version);

            var clientSequenceNumber = 0u;
            var serverSequenceNumber = 0u;

            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                var packetType = (PacketType)reader.ReadByte();
                var timestamp = reader.ReadInt64();
                var size = reader.ReadUInt32();

                // If the Record app wasn't properly shutdown, a recording could
                // possibly be missing data at the end.
                if (reader.BaseStream.Length - reader.BaseStream.Position < size)
                {
                    break;
                }

                _ = reader.ReadUInt16(); // packet size
                _ = reader.ReadUInt32(); // sequence number
                var outMessage = new NetworkMessage(client);
                var message = new NetworkMessage(client)
                {
                    Size = size
                };

                reader.BaseStream.Position -= 6;
                Array.Copy(reader.ReadBytes((int)message.Size), message.GetBuffer(), message.Size);

                if (packetType == PacketType.Server)
                {
                    client.Connection.ParseServerMessage(client, message, outMessage);
                }
                else if (_keepClientPackets)
                {
                    client.Connection.ParseClientMessage(client, message, outMessage);
                }

                // If the `outMessage` doesn't contain any data we don't want to write it to the file.
                if (outMessage.Size <= 8)
                {
                    continue;
                }

                // Prepare the message without an XTEA key so that the proper
                // sizes are added to the packet data, but it stays unencrypted.
                outMessage.PrepareToSend(null);
                outMessage.SequenceNumber = packetType == PacketType.Client ? clientSequenceNumber++ : serverSequenceNumber++;

                var data = outMessage.GetData();

                writer.Write((byte)packetType);
                writer.Write(timestamp);
                writer.Write(data.Length);
                writer.Write(data);
            }

            writer.Flush();
        }

        private static bool Connection_OnReceivedClientTalkPacket(Packet packet)
        {
            var p = (OXGaming.TibiaAPI.Network.ClientPackets.Talk)packet;
            p.SpeakerName = "Redacted";
            return true;
        }

        private static bool Connection_OnReceivedServerMessagePacket(Packet packet)
        {
            var p = (OXGaming.TibiaAPI.Network.ServerPackets.Message)packet;
            if (p.MessageMode == MessageModeType.Look)
            {
                if (_lookPlayerRx.IsMatch(p.Text) || p.Text.Contains("You see yourself.", StringComparison.OrdinalIgnoreCase))
                {
                    p.Text = "Redacted";
                }
            }
            return true;
        }

        private static bool Connection_OnReceivedServerTalkPacket(Packet packet)
        {
            var p = (OXGaming.TibiaAPI.Network.ServerPackets.Talk)packet;
            var creature = p.Client.CreatureStorage.GetCreature(p.SpeakerName);
            if (creature is null || creature.Type == CreatureType.Player)
            {
                p.SpeakerLevel = 100;
                p.SpeakerName = "Redacted";
            }
            else if (creature is object && creature.Type == CreatureType.Npc)
            {
                // In case the NPC used the player's name in their message,
                // we need to check and replace it.
                p.Text = p.Text.Replace(p.Client.Player.Name, "Redacted", StringComparison.OrdinalIgnoreCase);
            }
            return true;
        }

        private static bool Connection_OnReceivedServerCreatureUpdatePacket(Packet packet)
        {
            var p = (OXGaming.TibiaAPI.Network.ServerPackets.CreatureUpdate)packet;
            if (p.Creature is object && p.Creature.Type == CreatureType.Player)
            {
                p.Creature.Name = "Redacted";
            }
            return true;
        }

        private static bool Connection_OnReceivedServerCreateOnMapPacket(Packet packet)
        {
            var p = (OXGaming.TibiaAPI.Network.ServerPackets.CreateOnMap)packet;
            if (p.Creature is object && p.Creature.Type == CreatureType.Player)
            {
                p.Creature.Name = "Redacted";
            }
            return true;
        }

        private static bool Connection_OnReceivedServerMapPacket(Packet packet)
        {
            var p = (OXGaming.TibiaAPI.Network.ServerPackets.Map)packet;
            foreach (var field in p.Fields)
            {
                foreach (var obj in field.Objects)
                {
                    if (obj is null || obj.Id >= 100)
                    {
                        continue;
                    }

                    var creature = p.Client.CreatureStorage.GetCreature(obj.Data);
                    if (creature is null || creature.Type != CreatureType.Player)
                    {
                        continue;
                    }

                    creature.Name = "Redacted";
                }
            }
            return true;
        }

        private static bool Connection_OnReceivedUselessPacket(Packet _)
        {
            // Because we don't care about this data being in a redacted recording
            // we can just return `false` here and it won't be added to the `outMessage`.
            return false;
        }
    }
}
