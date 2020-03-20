using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class TrackBestiaryRace : ClientPacket
    {
        public ushort RaceId { get; set; }

        public byte Unknown { get; set; }

        public TrackBestiaryRace(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.TrackBestiaryRace;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            RaceId = message.ReadUInt16();
            Unknown = message.ReadByte();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.TrackBestiaryRace);
            message.Write(RaceId);
            message.Write(Unknown);
        }
    }
}
