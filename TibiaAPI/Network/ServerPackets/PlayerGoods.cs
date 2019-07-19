using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class PlayerGoods : ServerPacket
    {
        public List<(ushort Id, byte Count)> Goods { get; } = new List<(ushort Id, byte Count)>();

        public ulong Money { get; set; }

        public PlayerGoods(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.PlayerGoods;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            Money = message.ReadUInt64();
            Goods.Capacity = message.ReadByte();
            for (var i = 0; i < Goods.Capacity; ++i)
            {
                var id = message.ReadUInt16();
                var count = message.ReadByte();
                Goods.Add((id, count));
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.PlayerGoods);
            message.Write(Money);
            var count = Math.Min(Goods.Count, byte.MaxValue);
            message.Write((byte)count);
            for (var i = 0; i < count; ++i)
            {
                var (Id, Count) = Goods[i];
                message.Write(Id);
                message.Write(Count);
            }
        }
    }
}
