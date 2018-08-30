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
            Type = ClientPacketType.BuyIngameShopOffer;
        }

        public override bool ParseMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)Type)
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

        public override void AppendToMessage(NetworkMessage message)
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
