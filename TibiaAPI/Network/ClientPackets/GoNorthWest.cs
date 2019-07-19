using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class GoNorthWest : ClientPacket
    {
        public GoNorthWest(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.GoNorthWest;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.GoNorthWest);
        }
    }
}
