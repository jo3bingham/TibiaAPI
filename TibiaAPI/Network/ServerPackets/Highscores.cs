using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class Highscores : ServerPacket
    {
        public Highscores(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.Highscores;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            // TODO
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            // TODO
            // message.Write((byte)ServerPacketType.Highscores);
        }
    }
}
