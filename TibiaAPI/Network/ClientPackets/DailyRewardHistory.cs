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

        public override bool ParseFromNetworkMessage(NetworkMessage message)
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
