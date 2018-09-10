using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;
using OXGaming.TibiaAPI.Utilities;
using OXGaming.TibiaAPI.WorldMap;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class LeftColumn : ServerPacket
    {
        private const int MapSizeY = 14;

        public List<(Field, Position)> Fields { get; } = new List<(Field, Position)>();

        public LeftColumn()
        {
            PacketType = ServerPacketType.LeftColumn;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.LeftColumn)
            {
                return false;
            }

            var position = client.WorldMapStorage.GetPosition();
            position.X--;
            client.WorldMapStorage.SetPosition(position.X, position.Y, position.Z);
            client.WorldMapStorage.ScrollMap(1, 0);
            message.ReadArea(client, 0, 0, 0, (MapSizeY - 1), Fields);
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.LeftColumn);
            // TODO
        }
    }
}
