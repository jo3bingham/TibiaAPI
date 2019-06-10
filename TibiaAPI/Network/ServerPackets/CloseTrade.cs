using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class CloseTrade : ServerPacket
    {
        public CloseTrade(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.CloseTrade;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.CloseTrade);
        }
    }
}
