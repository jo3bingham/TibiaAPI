using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class LoginError : ServerPacket
    {
        public string Text { get; set; }

        public LoginError(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.LoginError;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            Text = message.ReadString();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.LoginError);
            message.Write(Text);
        }
    }
}
