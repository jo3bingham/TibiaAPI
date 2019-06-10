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

        public MarketCreate(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.MarketCreate;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            OfferType = (MarketOfferType)message.ReadByte();
            ObjectId = message.ReadUInt16();
            Amount = message.ReadUInt16();
            PiecePrice = message.ReadUInt32();
            IsAnonymous = message.ReadBool();
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
