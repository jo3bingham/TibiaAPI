using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class Trappers : ServerPacket
    {
        public List<uint> CreatureIds { get; } = new List<uint>();

        public Trappers(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.Trappers;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            CreatureIds.Capacity = message.ReadByte();
            for (var i = 0; i < CreatureIds.Capacity; ++i)
            {
                CreatureIds.Add(message.ReadUInt32());
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.Trappers);
            var count = Math.Min(CreatureIds.Count, byte.MaxValue);
            message.Write((byte)count);
            for (var i = 0; i < count; ++i)
            {
                message.Write(CreatureIds[i]);
            }
        }
    }
}
