using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class BottomRow : Map
    {
        private const int MapSizeX = 18;
        private const int MapSizeY = 14;

        public BottomRow(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.BottomRow;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            var position = Client.WorldMapStorage.GetPosition();
            position.Y++;
            Client.WorldMapStorage.SetPosition(position.X, position.Y, position.Z);
            Client.WorldMapStorage.ScrollMap(0, -1);
            message.ReadArea(0, (MapSizeY - 1), (MapSizeX - 1), (MapSizeY - 1), Fields);
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.BottomRow);
            base.AppendToNetworkMessage(message);
        }
    }
}
