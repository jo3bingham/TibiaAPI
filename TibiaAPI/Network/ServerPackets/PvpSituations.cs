using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class PvpSituations : ServerPacket
    {
        public byte OpenPvpSituations { get; set; }

        public PvpSituations()
        {
            PacketType = ServerPacketType.PvpSituations;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.PvpSituations)
            {
                return false;
            }

            OpenPvpSituations = message.ReadByte();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.PvpSituations);
            message.Write(OpenPvpSituations);
        }
    }
}
