using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class GoSouthEast : ClientPacket
    {
        public GoSouthEast(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.GoSouthEast;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.GoSouthEast);
        }
    }
}
