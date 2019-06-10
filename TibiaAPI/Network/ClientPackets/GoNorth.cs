using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class GoNorth : ClientPacket
    {
        public GoNorth(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.GoNorth;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.GoNorth);
        }
    }
}
