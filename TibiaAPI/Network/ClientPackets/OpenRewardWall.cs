using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class OpenRewardWall : ClientPacket
    {
        public OpenRewardWall(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.OpenRewardWall;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.OpenRewardWall);
        }
    }
}
