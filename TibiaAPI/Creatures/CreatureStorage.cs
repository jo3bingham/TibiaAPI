using System;
using System.Collections.Generic;

namespace OXGaming.TibiaAPI.Creatures
{
    public class CreatureStorage
    {
        private const int MaxCreaturesCount = 1300;

        private readonly List<Creature> _creatures = new List<Creature>();

        public void Reset()
        {
            _creatures.ForEach(c => c.Reset());
            _creatures.Clear();
        }

        public Creature GetCreature(uint creatureId)
        {
            var counter = 0;
            var count = _creatures.Count - 1;
            while (counter <= count)
            {
                var index = counter + count >> 1;
                var creature = _creatures[index];
                if (creature.Id == creatureId)
                {
                    return creature;
                }

                if (creature.Id < creatureId)
                {
                    counter = index + 1;
                    continue;
                }

                count = index - 1;
            }

            return null;
        }

        public void RemoveCreature(uint creatureId)
        {
            Creature creature = null;
            var index = 0;
            var counter = 0;
            var count = _creatures.Count - 1;
            while (counter <= count)
            {
                index = counter + count >> 1;
                creature = _creatures[index];
                if (creature.Id == creatureId)
                {
                    break;
                }

                if (creature.Id < creatureId)
                {
                    counter = index + 1;
                    continue;
                }

                count = index - 1;
            }

            if (creature == null || index < 0)
            {
                throw new Exception("CreatureStorage.removeCreature: Creature " + creatureId + " not found.");
            }

            //if (creature == Player)
            //{
            //    throw new Exception(@"CreatureStorage.removeCreature: Can't remove the player.");
            //}

            //creature.Reset();
            _creatures.RemoveAt(index);
        }

        public Creature ReplaceCreature(Creature newCreature, uint removeCreatureId = 0)
        {
            if (removeCreatureId != 0)
            {
                RemoveCreature(removeCreatureId);
            }

            if (_creatures.Count >= MaxCreaturesCount)
            {
                throw new Exception($"[CreatureStorage.ReplaceCreature] No space left to append {newCreature.Id}");
            }

            var counter = 0;
            var count = _creatures.Count - 1;
            while (counter <= count)
            {
                var index = counter + count >> 1;
                var creature = _creatures[index];
                if (creature.Id == newCreature.Id)
                {
                    return newCreature;
                }

                if (creature.Id < newCreature.Id)
                {
                    counter = index + 1;
                    continue;
                }

                count = index - 1;
            }

            _creatures.Insert(counter, newCreature);
            return newCreature;
        }
    }
}
