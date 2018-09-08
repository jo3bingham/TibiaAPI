using OXGaming.TibiaAPI.Utilities;

namespace OXGaming.TibiaAPI.Appearances
{
    public class AppearanceInstance
    {
        public Appearance Type { get; }

        public ushort Id { get; }

        public AppearanceInstance(ushort id, Appearance type)
        {
            Id = id;
            Type = type;
        }
    }
}
