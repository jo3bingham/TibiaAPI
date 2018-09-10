using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class ShowMessageDialog : ServerPacket
    {
        public string Text { get; set; }

        public byte Result { get; set; }

        public ShowMessageDialog()
        {
            PacketType = ServerPacketType.ShowMessageDialog;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.ShowMessageDialog)
            {
                return false;
            }

            Result = message.ReadByte();
            Text = message.ReadString();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.ShowMessageDialog);
            message.Write(Result);
            message.Write(Text);
        }
    }
}
