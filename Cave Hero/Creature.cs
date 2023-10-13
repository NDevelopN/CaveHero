namespace Cave
{
    public class Creature
    {
        protected Status Status = Status.OK;
        protected string Name = "";
        protected int Hp = 0;
        protected int Atk = 0;
        protected int Spd = 0;

        protected Creature() { }

        public Creature(string name, int hp, int atk, int spd)
        {
            Name = name;
            Hp = hp;
            Atk = atk;
            Spd = spd;
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

        public int GetSpd()
        {
            return Spd;
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