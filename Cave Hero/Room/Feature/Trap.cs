using CaveHero.CHDie;
using CaveHero.CHCreature;

namespace CaveHero.CHRoom.Feature {
    public class Trap : IFeature
    {
        private bool _active;

        private Die _dmg;
        private Die _spd;
        private Die _count;

        public Trap(Die dmg, Die spd, Die count) {
            _dmg = dmg;
            _spd = spd;
            _count = count;

            _active = true;
        }

        public void SetActive(bool active) {
            _active = active;
        }

        public void Trigger(List<Creature> party)
        {
            if (_active) {
                Spring(party);
            } else {
                Inert();
            }
        }
        
        public string Mention() {
            return "It looks like there's some form of trap in this room";
        }


        protected void Spring(List<Creature> party) {
            int c = _count.Roll();
            int o = party.Count;
            if (c > o) {
                c = o;
            }

            Random rnd = new();

            while (c > 0) {
                int i = rnd.Next(0, o);
                ApplyTrap(party[i]);
                c--;
            }

            _active = false;
        }

        protected void ApplyTrap(Creature target) {
            string name = target.GetName();

            int tSpd = target.RollSpd();
            int dSpd = _spd.Roll();

            if (tSpd >= dSpd)
            {
                Game.IO.WriteMsg(" It triggers (" + dSpd + ") , but " + name + " (" + tSpd + ") manages to quickly evade it!");
            }
            else
            {
                int damage = _dmg.Roll();
                Game.IO.WriteMsg("A trap tiggers and catches " + name + " off guard, dealing " + damage + " damage !");
                target.Damage(damage);
            }
        }

        protected void Inert() {
            Game.IO.WriteMsg("There was a trap in this room, but it no longer functions.");
        }
    }
}