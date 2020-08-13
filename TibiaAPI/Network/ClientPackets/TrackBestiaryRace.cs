using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class TrackBestiaryRace : ClientPacket
    {
        public byte UnknownByte1 { get; set; }

        public ushort RaceId { get; set; }

        public TrackBestiaryRace(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.TrackBestiaryRace;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            RaceId = message.ReadUInt16();
            // TODO
            UnknownByte1 = message.ReadByte();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.TrackBestiaryRace);
            message.Write(RaceId);
            message.Write(UnknownByte1);
        }
    }
}
