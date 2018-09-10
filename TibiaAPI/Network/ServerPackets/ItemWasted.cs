﻿using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class ItemWasted : ServerPacket
    {
        public ushort ItemId { get; set; }

        public ItemWasted()
        {
            PacketType = ServerPacketType.ItemWasted;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.ItemWasted)
            {
                return false;
            }

            ItemId = message.ReadUInt16();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.ItemWasted);
            message.Write(ItemId);
        }
    }
}
