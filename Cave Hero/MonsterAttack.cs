namespace Cave
{
    public class MonsterAttack : Encounter
    {
        protected Creature Enemy;

        public MonsterAttack() : base()
        {
            Random rnd = new();
            int hp = rnd.Next(1, 3);
            int atk = 1;
            int spd = rnd.Next(1, 5);

            int lvl = hp + atk + spd;

            Enemy = new Creature("Monster[" + lvl + "]", hp, atk, spd);
            Enemy.AddItem(new Potion(6, 1));
        }

        public MonsterAttack(Creature enemy) : base()
        {
            Enemy = enemy;
        }

        public override void Trigger(Hero hero)
        {
            if (Solved) { NoDanger(hero); return; }

            Console.WriteLine("Ahh! An enemy attacks!");
            Console.WriteLine(Enemy.PrintStats());

            Combat(hero);
            Solved = true;
        }

        protected virtual void Combat(Hero hero)
        {
            int round = 1;
            while (hero.GetStatus() != Status.DEAD)
            {
                if (Enemy.GetStatus() != Status.DEAD)
                {
                    Reward(hero);
                    break;
                }

                Console.Write("Round " + round + ": ");
                Creature first = (Enemy.RollSpd(1) > hero.RollSpd(1)) ? Enemy : hero;
                Console.WriteLine(first.GetName() + " goes first!");
                Creature second = (first == Enemy) ? hero : Enemy;

                second.Damage(first.GetAtk());
                if (second.GetStatus() != Status.DEAD)
                {
                    first.Damage(second.GetAtk());
                }
            }
        }

        protected virtual void Reward(Hero hero)
        {
            foreach (Item item in Enemy.GetItems())
            {
                string name = item.GetName();
                Console.Write(Enemy.GetName() + " dropped " + name + ". ");
                hero.CollectItem(item);
            }
        }
    }
}