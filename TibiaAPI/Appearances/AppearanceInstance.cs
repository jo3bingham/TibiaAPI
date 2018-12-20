using OXGaming.TibiaAPI.Utilities;

namespace OXGaming.TibiaAPI.Appearances
{
    public class AppearanceInstance
    {
        public Appearance Type { get; }

        public uint Id { get; }

        public byte Mark { get; set; }
        public byte Phase { get; set; }

        public AppearanceInstance(uint id, Appearance type)
        {
            Id = id;
            Type = type;
        }
    }
}
