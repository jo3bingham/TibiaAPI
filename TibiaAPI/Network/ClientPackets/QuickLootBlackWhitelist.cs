using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class QuickLootBlackWhitelist : ClientPacket
    {
        public List<ushort> Items { get; } = new List<ushort>();

        public byte LootListType { get; set; }

        // This is more-than-likely a count variable.
        public ushort Unknown { get; set; }

        public QuickLootBlackWhitelist()
        {
            PacketType = ClientPacketType.QuickLootBlackWhitelist;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.QuickLootBlackWhitelist)
            {
                return false;
            }

            LootListType = message.ReadByte();
            Items.Capacity = message.ReadUInt16();
            for (var i = 0; i < Items.Capacity; ++i)
            {
                Items.Add(message.ReadUInt16());
            }
            return true;
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
