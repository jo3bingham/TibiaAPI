﻿using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class OpenTransactionHistory : ClientPacket
    {
        public byte EntriesPerPage { get; set; }

        public OpenTransactionHistory()
        {
            PacketType = ClientPacketType.OpenTransactionHistory;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.OpenTransactionHistory)
            {
                return false;
            }

            EntriesPerPage = message.ReadByte();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.OpenTransactionHistory);
            message.Write(EntriesPerPage);
        }
    }
}
