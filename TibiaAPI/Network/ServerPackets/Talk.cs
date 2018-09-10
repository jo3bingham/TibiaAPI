﻿using System;

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

        public Talk()
        {
            PacketType = ServerPacketType.Talk;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.Talk)
            {
                return false;
            }

            StatementId = message.ReadUInt32();
            SpeakerName = message.ReadString();
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
                    throw new Exception("[ServerPackets.Talk.ParseFromNetworkMessage] Invalid MessageMode: " + MessageMode.ToString());
            }
            Text = message.ReadString();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.Talk);
            message.Write(StatementId);
            message.Write(SpeakerName);
            message.Write(SpeakerLevel);
            message.Write((byte)MessageMode);
            switch (MessageMode)
            {
                case MessageModeType.Say:
                case MessageModeType.Whisper:
                case MessageModeType.Yell:
                case MessageModeType.Spell:
                    {
                        message.Write(Position);
                    }
                    break;
                case MessageModeType.Channel:
                case MessageModeType.ChannelHighlight:
                case MessageModeType.GamemasterChannel:
                    {
                        message.Write(ChannelId);
                    }
                    break;
                case MessageModeType.NpcFromStartBlock:
                case MessageModeType.BarkLow:
                case MessageModeType.BarkLoud:
                    {
                        message.Write(Position);
                    }
                    break;
                default:
                    throw new Exception("[ServerPackets.Talk.AppendToNetworkMessage] Invalid MessageMode: " + MessageMode.ToString());
            }
            message.Write(Text);
        }
    }
}