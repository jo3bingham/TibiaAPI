using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class BuyIngameShopOffer : ClientPacket
    {
        public StoreServiceType ServiceType { get; set; }

        public string DesiredCharacterName { get; set; }

        public uint OfferId { get; set; }

        public BuyIngameShopOffer()
        {
            PacketType = ClientPacketType.BuyIngameShopOffer;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.BuyIngameShopOffer)
            {
                return false;
            }

            OfferId = message.ReadUInt32();
            ServiceType = (StoreServiceType)message.ReadByte();
            if (ServiceType == StoreServiceType.CharacterNameChange)
            {
                DesiredCharacterName = message.ReadString();
            }
            return true;
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
