using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;
using OXGaming.TibiaAPI.Utilities;
using OXGaming.TibiaAPI.WorldMap;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class TopFloor : ServerPacket
    {
        private const int GroundLayer = 7;
        private const int UndergroundLayer = 2;

        public List<(Field, Position)> Fields { get; } = new List<(Field, Position)>();

        public TopFloor()
        {
            PacketType = ServerPacketType.TopFloor;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.TopFloor)
            {
                return false;
            }

            var position = client.WorldMapStorage.GetPosition();
            position.X++;
            position.Y++;
            position.Z--;

            client.WorldMapStorage.SetPosition(position.X, position.Y, position.Z);

            if (position.Z > GroundLayer)
            {
               client.WorldMapStorage.ScrollMap(0, 0, -1);
               message.ReadFloor(client, (2 * UndergroundLayer), 0, Fields);
            }
            else if (position.Z == GroundLayer)
            {
               client.WorldMapStorage.ScrollMap(0, 0, -(UndergroundLayer + 1));

               var numberOfTilesToSkip = 0;
               var floorNumber = UndergroundLayer;
               while (floorNumber <= GroundLayer)
               {
                   numberOfTilesToSkip = message.ReadFloor(client, floorNumber, numberOfTilesToSkip, Fields);
                   floorNumber++;
               }
            }
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.TopFloor);
            // TODO
        }
    }
}
