using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class StoreButtonIndicators : ServerPacket
    {
        public bool IsNewBannerVisible { get; set; }
        public bool IsSaleBannerVisible { get; set; }

        public StoreButtonIndicators()
        {
            PacketType = ServerPacketType.StoreButtonIndicators;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.StoreButtonIndicators)
            {
                return false;
            }

            IsSaleBannerVisible = message.ReadBool();
            IsNewBannerVisible = message.ReadBool();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.StoreButtonIndicators);
            message.Write(IsSaleBannerVisible);
            message.Write(IsNewBannerVisible);
        }
    }
}
