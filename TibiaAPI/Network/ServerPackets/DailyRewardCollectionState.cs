using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class DailyRewardCollectionState : ServerPacket
    {
        public byte CollectionState { get; set; }

        public DailyRewardCollectionState(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.DailyRewardCollectionState;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            CollectionState = message.ReadByte();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.DailyRewardCollectionState);
            message.Write(CollectionState);
        }
    }
}
