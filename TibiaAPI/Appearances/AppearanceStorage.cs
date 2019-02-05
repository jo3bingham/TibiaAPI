using System.IO;
using System.Linq;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Appearances
{
    public class AppearanceStorage
    {
        private Utilities.Appearances appearances;

        private uint lastObjectId;
        private uint lastOutfitId;

        public void LoadAppearances(FileStream datFileStream)
        {
            appearances = Utilities.Appearances.Parser.ParseFrom(datFileStream);
            lastObjectId = appearances.Object.Aggregate((last, current) => last.Id > current.Id ? last : current).Id;
            lastOutfitId = appearances.Outfit.Aggregate((last, current) => last.Id > current.Id ? last : current).Id;
        }

        public ObjectInstance CreateObjectInstance(uint id, uint data)
        {
            if (id >= (uint)CreatureInstanceType.Creature && id <= lastObjectId)
            {
                return new ObjectInstance(id, appearances.Object.FirstOrDefault(i => i.Id == id), data);
            }
            return null;
        }

        public OutfitInstance CreateOutfitInstance(uint id, byte colorHead, byte colorTorso, byte colorLegs, byte colorDetail, byte addons)
        {
            if (id >= 0 && id <= lastOutfitId)
            {
                return new OutfitInstance(id, appearances.Outfit.FirstOrDefault(i => i.Id == id), colorHead, colorTorso, colorLegs, colorDetail, addons);
            }
            return null;
        }

        public Utilities.Appearance GetObjectType(uint id)
        {
            if (id > (uint)CreatureInstanceType.Creature && id <= lastObjectId)
            {
                return appearances.Object.FirstOrDefault(o => o.Id == id);
            }
            return null;
        }

        public Utilities.Appearance GetOutfitType(uint id)
        {
            if (id > 0 && id <= lastOutfitId)
            {
                appearances.Outfit.FirstOrDefault(i => i.Id == id);
            }
            return null;
        }
    }
}
