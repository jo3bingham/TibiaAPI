namespace OXGaming.TibiaAPI.Utilities
{
    public class Position
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
    }
}
