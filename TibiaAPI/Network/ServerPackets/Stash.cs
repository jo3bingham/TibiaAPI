using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class Stash : ServerPacket
    {
        public List<(ushort ItemId, uint Count)> Items { get; } = new List<(ushort ItemId, uint Count)>();

        public ushort FreeSlots { get; set; }

        public Stash(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.Stash;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            Items.Capacity = message.ReadUInt16();
            for (var i = 0; i < Items.Capacity; ++i)
            {
                var itemId = message.ReadUInt16();
                var itemCount = message.ReadUInt32();
                Items.Add((itemId, itemCount));
            }
            FreeSlots = message.ReadUInt16();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.Stash);
            var count = Math.Min(Items.Count, ushort.MaxValue);
            message.Write((ushort)count);
            for (var i = 0; i < count; ++i)
            {
                var (ItemId, Count) = Items[i];
                message.Write(ItemId);
                message.Write(Count);
            }
            message.Write(FreeSlots);
        }
    }
}
