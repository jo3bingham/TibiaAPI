using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class RotateSouth : ClientPacket
    {
        public RotateSouth(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.RotateSouth;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.RotateSouth);
        }
    }
}
