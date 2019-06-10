using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class GoPath : ClientPacket
    {
        public List<Direction> Directions { get; } = new List<Direction>();

        public GoPath(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.GoPath;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            Directions.Capacity = message.ReadByte();
            for (var i = 0; i < Directions.Capacity; ++i)
            {
                Directions.Add((Direction)message.ReadByte());
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.GoPath);
            var count = Math.Min(Directions.Count, byte.MaxValue);
            message.Write((byte)count);
            for (var i = 0; i < count; ++i)
            {
                message.Write((byte)Directions[i]);
            }
        }
    }
}
