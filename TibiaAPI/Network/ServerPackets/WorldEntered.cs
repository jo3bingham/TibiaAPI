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

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.WorldEntered)
            {
                return false;
            }

            if (Client.Proxy.ConnectionState == ConnectionState.Pending)
            {
                Client.CreatureStorage.Reset();
                Client.WorldMapStorage.ResetMap();
                Client.WorldMapStorage.SetPosition(0, 0, 0);
            }

            Client.Proxy.ConnectionState = ConnectionState.Game;
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.WorldEntered);
        }
    }
}
