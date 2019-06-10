using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class EditText : ClientPacket
    {
        public string Text { get; set; }

        public uint WindowId { get; set; }

        public EditText(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.EditText;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            WindowId = message.ReadUInt32();
            Text = message.ReadString();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.EditText);
            message.Write(WindowId);
            message.Write(Text);
        }
    }
}
