using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class GetChannels : ClientPacket
    {
        public GetChannels(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.GetChannels;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.GetChannels);
        }
    }
}
