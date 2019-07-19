using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class StoreButtonIndicators : ServerPacket
    {
        public bool IsNewBannerVisible { get; set; }
        public bool IsSaleBannerVisible { get; set; }

        public StoreButtonIndicators(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.StoreButtonIndicators;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            IsSaleBannerVisible = message.ReadBool();
            IsNewBannerVisible = message.ReadBool();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.StoreButtonIndicators);
            message.Write(IsSaleBannerVisible);
            message.Write(IsNewBannerVisible);
        }
    }
}
