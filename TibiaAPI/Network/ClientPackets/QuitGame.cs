using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class QuitGame : ClientPacket
    {
        public QuitGame(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.QuitGame;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.QuitGame);
        }
    }
}
