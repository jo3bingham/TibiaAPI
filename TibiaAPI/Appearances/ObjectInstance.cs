using OXGaming.TibiaAPI.Utilities;

namespace OXGaming.TibiaAPI.Appearances
{
    public class ObjectInstance : AppearanceInstance
    {
        public uint LootContainerUnknown { get; set; }

        public byte Data { get; set; }
        public byte Phase { get; set; }

        public bool IsLootContainer { get; set; } = false;

        public ObjectInstance(ushort id, Appearance type, byte data = 0) : base(id, type)
        {
            Data = data;
        }
    }
}
