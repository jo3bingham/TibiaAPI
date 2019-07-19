using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class RotateNorth : ClientPacket
    {
        public RotateNorth(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.RotateNorth;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.RotateNorth);
        }
    }
}
