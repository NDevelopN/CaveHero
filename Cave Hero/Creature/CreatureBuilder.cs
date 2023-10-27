using CaveHero.CHDie;

namespace CaveHero.CHCreature {
    public class CreatureBuilder {

        public string Name { get; set; }
        public int HP { get; set; }
        public Die Atk { get; set; }
        public Die Spd { get; set; }

        public CreatureBuilder() {
            Name = "";
            HP = 0;
            Atk = new Die(0, 0);
            Spd = new Die(0, 0);
        }

        public virtual Creature Build() {
            return new Creature(Name, HP, Atk, Spd);
        }

        public void Reset() {
            Name = "";
            HP = 0;
            Atk = new Die(0, 0);
            Spd = new Die(0, 0);
        }
    }
}