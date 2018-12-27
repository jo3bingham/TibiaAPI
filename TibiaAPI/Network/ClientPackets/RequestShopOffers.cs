using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class RequestShopOffers : ClientPacket
    {
        public StoreServiceType ServiceType { get; set; }

        public string Category { get; set; }

        public uint OfferId { get; set; }

        public uint Unknown { get; set; }

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

            ServiceType = (StoreServiceType)message.ReadByte();
            if (ServiceType == StoreServiceType.Mounts)
            {
                OfferId = message.ReadUInt32();
            }
            else if (ServiceType == StoreServiceType.Premium)
            {
                Category = message.ReadString();
            }

            // TODO: Figure out this unknown.
            Unknown = (Client.VersionNumber >= 11900000) ? message.ReadUInt32() : message.ReadUInt16();
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
            if (Client.VersionNumber >= 11900000)
            {
                message.Write(Unknown);
            }
            else
            {
                message.Write((ushort)Unknown);
            }
        }
    }
}
