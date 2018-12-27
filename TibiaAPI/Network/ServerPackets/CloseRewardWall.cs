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

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.CloseRewardWall)
            {
                return false;
            }

            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.CloseRewardWall);
        }
    }
}
