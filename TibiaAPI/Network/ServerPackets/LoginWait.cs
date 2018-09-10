using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class LoginWait : ServerPacket
    {
        public string Text { get; set; }

        public byte Delay { get; set; }

        public LoginWait()
        {
            PacketType = ServerPacketType.LoginWait;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.LoginWait)
            {
                return false;
            }

            Text = message.ReadString();
            Delay = message.ReadByte();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.LoginWait);
            message.Write(Text);
            message.Write(Delay);
        }
    }
}
