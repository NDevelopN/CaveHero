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

        public override void Damage(int val)
        {
            if (CompanionDamage(val)) { return; }

            base.Damage(val);
            if (Status != Status.DEAD)
            {
                if (Hp <= 5)
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
                if (Spd.Roll() < target.RollSpd())
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

        public List<Creature> GetParty()
        {
            return _party;
        }
    }
}