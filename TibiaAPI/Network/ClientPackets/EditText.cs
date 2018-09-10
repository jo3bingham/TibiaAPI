using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class EditText : ClientPacket
    {
        public string Text { get; set; }

        public uint WindowId { get; set; }

        public EditText()
        {
            PacketType = ClientPacketType.EditText;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.EditText)
            {
                return false;
            }

            WindowId = message.ReadUInt32();
            Text = message.ReadString();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.EditText);
            message.Write(WindowId);
            message.Write(Text);
        }
    }
}
