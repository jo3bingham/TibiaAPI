using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class BottomRow : ServerPacket
    {
        private const int MapSizeX = 18;
        private const int MapSizeY = 14;

        public BottomRow()
        {
            PacketType = ServerPacketType.BottomRow;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.BottomRow)
            {
                return false;
            }

            var position = client.WorldMapStorage.GetPosition();
            position.Y++;
            client.WorldMapStorage.SetPosition(position.X, position.Y, position.Z);
            client.WorldMapStorage.ScrollMap(0, -1);
            message.ReadArea(client, 0, (MapSizeY - 1), (MapSizeX - 1), (MapSizeY - 1));
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.BottomRow);
            // TODO
        }
    }
}
