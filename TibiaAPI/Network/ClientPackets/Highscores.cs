using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class Highscores : ClientPacket
    {
        public Highscores(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.Highscores;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            // TODO

            // 00 00 FF FF FF FF 00 00 01 00 14

            // FF FF FF FF may be vocation selection of `(all)`
            // 14 may be number of entries per page
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            // TODO
            // message.Write((byte)ClientPacketType.Highscores);
        }
    }
}
