using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class LoginAdvice : ServerPacket
    {
        public string Text { get; set; }

        public LoginAdvice()
        {
            PacketType = ServerPacketType.LoginAdvice;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.LoginAdvice)
            {
                return false;
            }

            Text = message.ReadString();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.LoginAdvice);
            message.Write(Text);
        }
    }
}
