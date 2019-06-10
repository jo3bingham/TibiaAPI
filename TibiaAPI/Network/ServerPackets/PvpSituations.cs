using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class PvpSituations : ServerPacket
    {
        public byte OpenPvpSituations { get; set; }

        public PvpSituations(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.PvpSituations;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            OpenPvpSituations = message.ReadByte();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.PvpSituations);
            message.Write(OpenPvpSituations);
        }
    }
}
