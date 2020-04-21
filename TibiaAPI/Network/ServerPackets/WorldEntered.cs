using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class WorldEntered : ServerPacket
    {
        public WorldEntered(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.WorldEntered;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            if (Client.Connection.ConnectionState == ConnectionState.Pending)
            {
                Client.WorldMapStorage.ResetMap();
                Client.WorldMapStorage.SetPosition(0, 0, 0);
            }

            Client.Connection.ConnectionState = ConnectionState.Game;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.WorldEntered);
        }
    }
}
