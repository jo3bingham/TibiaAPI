using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class DailyRewardHistory : ClientPacket
    {
        public DailyRewardHistory()
        {
            PacketType = ClientPacketType.DailyRewardHistory;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.DailyRewardHistory)
            {
                return false;
            }

            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.DailyRewardHistory);
        }
    }
}
