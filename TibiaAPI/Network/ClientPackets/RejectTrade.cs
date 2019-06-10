using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class RejectTrade : ClientPacket
    {
        public RejectTrade(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.RejectTrade;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.RejectTrade);
        }
    }
}
