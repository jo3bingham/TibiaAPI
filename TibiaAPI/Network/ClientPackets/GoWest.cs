using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class GoWest : ClientPacket
    {
        public GoWest(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.GoWest;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.GoWest);
        }
    }
}
