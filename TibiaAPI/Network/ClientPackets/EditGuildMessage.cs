using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class EditGuildMessage : ClientPacket
    {
        public string GuildMotd { get; set; }

        public EditGuildMessage()
        {
            PacketType = ClientPacketType.EditGuildMessage;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.EditGuildMessage)
            {
                return false;
            }

            GuildMotd = message.ReadString();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.EditGuildMessage);
            message.Write(GuildMotd);
        }
    }
}
