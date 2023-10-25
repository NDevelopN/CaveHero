using System.Collections;
using Server;

namespace Cave
{
    public class Combat : IFeature
    {
        private CreatureDirector _cDir;
        private CreatureBuilder _cBuild;
        private List<Creature> _monsters;
        private int _monCount;

        private bool _fought;


        public Combat(double maxPower)
        {
            _cDir = new();
            _cBuild = new();

            _monsters = new();
            _monCount = 0;

            GenerateMonsters(maxPower);
        }

        private void GenerateMonsters(double maxPower)
        {
            double power = 0;
            while (power < maxPower)
            {
                Creature monster = _cDir.CreateRandomMonster(_cBuild);
                double curPwr = power + monster.GetPower();
                if (curPwr > maxPower + 1)
                {
                    power++;
                    continue;
                }

                power = curPwr;

                _monsters.Add(monster);
                _monCount++;
            }
        }

        public void Trigger(List<Creature> party)
        {
            if (_monsters.Count == 0)
            {
                IOBuffer.WriteMsg("What a relief! No monsters here.");
                return;
            }

            if (_fought)
            {
                Aftermath();
            }
            else
            {
                Fight(party);
            }
        }

        public string Mention()
        {
            return "There are monsters in this room, ready to fight!";
        }

        protected void Fight(List<Creature> party)
        {
            IOBuffer.WriteMsg("There are monsters, here. It's time to fight!");
            Initiative.Clear();
            Initiative.AddToInitiative(party);
            Initiative.AddToInitiative(_monsters);

            Creature next;
            while (party[0].GetStatus() != Status.DEFEATED && CheckMonsters())
            {
                next = Initiative.Next();
                Status status = next.GetStatus();
                if (status == Status.OK)
                {
                    if (_monsters.Contains(next))
                    {
                        next.DoCombat(_monsters, party);
                    }
                    else
                    {
                        next.DoCombat(party, _monsters);
                    }
                }
            }

            if (party[0].GetStatus() != Status.OK)
            {
                IOBuffer.WriteMsg("You fall in battle, another victim of the Cave.");
            }
            else
            {
                IOBuffer.WriteMsg("You won the fight, well done!");
                IOBuffer.WriteMsg("State of your party: ");
                foreach (Creature member in party)
                {
                    IOBuffer.WriteMsg(member.GetName() + ": " + member.GetHP());
                }
                _fought = true;
            }
        }

        protected bool CheckMonsters()
        {
            foreach (Creature monster in _monsters)
            {
                if (monster.GetStatus() == Status.DEFEATED)
                {
                    monster.SetStatus(Status.DEAD);
                    _monCount--;
                }
            }

            return _monCount > 0;
        }

        protected void Aftermath()
        {
            IOBuffer.WriteMsg("You won a battle here against:");
            foreach (Creature monster in _monsters)
            {
                IOBuffer.WriteMsg(monster.GetName());
            }
            IOBuffer.WriteMsg("Their bodies lay here still, growing cold.");
            //TODO
        }
    }
}