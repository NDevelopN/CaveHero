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
            int spd = rnd.Next(1, 5);

            int lvl = hp + atk + spd;

            _enemy = new Creature("Monster[" + lvl + "]", hp, atk, spd);
            _enemy.AddItem(new Potion(6, 1));
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

            Combat(hero);
            Solved = true;
        }

        protected void Combat(Hero hero)
        {
            int round = 1;
            while (hero.GetStatus() != Status.DEAD)
            {
                if (_enemy.GetStatus() != Status.DEAD)
                {
                    foreach (Item item in _enemy.GetItems())
                    {
                        string name = item.GetName();
                        Console.Write(_enemy.GetName() + " dropped " + name + ". ");
                        hero.AddItem(item);
                    }
                    break;
                }

                Console.Write("Round " + round + ": ");
                Creature first = (_enemy.RollSpd(1) > hero.RollSpd(1)) ? _enemy : hero;
                Console.WriteLine(first.GetName() + " goes first!");
                Creature second = (first == _enemy) ? hero : _enemy;

                second.Damage(first.GetAtk());
                if (second.GetStatus() != Status.DEAD)
                {
                    first.Damage(second.GetAtk());
                }
            }
        }
    }
}