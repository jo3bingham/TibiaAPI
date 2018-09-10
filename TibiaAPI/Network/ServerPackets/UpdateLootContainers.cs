using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class UpdateLootContainers : ServerPacket
    {
        public List<(byte Id, ushort ObjectId)> LootContainers { get; } = new List<(byte Id, ushort ObjectId)>();
        public bool UseMainContainerAsFallback { get; set; }

        public UpdateLootContainers()
        {
            PacketType = ServerPacketType.UpdateLootContainers;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.UpdateLootContainers)
            {
                return false;
            }

            UseMainContainerAsFallback = message.ReadBool();

            LootContainers.Capacity = message.ReadByte();
            for (var i = 0; i < LootContainers.Capacity; ++i)
            {
                var id = message.ReadByte();
                var objectId = message.ReadUInt16();
                LootContainers.Add((id, objectId));
            }
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.UpdateLootContainers);
            message.Write(UseMainContainerAsFallback);
            var count = (byte)Math.Min(LootContainers.Count, byte.MaxValue);
            message.Write(count);
            for (var i = 0; i < count; ++i)
            {
                var lootContainer = LootContainers[i];
                message.Write(lootContainer.Id);
                message.Write(lootContainer.ObjectId);
            }
        }
    }
}
