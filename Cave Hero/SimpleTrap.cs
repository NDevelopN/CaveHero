namespace Cave
{
    public class SimpleTrap : Encounter
    {

        int _dmg = 1;
        Die _spd;

        public SimpleTrap() : base()
        {
            _spd = new Die(1);
        }

        public SimpleTrap(int dmg, int spd)
        {
            _dmg = dmg;
            _spd = new Die(spd);
        }

        public void SetDmg(int dmg)
        {
            _dmg = dmg;
        }

        public void SetSpd(int spd)
        {
            _spd.SetSides(spd);
        }

        public override void Trigger(Hero hero)
        {
            if (Solved) { NoDanger(hero); return; }

            Console.Write("A Trap!");

            string name = hero.GetName();

            int hSpd = hero.RollSpd(1);
            int tSpd = _spd.Roll(1, false);

            if (hSpd >= tSpd)
            {
                Console.WriteLine(" It triggers (" + tSpd + ") , but " + name + " (" + hSpd + ") manages to quickly evade it!");
            }
            else
            {
                Console.WriteLine("A trap tiggers and catches " + name + " off guard, dealing " + _dmg + " damage !");
                hero.Damage(_dmg);
            }
        }
    }
}