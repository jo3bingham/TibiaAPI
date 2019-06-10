using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class Ping : ClientPacket
    {
        public Ping(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.Ping;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.Ping);
        }
    }
}
