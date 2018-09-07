using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class MonsterCyclopediaNewDetails : ServerPacket
    {
        // TODO
        public byte[] Unknown { get; private set; } = new byte[3];

        public ushort RaceId { get; set; }

        public MonsterCyclopediaNewDetails()
        {
            PacketType = ServerPacketType.MonsterCyclopediaNewDetails;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.MonsterCyclopediaNewDetails)
            {
                return false;
            }

            RaceId = message.ReadUInt16();
            // TODO
            Unknown = message.ReadBytes(3);
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.MonsterCyclopediaNewDetails);
            message.Write(RaceId);
            // TODO
        }
    }
}
