﻿using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class ClosedImbuingDialog : ClientPacket
    {
        public ClosedImbuingDialog()
        {
            PacketType = ClientPacketType.ClosedImbuingDialog;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.ClosedImbuingDialog)
            {
                return false;
            }

            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.ClosedImbuingDialog);
        }
    }
}
