using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class LoginWait : ServerPacket
    {
        public string Text { get; set; }

        public byte Delay { get; set; }

        public LoginWait(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.LoginWait;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            Text = message.ReadString();
            Delay = message.ReadByte();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.LoginWait);
            message.Write(Text);
            message.Write(Delay);
        }
    }
}
