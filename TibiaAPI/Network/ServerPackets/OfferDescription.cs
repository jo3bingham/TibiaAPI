using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class OfferDescription : ServerPacket
    {
        public string Description { get; set; }

        public uint OfferId { get; set; }

        public OfferDescription(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.OfferDescription;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            OfferId = message.ReadUInt32();
            Description = message.ReadString();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.OfferDescription);
            message.Write(OfferId);
            message.Write(Description);
        }
    }
}
