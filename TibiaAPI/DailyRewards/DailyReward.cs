using System.Collections.Generic;

namespace OXGaming.TibiaAPI.DailyRewards
{
    public class DailyReward
    {
        public List<(ushort ItemId, string Name, uint Weight)> ChoiceRewards { get; } =
            new List<(ushort ItemId, string Name, uint Weight)>();
        public List<(byte Type, ushort ItemId, string Name, byte Count, ushort Duration)> SetRewards { get; } =
            new List<(byte Type, ushort ItemId, string Name, byte Count, ushort Duration)>();

        public byte TotalChoiceRewards { get; set; }
        public byte Type { get; set; }
    }
}
