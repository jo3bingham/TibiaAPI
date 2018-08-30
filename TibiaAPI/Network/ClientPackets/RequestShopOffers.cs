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
            Type = ClientPacketType.RequestShopOffers;
        }

        public override bool ParseMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)Type)
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

        public override void AppendToMessage(NetworkMessage message)
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
