using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class PendingStateEntered : ServerPacket
    {
        public PendingStateEntered(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.PendingStateEntered;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.PendingStateEntered)
            {
                return false;
            }

            Client.Proxy.ConnectionState = ConnectionState.Pending;
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.PendingStateEntered);
        }
    }
}
