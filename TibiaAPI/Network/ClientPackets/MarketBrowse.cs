﻿using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class MarketBrowse : ClientPacket
    {
        public ushort ObjectId { get; set; }

        public MarketBrowse()
        {
            PacketType = ClientPacketType.MarketBrowse;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.MarketBrowse)
            {
                return false;
            }

            ObjectId = message.ReadUInt16();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.MarketBrowse);
            message.Write(ObjectId);
        }
    }
}
