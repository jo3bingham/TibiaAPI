using System;

using OXGaming.TibiaAPI.Appearances;

namespace OXGaming.TibiaAPI.WorldMap
{
    public class Field
    {
        private const int MapSizeW = 10;

        private ObjectInstance[] objectsNetwork;

        private int objectsCount = 0;

        public Field()
        {
            objectsNetwork = new ObjectInstance[MapSizeW];
        }

        public void Reset()
        {
            ResetObjects();
        }

        public void ResetObjects()
        {
            objectsNetwork = new ObjectInstance[MapSizeW];
            objectsCount = 0;
        }

        public ObjectInstance ChangeObject(ObjectInstance objectInstance, int stackPosition)
        {
            if (objectInstance == null)
            {
                return null;
            }

            if (stackPosition < 0 || stackPosition >= objectsCount)
            {
                return null;
            }

            var oldThing = objectsNetwork[stackPosition];
            objectsNetwork[stackPosition] = objectInstance;
            return oldThing;
        }

        public ObjectInstance DeleteObject(int stackPosition)
        {
            if (stackPosition < 0 || stackPosition >= objectsCount)
            {
                return null;
            }

            var removedThing = objectsNetwork[stackPosition];
            objectsCount = Math.Max(0, objectsCount - 1);

            while (stackPosition < objectsCount)
            {
                objectsNetwork[stackPosition] = objectsNetwork[stackPosition + 1];
                stackPosition++;
            }

            objectsNetwork[objectsCount] = null;
            return removedThing;
        }

        public ObjectInstance GetObject(int stackPosition)
        {
            if (stackPosition == 0 && objectsCount == 0)
            {
                return null;
            }

            if (stackPosition < 0 || stackPosition >= objectsCount)
            {
                return null;
            }

            return objectsNetwork[stackPosition];
        }

        public ObjectInstance PutObject(ObjectInstance objectInstance, int stackPosition)
        {
            if (objectInstance == null)
            {
                return null;
            }

            if (stackPosition < 0 || stackPosition == MapSizeW)
            {
                stackPosition = 0;
                var newPriority = GetObjectPriority(objectInstance);
                while (stackPosition < objectsCount)
                {
                    var currentPriority = GetObjectPriority(objectsNetwork[stackPosition]);
                    if (currentPriority > newPriority || (currentPriority == newPriority && currentPriority == 5))
                    {
                        break;
                    }
                    stackPosition++;
                }

                if (stackPosition >= MapSizeW)
                {
                    return objectInstance;
                }
            }
            else if (stackPosition <= objectsCount || stackPosition == MapSizeW)
            {
                stackPosition = Math.Min(Math.Min(stackPosition, objectsCount), (MapSizeW - 1));
            }
            else
            {
                return null;
            }

            ObjectInstance removedThing = null;
            if (objectsCount >= MapSizeW)
            {
                objectsCount = MapSizeW;
                removedThing = objectsNetwork[MapSizeW - 1];
            }
            else
            {
                objectsCount++;
            }

            var count = objectsCount - 1;
            while (count > stackPosition)
            {
                objectsNetwork[count] = objectsNetwork[count - 1];
                count--;
            }

            objectsNetwork[stackPosition] = objectInstance;
            return removedThing;
        }

        public static int GetObjectPriority(ObjectInstance objectInstance)
        {
            if (objectInstance.Id == 99)
            {
                return 4;
            }

            var type = objectInstance.Type;
            if (type == null)
            {
                return 5;
            }

            if (type.Flags.Bank != null)
            {
                return 0;
            }

            if (type.Flags.Clip)
            {
                return 1;
            }

            if (type.Flags.Bottom)
            {
                return 2;
            }

            if (type.Flags.Top)
            {
                return 3;
            }

            return 5;
        }
    }
}
