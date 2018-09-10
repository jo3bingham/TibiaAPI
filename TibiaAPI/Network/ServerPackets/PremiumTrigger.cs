using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class PremiumTrigger : ServerPacket
    {
        public List<byte> PremiumTriggers { get; } = new List<byte>();

        public PremiumTrigger()
        {
            PacketType = ServerPacketType.PremiumTrigger;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.PremiumTrigger)
            {
                return false;
            }

            PremiumTriggers.Capacity = message.ReadByte();
            for (var i = 0; i < PremiumTriggers.Capacity; ++i)
            {
                PremiumTriggers.Add(message.ReadByte());
            }
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.PremiumTrigger);
            var count = (byte)Math.Min(PremiumTriggers.Count, byte.MaxValue);
            message.Write(count);
            for (var i = 0; i < count; ++i)
            {
                message.Write(PremiumTriggers[i]);
            }
        }
    }
}
