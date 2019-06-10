using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class ConnectionPingBack : ClientPacket
    {
        public ConnectionPingBack(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.ConnectionPingBack;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.ConnectionPingBack);
        }
    }
}
