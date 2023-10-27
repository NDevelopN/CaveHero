using CaveHero.CHDie;
using CaveHero.CHCreature;

namespace CaveHero.CHItem
{
    public class Potion : Item
    {
        private Die _die;
        private int _maxUses = 7;

        public Potion(int size, int count, int uses) : base("Potion", uses)
        {
            _die = new Die(size, count);
        }

        public override bool Use(Creature? target)
        {
            //TODO
            if (target == null) {
                return false;
            }

            target.Heal(_die.Roll());
            return Use();
        }

        public override bool AddUses(int uses)
        {
            int total = uses + Uses;
            if (total > _maxUses)
            {
                Uses = _maxUses;
                return false;
            }

            Uses = total;
            return true;
        }
    }
}