using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class ConnectionPingBack : ClientPacket
    {
        public ConnectionPingBack()
        {
            PacketType = ClientPacketType.ConnectionPingBack;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.ConnectionPingBack)
            {
                return false;
            }

            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.AddSequenceNumber = false;
            message.Write((byte)ClientPacketType.ConnectionPingBack);
        }
    }
}
