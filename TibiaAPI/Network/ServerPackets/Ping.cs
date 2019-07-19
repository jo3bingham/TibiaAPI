using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class Ping : ServerPacket
    {
        public Ping(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.Ping;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.Ping);
        }
    }
}
