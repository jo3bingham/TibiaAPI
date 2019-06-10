using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class ClientCheck : ServerPacket
    {
        public List<byte> Data { get; } = new List<byte>();

        public ClientCheck(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.ClientCheck;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            Data.Capacity = (int)message.ReadUInt32();
            for (var i = 0; i < Data.Capacity; ++i)
            {
                Data.Add(message.ReadByte());
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.ClientCheck);
            var count = Math.Min(Data.Count, uint.MaxValue);
            message.Write((uint)count);
            for (var i = 0; i < count; ++i)
            {
                message.Write(Data[i]);
            }
        }
    }
}
