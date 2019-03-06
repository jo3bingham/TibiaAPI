using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class RequestShopOffers : ClientPacket
    {
        public string Category { get; set; }
        public string SearchText { get; set; }
        public string SubCategory { get; set; }

        public uint OfferId { get; set; }

        public ushort Unknown { get; set; }

        public byte ServiceType { get; set; }

        public RequestShopOffers(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.RequestShopOffers;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.RequestShopOffers)
            {
                return false;
            }

            ServiceType = message.ReadByte();
            if (Client.VersionNumber >= 11900000)
            {
                if (ServiceType == 2)
                {
                    Category = message.ReadString();
                    SubCategory = message.ReadString();
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

            // TODO: Figure out this unknown.
            Unknown = message.ReadUInt16();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.RequestShopOffers);
            message.Write(ServiceType);
            if (Client.VersionNumber >= 11900000)
            {
                if (ServiceType == 2)
                {
                    message.Write(Category);
                    message.Write(SubCategory);
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
            message.Write(Unknown);
        }
    }
}
