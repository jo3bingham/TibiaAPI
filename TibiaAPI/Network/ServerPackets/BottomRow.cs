using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class BottomRow : MapBase
    {
        public BottomRow()
        {
            PacketType = ServerPacketType.BottomRow;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.BottomRow)
            {
                return false;
            }

            // TODO
            //var fields = new System.Collections.Generic.List<WorldMap.Field>();

            //var position = client.WorldMapStorage.GetPosition();
            //position.Y++;
            //client.WorldMapStorage.SetPosition(position.X, position.Y, position.Z);
            //client.WorldMapStorage.ScrollMap(0, -1);
            //ReadArea(message, 0, (MapSizeY - 1), (MapSizeX - 1), (MapSizeY - 1), fields);
            //client.WorldMapStorage.OnMapUpdated(fields);
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.BottomRow);
            // TODO
        }
    }
}
