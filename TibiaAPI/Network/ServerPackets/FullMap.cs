using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;
using OXGaming.TibiaAPI.Utilities;
using OXGaming.TibiaAPI.WorldMap;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class FullMap : ServerPacket
    {
        private const int MapSizeX = 18;
        private const int MapSizeY = 14;

        public List<(Field, Position)> Fields { get; } = new List<(Field, Position)>();

        public Position Position { get; set; }

        public FullMap()
        {
            PacketType = ServerPacketType.FullMap;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.FullMap)
            {
                return false;
            }

            Position = message.ReadPosition();
            client.WorldMapStorage.ResetMap();
            client.WorldMapStorage.SetPosition(Position.X, Position.Y, Position.Z);
            message.ReadArea(client, 0, 0, (MapSizeX - 1), (MapSizeY - 1), Fields);
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.FullMap);
            // TODO
        }
    }
}
