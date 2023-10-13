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

        protected Creature()
        {
            Spd = new Die(1);
            Items = new Dictionary<string, Item>();
        }

        public Creature(string name, int hp, int atk, int spd)
        {
            Name = name;
            Hp = hp;
            Atk = atk;
            Spd = new Die(spd);
            Items = new Dictionary<string, Item>();
        }

        public Status GetStatus()
        {
            return Status;
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

        public int RollSpd(int count)
        {
            Console.WriteLine(Name + " rolls for Spd.");
            return Spd.Roll(count, true);
        }

        public virtual string PrintStats()
        {
            return string.Format("_{0}_\nhp: {1}\natk:{2}\nspd: {3}",
                    Name, Hp, Atk, Spd);
        }

        public virtual void Damage(int val)
        {
            ChangeHP(-val);
        }

        public virtual void Heal(int val)
        {
            ChangeHP(val);
        }

        public virtual bool AddItem(Item item)
        {
            string name = item.GetName();
            if (Items.ContainsKey(name))
            {
                if (Items[name].AddUses(item.GetUses()))
                {
                    Console.WriteLine("Now have " + Items[name].GetUses() + " uses.");
                }
                else
                {
                    Console.WriteLine("Maximum uses " + Items[name].GetUses() + " in Inventory.");
                }
            }
            else
            {
                Console.WriteLine("New item " + name + " added to Inventory.");
                Items.Add(name, item);
            }

            return true;
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