using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class AnswerModalDialog : ClientPacket
    {
        public uint WindowId { get; set; }

        public byte ButtonId { get; set; }
        public byte Choice { get; set; }

        public AnswerModalDialog()
        {
            PacketType = ClientPacketType.AnswerModalDialog;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.AnswerModalDialog)
            {
                return false;
            }

            WindowId = message.ReadUInt32();
            ButtonId = message.ReadByte();
            Choice = message.ReadByte();
            return true;
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
