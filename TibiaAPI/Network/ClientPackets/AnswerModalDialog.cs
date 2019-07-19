using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class AnswerModalDialog : ClientPacket
    {
        public uint WindowId { get; set; }

        public byte ButtonId { get; set; }
        public byte Choice { get; set; }

        public AnswerModalDialog(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.AnswerModalDialog;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            WindowId = message.ReadUInt32();
            ButtonId = message.ReadByte();
            Choice = message.ReadByte();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.AnswerModalDialog);
            message.Write(WindowId);
            message.Write(ButtonId);
            message.Write(Choice);
        }
    }
}
