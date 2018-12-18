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

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.Trappers)
            {
                return false;
            }

            CreatureIds.Capacity = message.ReadByte();
            for (var i = 0; i < CreatureIds.Capacity; ++i)
            {
                CreatureIds.Add(message.ReadUInt32());
            }
            return true;
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
