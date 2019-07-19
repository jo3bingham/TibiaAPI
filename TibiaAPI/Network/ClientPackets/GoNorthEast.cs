using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class GoNorthEast : ClientPacket
    {
        public GoNorthEast(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.GoNorthEast;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.GoNorthEast);
        }
    }
}
