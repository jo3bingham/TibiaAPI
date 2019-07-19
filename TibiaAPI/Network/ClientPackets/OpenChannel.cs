using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class OpenChannel : ClientPacket
    {
        public OpenChannel(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.OpenChannel;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.OpenChannel);
        }
    }
}
