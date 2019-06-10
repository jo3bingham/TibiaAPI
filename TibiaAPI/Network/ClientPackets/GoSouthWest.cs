using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class GoSouthWest : ClientPacket
    {
        public GoSouthWest(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.GoSouthWest;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.GoSouthWest);
        }
    }
}
