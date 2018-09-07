using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class ClientCheck : ServerPacket
    {
        public List<byte> Data { get; } = new List<byte>();

        public ClientCheck()
        {
            PacketType = ServerPacketType.ClientCheck;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.ClientCheck)
            {
                return false;
            }

            Data.Capacity = (int)message.ReadUInt32();
            for (var i = 0; i < Data.Capacity; ++i)
            {
                Data.Add(message.ReadByte());
            }
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.ClientCheck);
            var count = (uint)Math.Min(Data.Count, uint.MaxValue);
            message.Write(count);
            for (var i = 0; i < count; ++i)
            {
                message.Write(Data[i]);
            }
        }
    }
}
