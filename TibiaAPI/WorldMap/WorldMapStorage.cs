using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Appearances;
using OXGaming.TibiaAPI.Utilities;

namespace OXGaming.TibiaAPI.WorldMap
{
    public class WorldMapStorage
    {
        private const int NumFields = 2016;
        private const int GroundLayer = 7;
        private const int UndergroundLayer = 2;
        private const int MapSizeX = 18;
        private const int MapSizeY = 14;
        private const int MapSizeZ = 8;
        private const int MapSizeW = 10;
        private const int PlayerOffsetX = 8;
        private const int PlayerOffsetY = 6;

        private List<Field> field;

        private Position position;
        private Position origin;

        private int playerZPlane;

        public WorldMapStorage()
        {
            position = new Position(0, 0, 0);
            origin = new Position(0, 0, 0);

            field = new List<Field>(NumFields);
            for (var i = 0; i < NumFields; ++i)
            {
                field.Add(new Field());
            }

            playerZPlane = 0;
        }

        public void ResetMap()
        {
            position.SetZero();
            origin.SetZero();

            playerZPlane = 0;

            var i = 0;
            while (i < NumFields)
            {
                field[i].Reset();
                i++;
            }
        }

        public void ResetField(int x, int y, int z)
        {
            var index = ToIndexInternal(x, y, z);
            var tile = field[index];
            tile.ResetObjects();
        }

        public bool IsVisible(int x, int y, int z, bool ignoreFloor)
        {
            var pos = new Position(x, y, z);
            pos = ToMapInternal(pos);
            return pos != null && (ignoreFloor || position.Z == z);
        }

        public void ScrollMap(int x, int y, int z = 0)
        {
            if (x < -MapSizeX || x > MapSizeX)
            {
                throw new ArgumentException("WorldMapStorage.scrollMap: X=" + x + " is out of range.");
            }

            if (y < -MapSizeY || y > MapSizeY)
            {
                throw new ArgumentException("WorldMapStorage.scrollMap: Y=" + y + " is out of range.");
            }

            if (z < -MapSizeZ || z > MapSizeZ)
            {
                throw new ArgumentException("WorldMapStorage.scrollMap: Z=" + z + " is out of range.");
            }

            if (x * y + y * z + x * z != 0)
            {
                throw new ArgumentException("WorldMapStorage.scrollMap: Only one of the agruments may be != 0.");
            }

            var currentX = 0;
            var currentY = 0;
            var currentZ = 0;

            if (x != 0)
            {
                var startX = 0;
                var endX = -x;
                if (x > 0)
                {
                    startX = MapSizeX - x;
                    endX = MapSizeX;
                }

                currentX = startX;
                while (currentX < endX)
                {
                    currentY = 0;
                    while (currentY < MapSizeY)
                    {
                        currentZ = 0;
                        while (currentZ < MapSizeZ)
                        {
                            ResetField(currentX, currentY, currentZ);
                            currentZ++;
                        }
                        currentY++;
                    }
                    currentX++;
                }

                origin.X -= x;
                if (origin.X < 0)
                {
                    origin.X += MapSizeX;
                }

                origin.X %= MapSizeX;
            }

            if (y != 0)
            {
                var startY = 0;
                var endY = -y;
                if (y > 0)
                {
                    startY = MapSizeY - y;
                    endY = MapSizeY;
                }

                currentX = 0;
                while (currentX < MapSizeX)
                {
                    currentY = startY;
                    while (currentY < endY)
                    {
                        currentZ = 0;
                        while (currentZ < MapSizeZ)
                        {
                            ResetField(currentX, currentY, currentZ);
                            currentZ++;
                        }
                        currentY++;
                    }
                    currentX++;
                }

                origin.Y -= y;
                if (origin.Y < 0)
                {
                    origin.Y += MapSizeY;
                }

                origin.Y %= MapSizeY;
            }

            if (z != 0)
            {
                var startZ = 0;
                var endZ = -z;
                if (z > 0)
                {
                    startZ = MapSizeZ - z;
                    endZ = MapSizeZ;
                }

                currentX = 0;
                while (currentX < MapSizeX)
                {
                    currentY = 0;
                    while (currentY < MapSizeY)
                    {
                        currentZ = startZ;
                        while (currentZ < endZ)
                        {
                            ResetField(currentX, currentY, currentZ);
                            currentZ++;
                        }
                        currentY++;
                    }
                    currentX++;
                }

                origin.Z -= z;
                if (origin.Z < 0)
                {
                    origin.Z += MapSizeZ;
                }

                origin.Z %= MapSizeZ;

                if (z > 0)
                {
                    currentX = 0;
                    while (currentX < MapSizeX)
                    {
                        currentY = 0;
                        while (currentY < MapSizeY)
                        {
                            currentZ = MapSizeZ - UndergroundLayer - 1;
                            while (currentZ < MapSizeZ)
                            {
                                ResetField(currentX, currentY, currentZ);
                                currentZ++;
                            }
                            currentY++;
                        }
                        currentX++;
                    }
                }
            }
        }

