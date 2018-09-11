using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class RequestShopOffers : ClientPacket
    {
        public StoreServiceType ServiceType { get; set; }

        public string Category { get; set; }

        public uint OfferId { get; set; }

        public ushort Unknown { get; set; }

        public RequestShopOffers()
        {
            PacketType = ClientPacketType.RequestShopOffers;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.RequestShopOffers)
            {
                return false;
            }

            ServiceType = (StoreServiceType)message.ReadByte();
            if (ServiceType == StoreServiceType.Mounts)
            {
                OfferId = message.ReadUInt32();
            }
            else if (ServiceType == StoreServiceType.Premium)
            {
                Category = message.ReadString();
            }

            Unknown = message.ReadUInt16();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.RequestShopOffers);
            message.Write((byte)ServiceType);
            if (ServiceType == StoreServiceType.Mounts)
            {
                message.Write(OfferId);
            }
            else if (ServiceType == StoreServiceType.Premium)
            {
                message.Write(Category);
            }
            message.Write(Unknown);
        }
    }
}
