using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class MarketCreate : ClientPacket
    {
        public MarketOfferType OfferType { get; set; }

        public uint PiecePrice { get; set; }

        public ushort Amount { get; set; }
        public ushort ObjectId { get; set; }

        public bool IsAnonymous { get; set; }

        public MarketCreate()
        {
            PacketType = ClientPacketType.MarketCreate;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.MarketCreate)
            {
                return false;
            }

            OfferType = (MarketOfferType)message.ReadByte();
            ObjectId = message.ReadUInt16();
            Amount = message.ReadUInt16();
            PiecePrice = message.ReadUInt32();
            IsAnonymous = message.ReadBool();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.MarketCreate);
            message.Write((byte)OfferType);
            message.Write(ObjectId);
            message.Write(Amount);
            message.Write(PiecePrice);
            message.Write(IsAnonymous);
        }
    }
}
