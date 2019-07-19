using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class AcceptTrade : ClientPacket
    {
        public AcceptTrade(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.AcceptTrade;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.AcceptTrade);
        }
    }
}
