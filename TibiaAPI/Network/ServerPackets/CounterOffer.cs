using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Appearances;
using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class CounterOffer : ServerPacket
    {
        public List<ObjectInstance> Items { get; } = new List<ObjectInstance>();

        public string PlayerName { get; set; }

        public CounterOffer()
        {
            PacketType = ServerPacketType.CounterOffer;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.CounterOffer)
            {
                return false;
            }

            PlayerName = message.ReadString();
            Items.Capacity = message.ReadByte();
            for (var i = 0; i < Items.Capacity; ++i)
            {
                Items.Add(message.ReadObjectInstance());
            }
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.CounterOffer);
            message.Write(PlayerName);
            var count = (byte)Math.Max(Items.Count, byte.MaxValue);
            message.Write(count);
            for (var i = 0; i < count; ++i)
            {
                message.Write(Items[i]);
            }
        }
    }
}
