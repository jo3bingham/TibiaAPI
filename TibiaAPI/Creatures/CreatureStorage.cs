using System;
using System.Collections.Generic;
using System.Linq;

namespace OXGaming.TibiaAPI.Creatures
{
    public class CreatureStorage
    {
        private const int MaxCreaturesCount = 2600;

        private readonly List<Creature> _creatures = new List<Creature>(MaxCreaturesCount);

        public void Reset()
        {
            _creatures.Clear();
        }

        public Creature GetCreature(uint creatureId)
        {
            return _creatures.FirstOrDefault(c => c.Id == creatureId);
        }

        public Creature GetCreature(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }
            return _creatures.FirstOrDefault(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public void RemoveCreature(uint creatureId)
        {
            var creature = GetCreature(creatureId);
            if (creature == null)
            {
                throw new Exception($"[CreatureStorage.RemoveCreature] Creature not found: {creatureId}");
            }

            //if (creature == Player)
            //{
            //    throw new Exception("[CreatureStorage.RemoveCreature] Can't remove the player.");
            //}

            _creatures.Remove(creature);
        }

        public Creature ReplaceCreature(Creature newCreature, uint removeCreatureId = 0)
        {
            if (removeCreatureId != 0)
            {
                RemoveCreature(removeCreatureId);
            }

            if (_creatures.Count >= MaxCreaturesCount)
            {
                throw new Exception($"[CreatureStorage.ReplaceCreature] No space left to add creature: {newCreature.Id}");
            }

            var creature = GetCreature(newCreature.Id);
            if (creature != null)
            {
                return newCreature;
            }

            _creatures.Add(newCreature);
            return newCreature;
        }
    }
}
