using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class CloseNpcTrade : ClientPacket
    {
        public CloseNpcTrade(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.CloseNpcTrade;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.CloseNpcTrade);
        }
    }
}
