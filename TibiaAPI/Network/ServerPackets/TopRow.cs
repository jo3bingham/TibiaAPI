using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class TopRow : Map
    {
        private const int MapSizeX = 18;

        public TopRow(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.TopRow;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            var position = Client.WorldMapStorage.GetPosition();
            position.Y--;
            Client.WorldMapStorage.SetPosition(position.X, position.Y, position.Z);
            Client.WorldMapStorage.ScrollMap(0, 1);
            message.ReadArea(0, 0, (MapSizeX - 1), 0, Fields);
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.TopRow);
            base.AppendToNetworkMessage(message);
        }
    }
}
