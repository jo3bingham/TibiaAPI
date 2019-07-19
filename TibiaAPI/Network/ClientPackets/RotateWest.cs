using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class RotateWest : ClientPacket
    {
        public RotateWest(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.RotateWest;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.RotateWest);
        }
    }
}
