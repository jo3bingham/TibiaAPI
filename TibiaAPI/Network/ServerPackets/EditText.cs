using OXGaming.TibiaAPI.Appearances;
using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class EditText : ServerPacket
    {
        public ObjectInstance Window { get; set; }

        public string Author { get; set; }
        public string Date { get; set; }
        public string Text { get; set; }

        public uint WindowId { get; set; }

        public ushort MaxTextLength { get; set; }

        public byte Unknown { get; set; }

        public EditText(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.EditText;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            WindowId = message.ReadUInt32();
            Window = message.ReadObjectInstance();
            MaxTextLength = message.ReadUInt16();
            Text = message.ReadString();
            Author = message.ReadString();
            if (Client.VersionNumber >= 125010109)
            {
                Unknown = message.ReadByte();
            }
            Date = message.ReadString();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.EditText);
            message.Write(WindowId);
            message.Write(Window);
            message.Write(MaxTextLength);
            message.Write(Text);
            message.Write(Author);
            if (Client.VersionNumber >= 125010109)
            {
                message.Write(Unknown);
            }
            message.Write(Date);
        }
    }
}
