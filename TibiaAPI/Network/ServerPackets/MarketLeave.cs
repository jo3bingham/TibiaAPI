using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class MarketLeave : ServerPacket
    {
        public MarketLeave(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.MarketLeave;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.MarketLeave);
        }
    }
}