        public Field GetField(int x, int y, int z)
        {
            return field[ToIndexInternal(x, y, z)];
        }

        public ObjectInstance AppendObject(int x, int y, int z, ObjectInstance thing)
        {
            return field[ToIndexInternal(x, y, z)].PutObject(thing, MapSizeW);
        }

        public ObjectInstance ChangeObject(int x, int y, int z, int stackPosition, ObjectInstance thing)
        {
            return field[ToIndexInternal(x, y, z)].ChangeObject(thing, stackPosition);
        }

        public ObjectInstance DeleteObject(int x, int y, int z, int stackPosition)
        {
            return field[ToIndexInternal(x, y, z)].DeleteObject(stackPosition);
        }

        public ObjectInstance GetObject(int x, int y, int z, int stackPosition)
        {
            return field[ToIndexInternal(x, y, z)].GetObject(stackPosition);
        }

        public ObjectInstance InsertObject(int x, int y, int z, int stackPosition, ObjectInstance thing)
        {
            return field[ToIndexInternal(x, y, z)].PutObject(thing, stackPosition);
        }

        public ObjectInstance PutObject(int x, int y, int z, ObjectInstance thing)
        {
            return field[ToIndexInternal(x, y, z)].PutObject(thing, -1);
        }

        public Position GetPosition()
        {
            return position.Clone();
        }

        public void SetPosition(int x, int y, int z)
        {
            position.X = x;
            position.Y = y;
            position.Z = z;
            playerZPlane = z <= GroundLayer ? (MapSizeZ - 1 - z) : UndergroundLayer;
        }

        public Position ToAbsolute(Position pos)
        {
            var z = pos.Z - playerZPlane;
            var x = pos.X + (position.X - PlayerOffsetX) + z;
            var y = pos.Y + (position.Y - PlayerOffsetY) + z;
            z = position.Z - z;
            return new Position(x, y, z);
        }

        public Position ToMap(Position pos)
        {
            if (pos == null)
            {
                throw new ArgumentNullException(nameof(pos));
            }

            var mapPosition = ToMapInternal(pos);
            if (mapPosition == null)
            {
                throw new ArgumentException("WorldMapStorage.ToMap: Input co-ordinate " + pos + " is out of range.");
            }
            return mapPosition;
        }

        int ToIndexInternal(int x, int y, int z)
        {
            return ((z + origin.Z) % MapSizeZ * MapSizeX + (x + origin.X) % MapSizeX) * MapSizeY + (y + origin.Y) % MapSizeY;
        }

        Position ToMapInternal(Position pos)
        {
            if (pos == null)
            {
                return null;
            }

            var z = position.Z - pos.Z;
            var x = pos.X - (position.X - PlayerOffsetX) - z;
            var y = pos.Y - (position.Y - PlayerOffsetY) - z;
            z += playerZPlane;

            if (x < 0 || x >= MapSizeX || y < 0 || y >= MapSizeY || z < 0 || z >= MapSizeZ)
            {
                return null;
            }
            return new Position(x, y, z);
        }
    }
}
