namespace Cave
{
    public class Creature
    {
        protected Status Status = Status.OK;
        protected string Name = "";
        protected int Hp = 0;
        protected int Atk = 0;
        protected Die Spd;

        protected Dictionary<string, Item> Items;

        protected Creature? Target;

        protected Creature()
        {
            Spd = new Die(1, 1);
            Items = new Dictionary<string, Item>();
        }

        public Creature(string name, int hp, int atk, int spd)
        {
            Name = name;
            Hp = hp;
            Atk = atk;
            Spd = new Die(spd, 1);
            Items = new Dictionary<string, Item>();
        }

        public Status GetStatus()
        {
            return Status;
        }

        public void SetStatus(Status status)
        {
            Status = status;
        }

        public string GetName()
        {
            return Name;
        }

        public int GetHP()
        {
            return Hp;
        }

        public int GetAtk()
        {
            return Atk;
        }

        public Die GetSpd()
        {
            return Spd;
        }

        public int RollSpd()
        {
            Console.WriteLine(Name + " rolls for Spd.");
            return Spd.Roll();
        }

        public virtual string PrintStats()
        {
            return string.Format("_{0}_\nhp: {1}\natk:{2}\nspd: {3}",
                    Name, Hp, Atk, Spd.GetSides());
        }

        /**
         * Choose next creature to attack.
         * Overriding this function allows for varied target selection behaviour such as:
         *      - RandomEnemy       -   Target a random enemy to be the next target.
         *      - RandomCreature    -   Target any random creature, friend or foe.
         *      - RecentAttacker    -   Target last creature to have dealt damage (needs fallback).
         *      - Soft target       -   Target weakest foe.
         *      - Hard target       -   Target toughest foe.
         *      - Leader's target   -   Target the target of an ally.
         */
        protected virtual void SelectTarget(List<Creature> allies, List<Creature> enemies) {
            Random rnd = new();
            Target = enemies[rnd.Next(0, enemies.Count)];
        }

        public virtual void DoCombat(List<Creature> allies, List<Creature> enemies) {
            SelectTarget(allies, enemies);
            if (Target == null) {
                return;
            }

            Target.Damage(Atk);
            //TODO
        }

        public virtual void Damage(int val)
        {
            ChangeHP(-val);
        }

        public virtual void Heal(int val)
        {
            ChangeHP(val);
        }

        public virtual void AddItem(Item item) {
            Items.Add(item.GetName(), item);
        }

        public virtual Item[] GetItems()
        {
            return Items.Values.ToArray();
        }

        public virtual int GetItemCount(string name)
        {
            if (!Items.ContainsKey(name))
            {
                return 0;
            }

            return Items[name].GetUses();
        }

        protected virtual void ChangeHP(int val)
        {
            Hp += val;
            if (Hp <= 0)
            {
                Die();
            }
            else
            {
                Console.WriteLine(Name + " now has " + Hp + "hp remaining.");
            }
        }

        protected virtual void Die()
        {
            Status = Status.DEAD;
            Console.WriteLine(Name + " has been defeated!");
        }
    }
}