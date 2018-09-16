using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class MonsterCyclopediaNewDetails : ServerPacket
    {
        public ushort RaceId { get; set; }

        public MonsterCyclopediaNewDetails()
        {
            PacketType = ServerPacketType.MonsterCyclopediaNewDetails;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
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
