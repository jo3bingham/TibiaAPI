namespace OXGaming.TibiaAPI.Imbuing
{
    public class AstralSource
    {
        public string Name { get; }

        public ushort AppearanceTypeId { get; }
        public ushort ObjectCount { get; }

        public AstralSource(ushort appearanceTypeId, ushort objectCount, string name = "")
        {
            AppearanceTypeId = appearanceTypeId;
            ObjectCount = objectCount;
            Name = name;
        }
    }
}
