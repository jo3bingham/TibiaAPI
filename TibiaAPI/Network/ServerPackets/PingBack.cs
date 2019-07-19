using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class PingBack : ServerPacket
    {
        public PingBack(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.PingBack;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.PingBack);
        }
    }
}
