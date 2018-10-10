using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class TopRow : Map
    {
        private const int MapSizeX = 18;

        public TopRow()
        {
            PacketType = ServerPacketType.TopRow;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.TopRow)
            {
                return false;
            }

            var position = client.WorldMapStorage.GetPosition();
            position.Y--;
            client.WorldMapStorage.SetPosition(position.X, position.Y, position.Z);
            client.WorldMapStorage.ScrollMap(0, 1);
            message.ReadArea(client, 0, 0, (MapSizeX - 1), 0, Fields);
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.TopRow);
            // TODO
        }
    }
}
