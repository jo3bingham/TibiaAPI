using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class EditGuildMessage : ServerPacket
    {
        public string Text { get; set; }

        public EditGuildMessage(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.EditGuildMessage;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            Text = message.ReadString();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.EditGuildMessage);
            message.Write(Text);
        }
    }
}
