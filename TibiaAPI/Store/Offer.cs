using System.Collections.Generic;

namespace OXGaming.TibiaAPI.Store
{
    public class Offer
    {
        public List<OfferDetails> Details { get; } = new List<OfferDetails>();
        public List<Offer> Products { get; } = new List<Offer>();

        public string DisplayImage { get; set; }
        public string Name { get; set; }
        public string ParentCategory { get; set; }

        public uint NewUntilTimestamp { get; set; }

        public ushort DisplayItemId { get; set; }
        public ushort DisplayFemaleLooktype { get; set; }
        public ushort DisplayLooktype { get; set; }
        public ushort DisplayMaleLooktype { get; set; }
        public ushort DisplayMountId { get; set; }
        public ushort PopularityScore { get; set; }

        public byte DisplayColorDetail { get; set; }
        public byte DisplayColorHead { get; set; }
        public byte DisplayColorLegs { get; set; }
        public byte DisplayColorTorso { get; set; }
        public byte DisplayType { get; set; }
        public byte GenderToShow { get; set; }
        public byte TryType { get; set; }

        public bool NeedsUserConfigurationBeforeBuying { get; set; }
    }
}
