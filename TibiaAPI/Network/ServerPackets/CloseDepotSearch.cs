using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class CloseDepotSearch : ServerPacket
    {
        public CloseDepotSearch(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.CloseDepotSearch;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.CloseDepotSearch);
        }
    }
}
