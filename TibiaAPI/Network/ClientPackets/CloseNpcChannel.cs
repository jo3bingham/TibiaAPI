using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class CloseNpcChannel : ClientPacket
    {
        public CloseNpcChannel(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.CloseNpcChannel;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.CloseNpcChannel);
        }
    }
}
