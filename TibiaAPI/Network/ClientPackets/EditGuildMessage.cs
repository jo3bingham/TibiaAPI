using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class EditGuildMessage : ClientPacket
    {
        public string GuildMotd { get; set; }

        public EditGuildMessage()
        {
            Type = ClientPacketType.EditGuildMessage;
        }

        public override bool ParseMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)Type)
            {
                return false;
            }

            GuildMotd = message.ReadString();
            return true;
        }

        public override void AppendToMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.EditGuildMessage);
            message.Write(GuildMotd);
        }
    }
}
