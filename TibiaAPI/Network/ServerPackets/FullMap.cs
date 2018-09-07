using OXGaming.TibiaAPI.Constants;
using OXGaming.TibiaAPI.Utilities;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class FullMap : MapBase
    {
        public Position Position { get; set; }

        public FullMap()
        {
            PacketType = ServerPacketType.FullMap;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.FullMap)
            {
                return false;
            }

            // TODO
            //var fields = new System.Collections.Generic.List<WorldMap.Field>();

            //Position = message.ReadPosition();
            //client.WorldMapStorage.ResetMap();
            //client.WorldMapStorage.SetPosition(Position.X, Position.Y, Position.Z);
            //ReadArea(message, 0, 0, (MapSizeX - 1), (MapSizeY - 1), fields);
            //client.WorldMapStorage.OnMapUpdated(fields);
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.FullMap);
            // TODO
        }
    }
}
