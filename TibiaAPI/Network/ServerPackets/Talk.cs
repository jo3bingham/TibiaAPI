using System;

using OXGaming.TibiaAPI.Constants;
using OXGaming.TibiaAPI.Utilities;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class Talk : ServerPacket
    {
        public MessageModeType MessageMode { get; set; }

        public Position Position { get; set; }

        public string SpeakerName { get; set; }
        public string Text { get; set; }

        public uint StatementId { get; set; }

        public ushort ChannelId { get; set; }
        public ushort SpeakerLevel { get; set; }

        public bool IsTraded { get; set; }

        public Talk(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.Talk;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            StatementId = message.ReadUInt32();
            SpeakerName = message.ReadString();
            if (Client.VersionNumber >= 125010109 && StatementId != 0)
            {
                IsTraded = message.ReadBool();
            }
            SpeakerLevel = message.ReadUInt16();
            MessageMode = (MessageModeType)message.ReadByte();
            switch (MessageMode)
            {
                case MessageModeType.Say:
                case MessageModeType.Whisper:
                case MessageModeType.Yell:
                    {
                        Position = message.ReadPosition();
                    }
                    break;
                case MessageModeType.PrivateFrom:
                    break;
                case MessageModeType.Channel:
                case MessageModeType.ChannelHighlight:
                    {
                        ChannelId = message.ReadUInt16();
                    }
                    break;
                case MessageModeType.Spell:
                case MessageModeType.Potion:
                    {
                        Position = message.ReadPosition();
                    }
                    break;
                case MessageModeType.NpcFromStartBlock:
                    {
                        Position = message.ReadPosition();
                    }
                    break;
                case MessageModeType.NpcFrom:
                    break;
                case MessageModeType.GamemasterBroadcast:
                    break;
                case MessageModeType.GamemasterChannel:
                    {
                        ChannelId = message.ReadUInt16();
                    }
                    break;
                case MessageModeType.GamemasterPrivateFrom:
                    break;
                case MessageModeType.BarkLow:
                case MessageModeType.BarkLoud:
                    {
                        Position = message.ReadPosition();
                    }
                    break;
                default:
                    Client.Logger.Warning("[ServerPackets.Talk.ParseFromNetworkMessage] Invalid MessageMode: " + MessageMode.ToString());
                    break;
            }
            Text = message.ReadString();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.Talk);
            message.Write(StatementId);
            message.Write(SpeakerName);
            if (Client.VersionNumber >= 125010109 && StatementId != 0)
            {
                message.Write(IsTraded);
            }
            message.Write(SpeakerLevel);
            message.Write((byte)MessageMode);
            switch (MessageMode)
            {
                case MessageModeType.Say:
                case MessageModeType.Whisper:
                case MessageModeType.Yell:
                    {
                        message.Write(Position);
                    }
                    break;
                case MessageModeType.PrivateFrom:
                    break;
                case MessageModeType.Channel:
                case MessageModeType.ChannelHighlight:
                    {
                        message.Write(ChannelId);
                    }
                    break;
                case MessageModeType.Spell:
                    {
                        message.Write(Position);
                    }
                    break;
                case MessageModeType.NpcFromStartBlock:
                    {
                        message.Write(Position);
                    }
                    break;
                case MessageModeType.NpcFrom:
                    break;
                case MessageModeType.GamemasterBroadcast:
                    break;
                case MessageModeType.GamemasterChannel:
                    {
                        message.Write(ChannelId);
                    }
                    break;
                case MessageModeType.GamemasterPrivateFrom:
                    break;
                case MessageModeType.BarkLow:
                case MessageModeType.BarkLoud:
                    {
                        message.Write(Position);
                    }
                    break;
                default:
                    Client.Logger.Warning("[ServerPackets.Talk.AppendToNetworkMessage] Invalid MessageMode: " + MessageMode.ToString());
                    break;
            }
            message.Write(Text);
        }
    }
}
