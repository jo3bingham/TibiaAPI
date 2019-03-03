using System.Collections.Generic;

namespace OXGaming.TibiaAPI.Store
{
    public class OfferDetails
    {
        public List<string> DisabledReasons { get; } = new List<string>();

        public uint Id { get; set; }
        public uint Price { get; set; }

        public ushort Amount { get; set; }

        public byte HighlightState { get; set; }
        public byte Unknown { get; set; }

        public bool IsDisabled { get; set; }
    }
}
