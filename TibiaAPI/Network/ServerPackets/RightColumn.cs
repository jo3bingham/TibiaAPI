using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class RightColumn : Map
    {
        private const int MapSizeX = 18;
        private const int MapSizeY = 14;

        public RightColumn(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.RightColumn;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            var position = Client.WorldMapStorage.GetPosition();
            position.X++;
            Client.WorldMapStorage.SetPosition(position.X, position.Y, position.Z);
            Client.WorldMapStorage.ScrollMap(-1, 0);
            message.ReadArea((MapSizeX - 1), 0, (MapSizeX - 1), (MapSizeY - 1), Fields);
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.RightColumn);
            base.AppendToNetworkMessage(message);
        }
    }
}
