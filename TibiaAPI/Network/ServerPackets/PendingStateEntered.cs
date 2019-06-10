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

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            Client.Connection.ConnectionState = ConnectionState.Pending;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.PendingStateEntered);
        }
    }
}
