namespace OXGaming.TibiaAPI.Utilities
{
    public struct Position : System.IEquatable<Position>
    {
        public ushort X { get; set; }
        public ushort Y { get; set; }
        public byte Z { get; set; }

        public Position(ushort x, ushort y, byte z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public bool Equals(Position position)
        {
            return position.X == X && position.Y == Y && position.Z == Z;
        }

        public override bool Equals(object obj)
        {
            return obj is Position && Equals((Position)obj);
        }

        public override int GetHashCode()
        {
            return ((X << 3) + (Y << 1) + Z).GetHashCode();
        }

        public static bool operator ==(Position left, Position right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Position left, Position right)
        {
            return !(left == right);
        }
    }
}
