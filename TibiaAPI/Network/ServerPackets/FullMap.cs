using OXGaming.TibiaAPI.Constants;
using OXGaming.TibiaAPI.Utilities;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class FullMap : Map
    {
        private const int MapSizeX = 18;
        private const int MapSizeY = 14;

        public Position Position { get; set; }

        public FullMap(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.FullMap;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.FullMap)
            {
                return false;
            }

            Position = message.ReadPosition();
            Client.WorldMapStorage.ResetMap();
            Client.WorldMapStorage.SetPosition(Position.X, Position.Y, Position.Z);
            message.ReadArea(Client, 0, 0, (MapSizeX - 1), (MapSizeY - 1), Fields);
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.FullMap);
            // TODO
        }
    }
}
