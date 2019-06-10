﻿using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class MonsterCyclopediaNewDetails : ServerPacket
    {
        private byte[] _unknown = new byte[3];

        public ushort RaceId { get; set; }

        public MonsterCyclopediaNewDetails(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.MonsterCyclopediaNewDetails;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            RaceId = message.ReadUInt16();

            if (Client.VersionNumber < 11900000 && Client.VersionNumber > 11596424)
            {
                _unknown = message.ReadBytes(3);
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.MonsterCyclopediaNewDetails);
            message.Write(RaceId);
            if (Client.VersionNumber < 11900000 && Client.VersionNumber > 11596424)
            {
                message.Write(_unknown);
            }
        }
    }
}
