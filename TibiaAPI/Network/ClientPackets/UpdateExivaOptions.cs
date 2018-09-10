﻿using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class UpdateExivaOptions : ClientPacket
    {
        public UpdateExivaOptions()
        {
            PacketType = ClientPacketType.UpdateExivaOptions;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.UpdateExivaOptions)
            {
                return false;
            }

            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.UpdateExivaOptions);
        }
    }
}
