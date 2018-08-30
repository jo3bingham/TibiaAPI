using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class DailyRewardHistory : ClientPacket
    {
        public DailyRewardHistory()
        {
            Type = ClientPacketType.DailyRewardHistory;
        }

        public override bool ParseMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)Type)
            {
                return false;
            }

            return true;
        }

        public override void AppendToMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.DailyRewardHistory);
        }
    }
}
