using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class BuyIngameShopOffer : ClientPacket
    {
        public StoreServiceType ServiceType { get; set; }

        public string DesiredCharacterName { get; set; }

        public uint OfferId { get; set; }

        public BuyIngameShopOffer(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.BuyIngameShopOffer;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            OfferId = message.ReadUInt32();
            ServiceType = (StoreServiceType)message.ReadByte();
            if (ServiceType == StoreServiceType.CharacterNameChange)
            {
                DesiredCharacterName = message.ReadString();
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.BuyIngameShopOffer);
            message.Write(OfferId);
            message.Write((byte)ServiceType);
            if (ServiceType == StoreServiceType.CharacterNameChange)
            {
                message.Write(DesiredCharacterName);
            }
        }
    }
}
