using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class GuildMessage : ClientPacket
    {
        public GuildMessage(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.GuildMessage;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.GuildMessage);
        }
    }
}
