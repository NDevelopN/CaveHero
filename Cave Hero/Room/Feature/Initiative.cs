using CaveHero.Utils;
using CaveHero.CHCreature;

namespace CaveHero.CHRoom.Feature {
    public class Initiative {

        private static SortedTuples<int, Creature> _order = new();
        private static int _i = 0;

        public static void AddToInitiative(List<Creature> group) {
            foreach (Creature creature in group) {
                int init = creature.RollSpd();
                _order.Add(new KeyValuePair<int, Creature>(init, creature));
            }
        }

        public static Creature Next() {
            if (_i >= _order.Count) {
                _i = 0;
            }

            Creature next = _order[_i].Value;
            _i++;

            return next;
        }

        public static void Clear() {
            _order.Clear();
            _i = 0;
        }
    }
}