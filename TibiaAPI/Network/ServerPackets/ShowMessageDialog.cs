using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class ShowMessageDialog : ServerPacket
    {
        public string Text { get; set; }

        public byte Result { get; set; }

        public ShowMessageDialog(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.ShowMessageDialog;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            Result = message.ReadByte();
            Text = message.ReadString();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.ShowMessageDialog);
            message.Write(Result);
            message.Write(Text);
        }
    }
}
