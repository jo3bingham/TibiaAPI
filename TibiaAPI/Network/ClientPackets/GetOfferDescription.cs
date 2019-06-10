using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class GetOfferDescription : ClientPacket
    {
        public uint OfferId { get; set; }

        public GetOfferDescription(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.GetOfferDescription;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            OfferId = message.ReadUInt32();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.GetOfferDescription);
            message.Write(OfferId);
        }
    }
}
