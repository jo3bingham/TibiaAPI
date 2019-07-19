using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class GoSouth : ClientPacket
    {
        public GoSouth(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.GoSouth;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.GoSouth);
        }
    }
}
