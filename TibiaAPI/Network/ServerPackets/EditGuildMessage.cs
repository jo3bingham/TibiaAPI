using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class EditGuildMessage : ServerPacket
    {
        public string Text { get; set; }

        public EditGuildMessage()
        {
            PacketType = ServerPacketType.EditGuildMessage;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.EditGuildMessage)
            {
                return false;
            }

            Text = message.ReadString();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.EditGuildMessage);
            message.Write(Text);
        }
    }
}
