using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Appearances;
using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class OwnOffer : ServerPacket
    {
        public List<ObjectInstance> Items { get; } = new List<ObjectInstance>();

        public string PlayerName { get; set; }

        public OwnOffer()
        {
            PacketType = ServerPacketType.OwnOffer;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.OwnOffer)
            {
                return false;
            }

            PlayerName = message.ReadString();
            Items.Capacity = message.ReadByte();
            for (var i = 0; i < Items.Capacity; ++i)
            {
                Items.Add(message.ReadObjectInstance(client));
            }
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.OwnOffer);
            message.Write(PlayerName);
            var count = Math.Min(Items.Count, byte.MaxValue);
            message.Write((byte)count);
            for (var i = 0; i < count; ++i)
            {
                message.Write(Items[i]);
            }
        }
    }
}
