﻿using System;

using OXGaming.TibiaAPI.Constants;
using OXGaming.TibiaAPI.Utilities;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class Message : ServerPacket
    {
        public MessageModeType MessageMode { get; set; } = MessageModeType.None;

        public Position Position { get; set; }

        public string Text { get; set; }

        public uint FirstValue { get; set; }
        public uint SecondValue { get; set; }

        public ushort ChannelId { get; set; }

        public byte FirstColor { get; set; }
        public byte SecondColor { get; set; }

        public Message(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.Message;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            MessageMode = (MessageModeType)message.ReadByte();
            switch (MessageMode)
            {
                case MessageModeType.ChannelManagement:
                    {
                        ChannelId = message.ReadUInt16();
                    }
                    break;
                case MessageModeType.Guild:
                case MessageModeType.PartyManagement:
                case MessageModeType.Party:
                    {
                        ChannelId = message.ReadUInt16();
                    }
                    break;
                case MessageModeType.Login:
                case MessageModeType.Admin:
                case MessageModeType.Game:
                case MessageModeType.GameHighlight:
                case MessageModeType.Failure:
                    break;
                case MessageModeType.Look:
                    break;
                case MessageModeType.Status:
                case MessageModeType.Loot:
                case MessageModeType.TradeNpc:
                case MessageModeType.HotkeyUse:
                case MessageModeType.Market:
                case MessageModeType.Report:
                    break;
                case MessageModeType.DamageDealed:
                case MessageModeType.DamageReceived:
                case MessageModeType.DamageOthers:
                    {
                        Position = message.ReadPosition();
                        FirstValue = message.ReadUInt32();
                        FirstColor = message.ReadByte();
                        SecondValue = message.ReadUInt32();
                        SecondColor = message.ReadByte();
                    }
                    break;
                case MessageModeType.Heal:
                case MessageModeType.Mana:
                case MessageModeType.Exp:
                case MessageModeType.HealOthers:
                case MessageModeType.ExpOthers:
                    {
                        Position = message.ReadPosition();
                        FirstValue = message.ReadUInt32();
                        FirstColor = message.ReadByte();
                    }
                    break;
                default:
                    throw new Exception($"Invalid MessageMode: {MessageMode}");
            }
            Text = message.ReadString();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.Message);
            message.Write((byte)MessageMode);
            switch (MessageMode)
            {
                case MessageModeType.ChannelManagement:
                    {
                        message.Write(ChannelId);
                    }
                    break;
                case MessageModeType.Guild:
                case MessageModeType.PartyManagement:
                case MessageModeType.Party:
                    {
                        message.Write(ChannelId);
                    }
                    break;
                case MessageModeType.Login:
                case MessageModeType.Admin:
                case MessageModeType.Game:
                case MessageModeType.GameHighlight:
                case MessageModeType.Failure:
                    break;
                case MessageModeType.Look:
                    break;
                case MessageModeType.Status:
                case MessageModeType.Loot:
                case MessageModeType.TradeNpc:
                case MessageModeType.HotkeyUse:
                case MessageModeType.Market:
                case MessageModeType.Report:
                    break;
                case MessageModeType.DamageDealed:
                case MessageModeType.DamageReceived:
                case MessageModeType.DamageOthers:
                    {
                        message.Write(Position);
                        message.Write(FirstValue);
                        message.Write(FirstColor);
                        message.Write(SecondValue);
                        message.Write(SecondColor);
                    }
                    break;
                case MessageModeType.Heal:
                case MessageModeType.Mana:
                case MessageModeType.Exp:
                case MessageModeType.HealOthers:
                case MessageModeType.ExpOthers:
                    {
                        message.Write(Position);
                        message.Write(FirstValue);
                        message.Write(FirstColor);
                    }
                    break;
                default:
                    throw new Exception($"Invalid MessageMode: {MessageMode}");
            }
            message.Write(Text);
        }
    }
}
