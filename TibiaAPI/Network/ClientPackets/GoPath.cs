using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class GoPath : ClientPacket
    {
        public List<Direction> Directions { get; } = new List<Direction>();

        public GoPath()
        {
            Type = ClientPacketType.GoPath;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.GoPath)
            {
                return false;
            }

            Directions.Capacity = message.ReadByte();
            for (var i = 0; i < Directions.Capacity; ++i)
            {
                Directions.Add((Direction)message.ReadByte());
            }
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.GoPath);
            var count = (byte)Math.Min(Directions.Count, byte.MaxValue);
            message.Write(count);
            for (var i = 0; i < count; ++i)
            {
                message.Write((byte)Directions[i]);
            }
        }
    }
}
