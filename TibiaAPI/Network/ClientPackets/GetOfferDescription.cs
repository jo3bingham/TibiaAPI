using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class GetOfferDescription : ClientPacket
    {
        public uint OfferId { get; set; }

        public GetOfferDescription()
        {
            PacketType = ClientPacketType.GetOfferDescription;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.GetOfferDescription)
            {
                return false;
            }

            OfferId = message.ReadUInt32();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.GetOfferDescription);
            message.Write(OfferId);
        }
    }
}
