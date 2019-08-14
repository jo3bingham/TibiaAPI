using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class DepotSearchResults : ServerPacket
    {
        public List<(ushort ItemId, ushort Amount)> Items { get; } = new List<(ushort ItemId, ushort Amount)>();

        public DepotSearchResults(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.DepotSearchResults;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            Items.Capacity = message.ReadUInt16();
            for (var i = 0; i < Items.Capacity; ++i)
            {
                var itemId = message.ReadUInt16();
                var amount = message.ReadUInt16();
                Items.Add((itemId, amount));
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.DepotSearchResults);
            var count = Math.Min(Items.Count, byte.MaxValue);
            message.Write((ushort)count);
            for (var i = 0; i < count; ++i)
            {
                var (itemId, amount) = Items[i];
                message.Write(itemId);
                message.Write(amount);
            }
        }
    }
}
