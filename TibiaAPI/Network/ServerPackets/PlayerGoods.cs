﻿using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class PlayerGoods : ServerPacket
    {
        public List<(ushort Id, byte Count)> Goods { get; } = new List<(ushort Id, byte Count)>();

        public ulong Money { get; set; }

        public PlayerGoods()
        {
            PacketType = ServerPacketType.PlayerGoods;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.PlayerGoods)
            {
                return false;
            }

            Money = message.ReadUInt64();
            Goods.Capacity = message.ReadByte();
            for (var i = 0; i < Goods.Capacity; ++i)
            {
                var id = message.ReadUInt16();
                var count = message.ReadByte();
                Goods.Add((id, count));
            }
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.PlayerGoods);
            message.Write(Money);
            var count = (byte)Math.Min(Goods.Count, byte.MaxValue);
            message.Write(count);
            for (var i = 0; i < count; ++i)
            {
                var (Id, Count) = Goods[i];
                message.Write(Id);
                message.Write(Count);
            }
        }
    }
}
