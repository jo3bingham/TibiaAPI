using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;
using OXGaming.TibiaAPI.Utilities;
using OXGaming.TibiaAPI.WorldMap;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class RightColumn : ServerPacket
    {
        private const int MapSizeX = 18;
        private const int MapSizeY = 14;

        public List<(Field, Position)> Fields { get; } = new List<(Field, Position)>();

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
            message.ReadArea(client, (MapSizeX - 1), 0, (MapSizeX - 1), (MapSizeY - 1), Fields);
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.RightColumn);
            // TODO
        }
    }
}
