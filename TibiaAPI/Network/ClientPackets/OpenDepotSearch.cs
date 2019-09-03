using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class OpenDepotSearch : ClientPacket
    {
        public OpenDepotSearch(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.OpenDepotSearch;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.OpenDepotSearch);
        }
    }
}
