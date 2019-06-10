using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class PlayerInventory : ServerPacket
    {
        public List<(ushort Id, byte Data, ushort Count)> Items { get; } = new List<(ushort Id, byte Data, ushort Count)>();

        public PlayerInventory(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.PlayerInventory;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            Items.Capacity = message.ReadUInt16();
            for (var i = 0; i < Items.Capacity; ++i)
            {
                var id = message.ReadUInt16();
                var data = message.ReadByte();
                var count = message.ReadUInt16();
                Items.Add((id, data, count));
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.PlayerInventory);
            var count = Math.Min(Items.Count, ushort.MaxValue);
            message.Write((ushort)count);
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
