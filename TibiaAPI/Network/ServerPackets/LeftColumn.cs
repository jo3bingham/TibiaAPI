using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class LeftColumn : MapBase
    {
        public LeftColumn()
        {
            PacketType = ServerPacketType.LeftColumn;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.LeftColumn)
            {
                return false;
            }

            //var fields = new System.Collections.Generic.List<WorldMap.Field>();

            //var position = client.WorldMapStorage.GetPosition();
            //position.X--;
            //client.WorldMapStorage.SetPosition(position.X, position.Y, position.Z);
            //client.WorldMapStorage.ScrollMap(1, 0);
            //ReadArea(message, 0, 0, 0, (MapSizeY - 1), fields);
            //client.WorldMapStorage.OnMapUpdated(fields);
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.LeftColumn);
            // TODO
        }
    }
}
