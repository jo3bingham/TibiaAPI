using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class EditGuildMessage : ClientPacket
    {
        public string GuildMotd { get; set; }

        public EditGuildMessage(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.EditGuildMessage;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            GuildMotd = message.ReadString();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.EditGuildMessage);
            message.Write(GuildMotd);
        }
    }
}
