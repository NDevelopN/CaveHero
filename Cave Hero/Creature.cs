namespace Cave
{
    public class Creature
    {
        protected Status Status = Status.OK;
        protected string Name = "";
        protected int Hp = 0;
        protected int Atk = 0;
        protected Die Spd;

        protected Creature()
        {
            Spd = new Die(1);
        }

        public Creature(string name, int hp, int atk, int spd)
        {
            Name = name;
            Hp = hp;
            Atk = atk;
            Spd = new Die(spd);
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