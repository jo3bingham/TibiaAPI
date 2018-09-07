using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class RightColumn : ServerPacket
    {
        public RightColumn()
        {
            PacketType = ServerPacketType.RightColumn;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.RightColumn)
            {
                return false;
            }

            //var fields = new System.Collections.Generic.List<WorldMap.Field>();

            //var position = client.WorldMapStorage.GetPosition();
            //position.X++;
            //client.WorldMapStorage.SetPosition(position.X, position.Y, position.Z);
            //client.WorldMapStorage.ScrollMap(-1, 0);
            //ReadArea(message, (MapSizeX - 1), 0, (MapSizeX - 1), (MapSizeY - 1), fields);
            //client.WorldMapStorage.OnMapUpdated(fields);
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.RightColumn);
            // TODO
        }
    }
}
