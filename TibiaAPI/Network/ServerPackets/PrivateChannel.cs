﻿using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class PrivateChannel : ServerPacket
    {
        public string ChannelName { get; set; }

        public PrivateChannel()
        {
            PacketType = ServerPacketType.PrivateChannel;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.PrivateChannel)
            {
                return false;
            }

            ChannelName = message.ReadString();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.PrivateChannel);
            message.Write(ChannelName);
        }
    }
}
