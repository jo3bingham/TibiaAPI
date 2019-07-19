using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class RotateEast : ClientPacket
    {
        public RotateEast(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.RotateEast;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.RotateEast);
        }
    }
}
