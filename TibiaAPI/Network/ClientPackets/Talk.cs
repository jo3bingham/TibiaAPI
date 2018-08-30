using System;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class Talk : ClientPacket
    {
        public MessageModeType MessageMode { get; set; }

        public string SpeakerName { get; set; }
        public string Text { get; set; }

        public ushort ChannelId { get; set; }

        public Talk()
        {
            Type = ClientPacketType.Talk;
        }

        public override bool ParseMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)Type)
            {
                return false;
            }

            MessageMode = (MessageModeType)message.ReadByte();
            switch (MessageMode)
            {
                case MessageModeType.Say:
                case MessageModeType.Whisper:
                case MessageModeType.Yell:
                    break;
                case MessageModeType.Channel:
                    {
                        ChannelId = message.ReadUInt16();
                    }
                    break;
                case MessageModeType.PrivateTo:
                    {
                        SpeakerName = message.ReadString();
                    }
                    break;
                case MessageModeType.NpcTo:
                    break;
                case MessageModeType.GamemasterBroadcast:
                    break;
                case MessageModeType.GamemasterChannel:
                    {
                        ChannelId = message.ReadUInt16();
                    }
                    break;
                case MessageModeType.GamemasterPrivateTo:
                    {
                        SpeakerName = message.ReadString();
                    }
                    break;
                default:
                    throw new Exception($"Invalid MessageMode: {MessageMode}");
            }
            Text = message.ReadString();
            return true;
        }

        public override void AppendToMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.Talk);
            message.Write((byte)MessageMode);
            switch (MessageMode)
            {
                case MessageModeType.Say:
                case MessageModeType.Whisper:
                case MessageModeType.Yell:
                    break;
                case MessageModeType.Channel:
                    {
                        message.Write(ChannelId);
                    }
                    break;
                case MessageModeType.PrivateTo:
                    {
                        message.Write(SpeakerName);
                    }
                    break;
                case MessageModeType.NpcTo:
                    break;
                case MessageModeType.GamemasterBroadcast:
                    break;
                case MessageModeType.GamemasterChannel:
                    {
                        message.Write(ChannelId);
                    }
                    break;
                case MessageModeType.GamemasterPrivateTo:
                    {
                        message.Write(SpeakerName);
                    }
                    break;
                default:
                    throw new Exception($"Invalid MessageMode: {MessageMode}");
            }
            message.Write(Text);
        }
    }
}
