using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class PlayerInventory : ServerPacket
    {
        public List<(ushort Id, byte Data, ushort Count)> Items { get; } = new List<(ushort Id, byte Data, ushort Count)>();

        public PlayerInventory()
        {
            PacketType = ServerPacketType.PlayerInventory;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.PlayerInventory)
            {
                return false;
            }

            Items.Capacity = message.ReadUInt16();
            for (var i = 0; i < Items.Capacity; ++i)
            {
                var id = message.ReadUInt16();
                var data = message.ReadByte();
                var count = message.ReadUInt16();
                Items.Add((id, data, count));
            }
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.PlayerInventory);
            var count = (ushort)Math.Min(Items.Count, ushort.MaxValue);
            message.Write(count);
            for (var i = 0; i < count; ++i)
            {
                var (Id, Data, Count) = Items[i];
                message.Write(Id);
                message.Write(Data);
                message.Write(Count);
            }
        }
    }
}
