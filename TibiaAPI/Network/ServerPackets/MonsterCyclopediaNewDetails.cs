using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class MonsterCyclopediaNewDetails : ServerPacket
    {
        public ushort RaceId { get; set; }

        public MonsterCyclopediaNewDetails(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.MonsterCyclopediaNewDetails;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.MonsterCyclopediaNewDetails)
            {
                return false;
            }

            RaceId = message.ReadUInt16();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.MonsterCyclopediaNewDetails);
            message.Write(RaceId);
        }
    }
}
