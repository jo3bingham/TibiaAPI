using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class GetOfferDescription : ClientPacket
    {
        public uint OfferId { get; set; }

        public GetOfferDescription()
        {
            Type = ClientPacketType.GetOfferDescription;
        }

        public override bool ParseMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)Type)
            {
                return false;
            }

            OfferId = message.ReadUInt32();
            return true;
        }

        public override void AppendToMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.GetOfferDescription);
            message.Write(OfferId);
        }
    }
}
