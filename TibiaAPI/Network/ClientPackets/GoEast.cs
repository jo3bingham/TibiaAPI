using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class GoEast : ClientPacket
    {
        public GoEast(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.GoEast;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.GoEast);
        }
    }
}
