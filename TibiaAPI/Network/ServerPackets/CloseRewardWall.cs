using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class CloseRewardWall : ServerPacket
    {
        public CloseRewardWall(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.CloseRewardWall;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.CloseRewardWall);
        }
    }
}
