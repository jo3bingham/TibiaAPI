using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class Stop : ClientPacket
    {
        public Stop(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.Stop;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.Stop);
        }
    }
}
