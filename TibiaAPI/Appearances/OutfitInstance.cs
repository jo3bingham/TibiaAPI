using OXGaming.TibiaAPI.Utilities;

namespace OXGaming.TibiaAPI.Appearances
{
    public class OutfitInstance : AppearanceInstance
    {
        public byte Addons { get; set; }
        public byte ColorDetail { get; set; }
        public byte ColorHead { get; set; }
        public byte ColorLegs { get; set; }
        public byte ColorTorso { get; set; }

        public OutfitInstance(uint id, Appearance type, byte colorHead, byte colorTorso, byte colorLegs, byte colorDetail, byte addons) : base(id, type)
        {
            ColorHead = colorHead;
            ColorTorso = colorTorso;
            ColorLegs = colorLegs;
            ColorDetail = colorDetail;
            Addons = addons;
        }

        public override string ToString()
        {
            return $"Outfit Looktype: {Id}, Head: {ColorHead}, Torso: {ColorTorso}, Legs: {ColorLegs}, Detail: {ColorDetail}, Addons: {Addons}";
        }
    }
}
