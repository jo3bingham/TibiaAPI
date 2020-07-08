using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class TrackBestiaryRace : ClientPacket
    {
        public ushort RaceId { get; set; }
        public TrackBestiaryRace(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.TrackBestiaryRace;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            RaceId = message.ReadUInt16();
            message.ReadByte(); // TODO
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            // TODO
            // message.Write((byte)ClientPacketType.TrackBestiaryRace);
            // message.Write(RaceId);
        }
    }
}
