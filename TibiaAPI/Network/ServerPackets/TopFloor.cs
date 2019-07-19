using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class TopFloor : Map
    {
        private const int GroundLayer = 7;
        private const int UndergroundLayer = 2;

        public TopFloor(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.TopFloor;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            var position = Client.WorldMapStorage.GetPosition();
            position.X++;
            position.Y++;
            position.Z--;

            Client.WorldMapStorage.SetPosition(position.X, position.Y, position.Z);

            if (position.Z > GroundLayer)
            {
               Client.WorldMapStorage.ScrollMap(0, 0, -1);
               message.ReadFloor((2 * UndergroundLayer), 0, Fields);
            }
            else if (position.Z == GroundLayer)
            {
               Client.WorldMapStorage.ScrollMap(0, 0, -(UndergroundLayer + 1));

               var numberOfTilesToSkip = 0;
               var floorNumber = UndergroundLayer;
               while (floorNumber <= GroundLayer)
               {
                   numberOfTilesToSkip = message.ReadFloor(floorNumber, numberOfTilesToSkip, Fields);
                   floorNumber++;
               }
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.TopFloor);
            base.AppendToNetworkMessage(message);
        }
    }
}
