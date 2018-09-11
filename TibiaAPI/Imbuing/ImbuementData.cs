using System.Collections.Generic;

namespace OXGaming.TibiaAPI.Imbuing
{
    public class ImbuementData
    {
        public List<AstralSource> AstralSources { get; } = new List<AstralSource>();

        public string Category { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }

        public uint DurationInSeconds { get; set; }
        public uint GoldCost { get; set; }
        public uint Id { get; set; }
        public uint ProtectionGoldCost { get; set; }

        public ushort IconId { get; set; }

        public byte SuccessRatePercent { get; set; }

        public bool PremiumOnly { get; set; }

        public ImbuementData(uint id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
