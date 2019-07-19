using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class CloseNpcTrade : ServerPacket
    {
        public CloseNpcTrade(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.CloseNpcTrade;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.CloseNpcTrade);
        }
    }
}
