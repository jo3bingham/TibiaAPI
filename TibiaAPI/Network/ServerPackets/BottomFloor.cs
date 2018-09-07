using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class BottomFloor : MapBase
    {
        public BottomFloor()
        {
            PacketType = ServerPacketType.BottomFloor;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.BottomFloor)
            {
                return false;
            }

            //var fields = new System.Collections.Generic.List<WorldMap.Field>();

            //var position = client.WorldMapStorage.GetPosition();
            //position.X--;
            //position.Y--;
            //position.Z++;

            //client.WorldMapStorage.SetPosition(position.X, position.Y, position.Z);

            //if (position.Z > (GroundLayer + 1))
            //{
            //    client.WorldMapStorage.ScrollMap(0, 0, 1);

            //    if (position.Z <= (MapMaxZ - UndergroundLayer))
            //    {
            //        ReadFloor(message, 0, 0, fields);
            //    }
            //}
            //else if (position.Z == (GroundLayer + 1))
            //{
            //    client.WorldMapStorage.ScrollMap(0, 0, (UndergroundLayer + 1));

            //    var numberOfTilesToSkip = 0;
            //    var floorNumber = UndergroundLayer;
            //    while (floorNumber >= 0)
            //    {
            //        numberOfTilesToSkip = ReadFloor(message, floorNumber, numberOfTilesToSkip, fields);
            //        floorNumber--;
            //    }
            //}

            //client.WorldMapStorage.OnMapUpdated(fields);
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.BottomFloor);
            // TODO
        }
    }
}
