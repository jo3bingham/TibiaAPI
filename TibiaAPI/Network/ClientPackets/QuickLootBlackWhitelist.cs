using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class QuickLootBlackWhitelist : ClientPacket
    {
        public List<ushort> Items { get; } = new List<ushort>();

        public byte LootListType { get; set; }

        public QuickLootBlackWhitelist(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.QuickLootBlackWhitelist;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            LootListType = message.ReadByte();
            Items.Capacity = message.ReadUInt16();
            for (var i = 0; i < Items.Capacity; ++i)
            {
                Items.Add(message.ReadUInt16());
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.QuickLootBlackWhitelist);
            message.Write(LootListType);
            var count = Math.Min(Items.Count, ushort.MaxValue);
            message.Write((ushort)count);
            for (var i = 0; i < count; ++i)
            {
                message.Write(Items[i]);
            }
        }
    }
}
