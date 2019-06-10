using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class LoginAdvice : ServerPacket
    {
        public string Text { get; set; }

        public LoginAdvice(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.LoginAdvice;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            Text = message.ReadString();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.LoginAdvice);
            message.Write(Text);
        }
    }
}
