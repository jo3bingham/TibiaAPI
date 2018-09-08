using System.Linq;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Appearances
{
    public class AppearanceStorage
    {
        private Utilities.Appearances appearances;

        private uint lastObjectId;

        public void LoadAppearances(System.IO.FileStream datFile)
        {
            appearances = Utilities.Appearances.Parser.ParseFrom(datFile);
            lastObjectId = appearances.Object.Aggregate((last, current) => last.Id > current.Id ? last : current).Id;
        }

        public Utilities.Appearance GetObjectType(ushort id)
        {
            if (id >= (uint)CreatureInstanceType.Creature && id <= lastObjectId)
            {
                return appearances.Object.FirstOrDefault(o => o.Id == id);
            }
            return null;
        }

        public ObjectInstance CreateObjectInstance(ushort id, byte data)
        {
            if (id >= (uint)CreatureInstanceType.Creature && id <= lastObjectId)
            {
                return new ObjectInstance(id, appearances.Object.FirstOrDefault(i => i.Id == id), data);
            }
            return null;
        }
    }
}
