﻿using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class LoginChallenge : ServerPacket
    {
        public uint Timestamp { get; set; }

        public byte Random { get; set; }

        public LoginChallenge()
        {
            PacketType = ServerPacketType.LoginChallenge;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.LoginChallenge)
            {
                return false;
            }

            Timestamp = message.ReadUInt32();
            Random = message.ReadByte();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.LoginChallenge);
            message.Write(Timestamp);
            message.Write(Random);
        }
    }
}
