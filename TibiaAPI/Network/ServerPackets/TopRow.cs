using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class TopRow : ServerPacket
    {
        public TopRow()
        {
            PacketType = ServerPacketType.TopRow;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.TopRow)
            {
                return false;
            }


            //var fields = new System.Collections.Generic.List<WorldMap.Field>();

            //var position = client.WorldMapStorage.GetPosition();
            //position.Y--;
            //client.WorldMapStorage.SetPosition(position.X, position.Y, position.Z);
            //client.WorldMapStorage.ScrollMap(0, 1);
            //ReadArea(message, 0, 0, (MapSizeX - 1), 0, fields);
            //client.WorldMapStorage.OnMapUpdated(fields);
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.TopRow);
            // TODO
        }
    }
}
