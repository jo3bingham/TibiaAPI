using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class DailyRewardHistory : ClientPacket
    {
        public DailyRewardHistory(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.DailyRewardHistory;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.DailyRewardHistory);
        }
    }
}
