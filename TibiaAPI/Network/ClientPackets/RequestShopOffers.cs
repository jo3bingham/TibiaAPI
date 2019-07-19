using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class RequestShopOffers : ClientPacket
    {
        public string Category { get; set; }
        public string SearchText { get; set; }
        public string SubCategory { get; set; }

        public uint OfferId { get; set; }

        public byte CategoryDeepLink { get; set; }
        public byte DeeplinkSource { get; set; }
        public byte OfferDeeplink { get; set; }
        public byte ServiceType { get; set; }
        public byte SortOrder { get; set; }

        public RequestShopOffers(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.RequestShopOffers;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            ServiceType = message.ReadByte();
            if (Client.VersionNumber >= 11900000)
            {
                if (ServiceType == 1)
                {
                    CategoryDeepLink = message.ReadByte();
                }
                else if (ServiceType == 2)
                {
                    Category = message.ReadString();
                    SubCategory = message.ReadString();
                }
                else if (ServiceType == 3)
                {
                    OfferDeeplink = message.ReadByte();
                }
                else if (ServiceType == 4)
                {
                    OfferId = message.ReadUInt32();
                }
                else if (ServiceType == 5)
                {
                    SearchText = message.ReadString();
                }
            }
            else
            {
                if (ServiceType == (byte)StoreServiceType.Mounts)
                {
                    OfferId = message.ReadUInt32();
                }
                else if (ServiceType == (byte)StoreServiceType.Premium)
                {
                    Category = message.ReadString();
                }
            }

            SortOrder = message.ReadByte();
            DeeplinkSource = message.ReadByte();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.RequestShopOffers);
            message.Write(ServiceType);
            if (Client.VersionNumber >= 11900000)
            {
                if (ServiceType == 1)
                {
                    message.Write(CategoryDeepLink);
                }
                else if (ServiceType == 2)
                {
                    message.Write(Category);
                    message.Write(SubCategory);
                }
                else if (ServiceType == 3)
                {
                    message.Write(OfferDeeplink);
                }
                else if (ServiceType == 4)
                {
                    message.Write(OfferId);
                }
                else if (ServiceType == 5)
                {
                    message.Write(SearchText);
                }
            }
            else
            {
                if (ServiceType == (byte)StoreServiceType.Mounts)
                {
                    message.Write(OfferId);
                }
                else if (ServiceType == (byte)StoreServiceType.Premium)
                {
                    message.Write(Category);
                }
            }
            message.Write(SortOrder);
            message.Write(DeeplinkSource);
        }
    }
}
