using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;
using OXGaming.TibiaAPI.Utilities;
using OXGaming.TibiaAPI.WorldMap;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class BottomFloor : ServerPacket
    {
        private const int GroundLayer = 7;
        private const int UndergroundLayer = 2;
        private const int MapMaxZ = 15;

        public List<(Field, Position)> Fields { get; } = new List<(Field, Position)>();

        public BottomFloor()
        {
            PacketType = ServerPacketType.BottomFloor;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.BottomFloor)
            {
                return false;
            }

            var position = client.WorldMapStorage.GetPosition();
            position.X--;
            position.Y--;
            position.Z++;

            client.WorldMapStorage.SetPosition(position.X, position.Y, position.Z);

            if (position.Z > (GroundLayer + 1))
            {
               client.WorldMapStorage.ScrollMap(0, 0, 1);

               if (position.Z <= (MapMaxZ - UndergroundLayer))
               {
                   message.ReadFloor(client, 0, 0, Fields);
               }
            }
            else if (position.Z == (GroundLayer + 1))
            {
               client.WorldMapStorage.ScrollMap(0, 0, (UndergroundLayer + 1));

               var numberOfTilesToSkip = 0;
               var floorNumber = UndergroundLayer;
               while (floorNumber >= 0)
               {
                   numberOfTilesToSkip = message.ReadFloor(client, floorNumber, numberOfTilesToSkip, Fields);
                   floorNumber--;
               }
            }
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.BottomFloor);
            // TODO
        }
    }
}
