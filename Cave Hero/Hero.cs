namespace Cave
{
    public class Hero : Creature
    {
        private int _maxCompanions = 3;

        private List<Creature> _party;

        public Hero(string name, int hp, Die atk, Die spd, int maxCompanions) :
                    base(name, hp, atk, spd)
        {
            AddItem(new Potion(6, 1, 1));
            _party = new();
            _maxCompanions = maxCompanions;
            _party = new List<Creature> { this };
        }

        public void UseItem(string name)
        {
            UseItem(name, this);
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

        public override void DoCombat(List<Creature> allies, List<Creature> enemies)
        {
            Console.WriteLine("Potential Targets");
            foreach (Creature target in enemies)
            {
                if (target.GetStatus() == Status.OK)
                {
                    Console.Write("[" + target.GetName() + "]");
                }
            }

            while (true)
            {
                string? selection = Console.ReadLine();
                if (selection != null)
                {
                    foreach (Creature target in enemies)
                    {
                        if (target.GetName() == selection)
                        {
                            target.Damage(GetAtk());
                            return;
                        }
                    }
                }

                Console.WriteLine("Invalid target '" + selection + "'.");
            }
        }

        public override void Damage(int val)
        {
            base.Damage(val);
            if (Status != Status.DEFEATED)
            {
                if (Hp <= 5)
                {
                    Console.WriteLine("Health is low, using Potion.");
                    UseItem("Potion");
                }
            }
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

        public List<Creature> GetParty()
        {
            return _party;
        }
    }
}