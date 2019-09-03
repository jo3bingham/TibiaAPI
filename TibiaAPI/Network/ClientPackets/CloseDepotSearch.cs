using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class CloseDepotSearch : ClientPacket
    {
        public CloseDepotSearch(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.CloseDepotSearch;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.CloseDepotSearch);
        }
    }
}
