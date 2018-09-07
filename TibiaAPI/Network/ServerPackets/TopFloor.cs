using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class TopFloor : ServerPacket
    {
        public TopFloor()
        {
            PacketType = ServerPacketType.TopFloor;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.TopFloor)
            {
                return false;
            }

            //var fields = new System.Collections.Generic.List<WorldMap.Field>();

            //var position = client.WorldMapStorage.GetPosition();
            //position.X++;
            //position.Y++;
            //position.Z--;

            //client.WorldMapStorage.SetPosition(position.X, position.Y, position.Z);

            //if (position.Z > GroundLayer)
            //{
            //    client.WorldMapStorage.ScrollMap(0, 0, -1);
            //    ReadFloor(message, (2 * UndergroundLayer), 0, fields);
            //}
            //else if (position.Z == GroundLayer)
            //{
            //    client.WorldMapStorage.ScrollMap(0, 0, -(UndergroundLayer + 1));

            //    var numberOfTilesToSkip = 0;
            //    var floorNumber = UndergroundLayer;
            //    while (floorNumber <= GroundLayer)
            //    {
            //        numberOfTilesToSkip = ReadFloor(message, floorNumber, numberOfTilesToSkip, fields);
            //        floorNumber++;
            //    }
            //}

            //client.WorldMapStorage.OnMapUpdated(fields);
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.TopFloor);
            // TODO
        }
    }
}
