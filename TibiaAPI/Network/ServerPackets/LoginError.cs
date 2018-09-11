using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class LoginError : ServerPacket
    {
        public string Text { get; set; }

        public LoginError()
        {
            PacketType = ServerPacketType.LoginError;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.LoginError)
            {
                return false;
            }

            Text = message.ReadString();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.LoginError);
            message.Write(Text);
        }
    }
}
