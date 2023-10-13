namespace Cave
{
    public class Hero : Creature
    {
        private int _maxCompanions = 3;

        private List<Creature> _companions;

        public Hero() : base()
        {
            Name = "Hero";
            Hp = 3;
            Atk = 1;
            Spd = new Die(4);
            Items = new Dictionary<string, Item>() { { "Potion", new Potion(6, 1) } };
            _companions = new List<Creature>();
        }

        public void UseItem(string name)
        {
            UseItem(name, this);
        }

        public void UseItem(string name, Creature target)
        {
            if (!Items.ContainsKey(name))
            {
                Console.WriteLine("No " + name + " in Inventory.");
                return;
            }

            Items[name].Use(target);

            int uses = Items[name].GetUses();
            Console.Write("There are " + uses + " uses remaining. ");
            if (uses == 0)
            {
                Console.WriteLine("It is all gone!");
                Items.Remove(name);
            }
            else
            {
                Console.WriteLine();
            }
        }

        public bool Join(Creature companion)
        {
            if (_companions.Count < _maxCompanions)
            {
                _companions.Add(companion);
                return true;
            }

            return false;
        }

        public override void Damage(int val)
        {
            if (CompanionDamage(val)) { return; }

            base.Damage(val);
            if (Status != Status.DEAD)
            {
                if (Hp <= 2)
                {
                    Console.WriteLine("Health is low, using Potion.");
                    UseItem("Potion");
                }
            }
        }

        private bool CompanionDamage(int val)
        {
            int cCount = _companions.Count;
            if (cCount > 0)
            {
                Random rnd = new();
                int i = rnd.Next(0, cCount);
                Creature target = _companions[i];

                //TODO add some stance mechanic to change how this works
                if (Spd.Roll(1, false) < target.RollSpd(1))
                {
                    target.Damage(val);
                    if (target.GetHP() < 2)
                    {
                        if (target.GetStatus() != Status.DEAD)
                        {
                            UseItem("Potion", target);
                        }
                        else
                        {
                            _companions.Remove(target);
                            Console.WriteLine("Farewell, " + target.GetName() + "...");
                        }
                    }
                    return true;
                }
            }

            return false;
        }

        public bool CheckSuccess(string name)
        {
            foreach (Creature companion in _companions)
            {
                if (companion.GetName() == name)
                {
                    return true;
                }
            }

            return false;
        }
    }
}