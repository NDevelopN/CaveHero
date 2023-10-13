namespace Cave
{
    public class SimpleTrap : Encounter 
    {

        int _dmg = 1;
        int _spd = 1;

        public SimpleTrap() : base() { }

        public SimpleTrap(int dmg, int spd)
        {
            _dmg = dmg;
            _spd = spd;
        }

        public void SetDmg(int dmg)
        {
            _dmg = dmg;
        }

        public void SetSpd(int spd)
        {
            _spd = spd;
        }

        public override void Trigger(Hero hero)
        {
            if (Solved) { NoDanger(hero); return; }

            string name = hero.GetName();

            if (hero.GetSpd() >= _spd)
            {
                Console.WriteLine("A trap (" + _spd + ") triggers, but " + name + " (" + hero.GetSpd() + ") manages to quickly evade it!");
            }
            else
            {
                Console.WriteLine("A trap tiggers and catches " + name + " off guard, dealing " + _dmg + " damage !");
                hero.Damage(_dmg);
            }
        }
    }
}