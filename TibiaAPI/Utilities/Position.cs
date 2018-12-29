namespace OXGaming.TibiaAPI.Utilities
{
    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public Position(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public void SetZero()
        {
            X = 0;
            Y = 0;
            Z = 0;
        }

        public Position Subtract(Position position)
        {
            return new Position(X - position.X, Y - position.Y, Z - position.Z);
        }

        public Position Clone()
        {
            return new Position(X, Y, Z);
        }

        public override string ToString()
        {
            return $"{X},{Y},{Z}";
        }
    }
}
