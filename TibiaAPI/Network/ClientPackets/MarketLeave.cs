using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class MarketLeave : ClientPacket
    {
        public MarketLeave(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.MarketLeave;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.MarketLeave);
        }
    }
}
