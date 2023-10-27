using Server;

namespace Cave
{
    public class Hero : Creature
    {
        private int _maxCompanions = 3;

        private List<Creature> _party;

        public Hero(string name, int hp, Die atk, Die spd, int maxCompanions)
            : base(name, hp, atk, spd)
        {
            AddItem(new Potion(4, 2, 3));
            _party = new();
            _maxCompanions = maxCompanions;
            _party = new List<Creature> { this };
        }

        public bool UseItem(string name)
        {
            return UseItem(name, this);
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
            List<string> options = new();

            foreach (Creature target in enemies)
            {
                if (target.GetStatus() == Status.OK)
                {
                    options.Add(target.GetName());
                }
            }

            Game.IO.WriteOption("Potential Targets", options);

            while (true)
            {
                Message selection = Game.IO.NextInput();
                string text = selection.Text;

                foreach (Creature target in enemies)
                {
                    if (target.GetName() == text)
                    {
                        target.Damage(GetAtk());
                        return;
                    }
                }
                Game.IO.WriteMsg("Invalid target '" + text + "'.");
                return;
            }
        }

        public override void Damage(int val)
        {
            base.Damage(val);
            if (Status != Status.DEFEATED)
            {
                if (Hp <= 5)
                {
                    Game.IO.WriteMsg("Health is low, time to use a Potion.");
                    if (!UseItem("Potion")) {
                        Game.IO.WriteMsg("Oh no! There are not Potions left!");
                    };
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