using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class RightColumn : ServerPacket
    {
        private const int MapSizeX = 18;
        private const int MapSizeY = 14;

        public RightColumn()
        {
            PacketType = ServerPacketType.RightColumn;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.RightColumn)
            {
                return false;
            }

            var position = client.WorldMapStorage.GetPosition();
            position.X++;
            client.WorldMapStorage.SetPosition(position.X, position.Y, position.Z);
            client.WorldMapStorage.ScrollMap(-1, 0);
            message.ReadArea(client, (MapSizeX - 1), 0, (MapSizeX - 1), (MapSizeY - 1));
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.RightColumn);
            // TODO
        }
    }
}
