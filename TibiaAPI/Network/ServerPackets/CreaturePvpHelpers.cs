using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class CreaturePvpHelpers : ServerPacket
    {
        public uint CreatureId { get; set; }

        public ushort PvpHelpers { get; set; }

        public CreaturePvpHelpers()
        {
            PacketType = ServerPacketType.CreaturePvpHelpers;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.CreaturePvpHelpers)
            {
                return false;
            }

            CreatureId = message.ReadUInt32();
            PvpHelpers = message.ReadUInt16();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.CreaturePvpHelpers);
            message.Write(CreatureId);
            message.Write(PvpHelpers);
        }
    }
}
