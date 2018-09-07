using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class OfferDescription : ServerPacket
    {
        public string Description { get; set; }

        public uint OfferId { get; set; }

        public OfferDescription()
        {
            PacketType = ServerPacketType.OfferDescription;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.OfferDescription)
            {
                return false;
            }

            OfferId = message.ReadUInt32();
            Description = message.ReadString();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.OfferDescription);
            message.Write(OfferId);
            message.Write(Description);
        }
    }
}
