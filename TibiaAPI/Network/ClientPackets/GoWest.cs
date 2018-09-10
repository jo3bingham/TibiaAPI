﻿using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class GoWest : ClientPacket
    {
        public GoWest()
        {
            PacketType = ClientPacketType.GoWest;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.GoWest)
            {
                return false;
            }

            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.GoWest);
        }
    }
}
