using OXGaming.TibiaAPI.Utilities;

namespace OXGaming.TibiaAPI.Appearances
{
    public class ObjectInstance : AppearanceInstance
    {
        public uint Data { get; set; }
        public uint LootContainerUnknown { get; set; }

        public bool IsLootContainer { get; set; } = false;

        public ObjectInstance(uint id, Appearance type, uint data = 0) : base(id, type)
        {
            Data = data;
        }
    }
}
