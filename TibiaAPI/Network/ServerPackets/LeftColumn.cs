using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class LeftColumn : Map
    {
        private const int MapSizeY = 14;

        public LeftColumn(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.LeftColumn;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            var position = Client.WorldMapStorage.GetPosition();
            position.X--;
            Client.WorldMapStorage.SetPosition(position.X, position.Y, position.Z);
            Client.WorldMapStorage.ScrollMap(1, 0);
            message.ReadArea(0, 0, 0, (MapSizeY - 1), Fields);
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.LeftColumn);
            base.AppendToNetworkMessage(message);
        }
    }
}
