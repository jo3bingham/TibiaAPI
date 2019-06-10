using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class MarketStatistics : ClientPacket
    {
        public MarketStatistics(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.MarketStatistics;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.MarketStatistics);
        }
    }
}
