namespace Cave
{
    public class Hero : Creature
    {
        private int _maxCompanions = 3;

        private List<Creature> _party;

        public Hero() : base()
        {
            Name = "Hero";
            Hp = 3;
            Atk = 1;
            Spd = new Die(4, 1);
            Items = new Dictionary<string, Item>() { { "Potion", new Potion(6, 1, 1) } };
            _party = new List<Creature> { this };
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

        public bool CollectItem(Item item)
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
                    return false;
                }
            }
            else
            {
                Console.WriteLine("New item " + name + " added to Inventory.");
                Items.Add(name, item);
            }

            return true;
        }

        public bool Join(Creature companion)
        {
            if (_party.Count < _maxCompanions + 1)
            {
                _party.Add(companion);
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
            int cCount = _party.Count;
            if (cCount > 0)
            {
                Random rnd = new();
                int i = rnd.Next(0, cCount);
                Creature target = _party[i];

                //TODO add some stance mechanic to change how this works
                if (Spd.Roll() < target.RollSpd(1))
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
                            _party.Remove(target);
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
            foreach (Creature companion in _party)
            {
                if (companion.GetName() == name)
                {
                    return true;
                }
            }

            return false;
        }

        public List<Creature> GetParty() {
            return _party;
        }
    }
}