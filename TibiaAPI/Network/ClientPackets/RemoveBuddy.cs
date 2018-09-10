﻿using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class RemoveBuddy : ClientPacket
    {
        public uint PlayerId { get; set; }

        public RemoveBuddy()
        {
            PacketType = ClientPacketType.RemoveBuddy;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.RemoveBuddy)
            {
                return false;
            }

            PlayerId = message.ReadUInt32();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.RemoveBuddy);
            message.Write(PlayerId);
        }
    }
}
