using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class Login : ClientPacket
    {
        public Login()
        {
            Type = ClientPacketType.Login;
        }

        public override Packet FromNetworkMessage(NetworkMessage message)
        {
            Type = (ClientPacketType)message.ReadByte();
            if (Type != ClientPacketType.Login)
            {
                return null;
            }

            var packet = new Login();
            return packet;
        }

        public override NetworkMessage ToNetworkMessage()
        {
            var message = new NetworkMessage();
            message.Write((byte)Type);
            return message;
        }
    }
}
