using OXGaming.TibiaAPI.Appearances;
using OXGaming.TibiaAPI.Constants;
using OXGaming.TibiaAPI.Utilities;

namespace OXGaming.TibiaAPI.Creatures
{
    public class Creature
    {
        public AppearanceInstance Mount { get; set; }
        public AppearanceInstance Outfit { get; set; }

        public Position Position { get; set; }

        public CreatureInstanceType InstanceType { get; set; }

        public CreatureType Type { get; set; }

        public Direction Direction { get; set; }

        public string Name { get; set; }

        public uint Id { get; set; }
        public uint RemoveCreatureId { get; set; }
        public uint SummonerCreatureId { get; set; }

        public ushort PvpHelpers { get; set; }
        public ushort Speed { get; set; }

        public byte Brightness { get; set; }
        public byte GuildFlag { get; set; }
        public byte HealthPercent { get; set; }
        public byte InspectionState { get; set; }
        public bool IsUnpassable { get; set; }
        public byte LightColor { get; set; }
        public byte Mark { get; set; }
        public byte PartyFlag { get; set; }
        public byte PkFlag { get; set; }
        public byte SpeechCategory { get; set; }
        public byte Vocation { get; set; }

        public bool Visible { get; set; }

        // TODO
        public byte Unknown { get; set; }

        public bool IsSummon
        {
            get
            {
                return Type == CreatureType.PlayerSummon;
            }
        }

        public Creature(uint id, CreatureType type = CreatureType.Monster, string name = null)
        {
            Id = id;
            Type = type;
            Name = name;
        }
    }
}
