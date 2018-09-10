﻿using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class LookTrade : ClientPacket
    {
        public byte Index { get; set; }
        public byte Side { get; set; }

        public LookTrade()
        {
            PacketType = ClientPacketType.LookTrade;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.LookTrade)
            {
                return false;
            }

            Side = message.ReadByte();
            Index = message.ReadByte();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.LookTrade);
            message.Write(Side);
            message.Write(Index);
        }
    }
}
