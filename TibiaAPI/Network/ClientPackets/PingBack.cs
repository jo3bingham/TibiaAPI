using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class PingBack : ClientPacket
    {
        public PingBack(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.PingBack;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.PingBack);
        }
    }
}
