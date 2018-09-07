using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class DailyRewardCollectionState : ServerPacket
    {
        public byte CollectionState { get; set; }

        public DailyRewardCollectionState()
        {
            PacketType = ServerPacketType.DailyRewardCollectionState;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.DailyRewardCollectionState)
            {
                return false;
            }

            CollectionState = message.ReadByte();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.DailyRewardCollectionState);
            message.Write(CollectionState);
        }
    }
}
