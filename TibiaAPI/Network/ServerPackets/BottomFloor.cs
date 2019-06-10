using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class BottomFloor : Map
    {
        private const int GroundLayer = 7;
        private const int UndergroundLayer = 2;
        private const int MapMaxZ = 15;

        public BottomFloor(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.BottomFloor;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            var position = Client.WorldMapStorage.GetPosition();
            position.X--;
            position.Y--;
            position.Z++;

            Client.WorldMapStorage.SetPosition(position.X, position.Y, position.Z);

            if (position.Z > (GroundLayer + 1))
            {
               Client.WorldMapStorage.ScrollMap(0, 0, 1);

               if (position.Z <= (MapMaxZ - UndergroundLayer))
               {
                   message.ReadFloor(0, 0, Fields);
               }
            }
            else if (position.Z == (GroundLayer + 1))
            {
               Client.WorldMapStorage.ScrollMap(0, 0, (UndergroundLayer + 1));

               var numberOfTilesToSkip = 0;
               var floorNumber = UndergroundLayer;
               while (floorNumber >= 0)
               {
                   numberOfTilesToSkip = message.ReadFloor(floorNumber, numberOfTilesToSkip, Fields);
                   floorNumber--;
               }
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.BottomFloor);
            base.AppendToNetworkMessage(message);
        }
    }
}
