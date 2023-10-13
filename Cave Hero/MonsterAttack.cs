namespace Cave
{
    public class MonsterAttack : Encounter 
    {
        private Creature _enemy;

        public MonsterAttack() : base()
        {
            Random rnd = new();
            int hp = rnd.Next(1, 3);
            int atk = 1;
            int spd = rnd.Next(0, 4);

            int lvl = hp + atk + spd;

            _enemy = new Creature("Monster[" + lvl + "]", hp, atk, spd);
        }

        public MonsterAttack(Creature enemy) : base()
        {
            _enemy = enemy;
        }

        public override void Trigger(Hero hero)
        {
            if (Solved) { NoDanger(hero); return; }

            Console.WriteLine("Ahh! An enemy attacks!");
            Console.WriteLine(_enemy.PrintStats());

            Creature first = (_enemy.GetSpd() > hero.GetSpd()) ? _enemy : hero;
            Creature second = (first == _enemy) ? hero : _enemy;

            Console.WriteLine(first.GetName() + " goes first!");

            int fAtk = first.GetAtk();
            int sAtk = second.GetAtk();

            while (first.GetStatus() != Status.DEAD && second.GetStatus() != Status.DEAD)
            {
                second.Damage(fAtk);
                if (second.GetStatus() != Status.DEAD)
                {
                    first.Damage(sAtk);
                }
            }

            Solved = true;
        }
    }
}