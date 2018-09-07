using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class Login : ClientPacket
    {
        public Login()
        {
            PacketType = ClientPacketType.Login;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.Login)
            {
                return false;
            }

            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.Login);
        }
    }
}
