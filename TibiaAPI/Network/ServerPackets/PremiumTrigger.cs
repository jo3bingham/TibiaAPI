using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class PremiumTrigger : ServerPacket
    {
        public List<byte> PremiumTriggers { get; } = new List<byte>();

        public PremiumTrigger(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.PremiumTrigger;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            PremiumTriggers.Capacity = message.ReadByte();
            for (var i = 0; i < PremiumTriggers.Capacity; ++i)
            {
                PremiumTriggers.Add(message.ReadByte());
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.PremiumTrigger);
            var count = Math.Min(PremiumTriggers.Count, byte.MaxValue);
            message.Write((byte)count);
            for (var i = 0; i < count; ++i)
            {
                message.Write(PremiumTriggers[i]);
            }
        }
    }
}
