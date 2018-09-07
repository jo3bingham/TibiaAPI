using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class MapBase : ServerPacket
    {
        //private const int GroundLayer = 7;
        //private const int UndergroundLayer = 2;
        //private const int MapSizeX = 18;
        //private const int MapSizeY = 14;
        //private const int MapSizeZ = 8;
        //private const int MapSizeW = 10;
        //private const int MapMaxZ = 15;

        // TODO: Implmemented equivalent Write methods.

        //protected int ReadField(NetworkMessage message, int x, int y, int z, List<Field> fields)
        //{
        //    var thingsCount = 0;
        //    var numberOfTilesToSkip = 0;
        //    var mapPosition = new Position(x, y, z);
        //    var absolutePosition = client.WorldMapStorage.ToAbsolute(mapPosition);

        //    while (true)
        //    {
        //        var thingId = message.ReadUInt16();
        //        if (thingId >= 65280)
        //        {
        //            numberOfTilesToSkip = thingId - 65280;
        //            break;
        //        }

        //        if (thingId == (int)CreatureInstanceType.UnknownCreature ||
        //            thingId == (int)CreatureInstanceType.OutdatedCreature ||
        //            thingId == (int)CreatureInstanceType.Creature)
        //        {
        //            var creature = message.ReadCreatureInstance(thingId, absolutePosition);
        //            var objectInstance = client.AppearanceStorage.CreateObjectInstance(99, creature.Id);

        //            if (thingsCount < MapSizeW)
        //            {
        //                client.WorldMapStorage.AppendObject(x, y, z, objectInstance);
        //            }
        //        }
        //        else
        //        {
        //            var objectInstance = message.ReadObjectInstance(thingId);

        //            if (thingsCount < MapSizeW)
        //            {
        //                client.WorldMapStorage.AppendObject(x, y, z, objectInstance);
        //            }
        //            else
        //            {
        //                throw new Exception("Connection.readField: Expected creatures but received regular object.");
        //            }
        //        }

        //        thingsCount++;
        //    }

        //    var field = client.WorldMapStorage.GetField(x, y, z);
        //    if (field != null && field.objectsCount > 0)
        //    {
        //        field.Position = absolutePosition;
        //        fields.Add(field);
        //    }

        //    return numberOfTilesToSkip;
        //}

        //protected int ReadFloor(NetworkMessage message, int floorNumber, int numberOfTilesToSkip, List<Field> fields)
        //{
        //    if (message == null)
        //    {
        //        throw new Exception("ReadFloor: Not enough data.");
        //    }

        //    if (floorNumber < 0 || floorNumber >= MapSizeZ)
        //    {
        //        throw new Exception("ReadFloor: Floor number out of range.");
        //    }

        //    var currentX = 0;
        //    var currentY = 0;
        //    while (currentX <= MapSizeX - 1)
        //    {
        //        currentY = 0;
        //        while (currentY <= MapSizeY - 1)
        //        {
        //            if (numberOfTilesToSkip > 0)
        //            {
        //                numberOfTilesToSkip--;
        //            }
        //            else
        //            {
        //                numberOfTilesToSkip = ReadField(message, currentX, currentY, floorNumber, fields);
        //            }
        //            currentY++;
        //        }
        //        currentX++;
        //    }

        //    return numberOfTilesToSkip;
        //}

        //protected int ReadArea(NetworkMessage message, int startX, int startY, int endX, int endY, List<Field> fields)
        //{
        //    var endZ = 0;
        //    var stepZ = 0;
        //    var numberOfTilesToSkip = 0;
        //    var currentX = 0;
        //    var currentY = 0;
        //    var currentZ = 0;
        //    var position = client.WorldMapStorage.GetPosition();

        //    if (position.Z <= GroundLayer)
        //    {
        //        currentZ = 0;
        //        endZ = GroundLayer + 1;
        //        stepZ = 1;
        //    }
        //    else
        //    {
        //        currentZ = 2 * UndergroundLayer;
        //        endZ = Math.Max(-1, position.Z - MapMaxZ + 1);
        //        stepZ = -1;
        //    }

        //    while (currentZ != endZ)
        //    {
        //        currentX = startX;
        //        while (currentX <= endX)
        //        {
        //            currentY = startY;
        //            while (currentY <= endY)
        //            {
        //                if (numberOfTilesToSkip > 0)
        //                {
        //                    numberOfTilesToSkip--;
        //                }
        //                else
        //                {
        //                    numberOfTilesToSkip = ReadField(message, currentX, currentY, currentZ, fields);
        //                }
        //                currentY++;
        //            }
        //            currentX++;
        //        }
        //        currentZ += stepZ;
        //    }

        //    return numberOfTilesToSkip;
        //}
    }
}
