using System.Collections;

namespace Cave
{
    public class Combat : IFeature
    {
        private List<Creature> _monsters;
        private bool _fought;

        public Combat()
        {
            _monsters = new();

            _fought = false;
        }

        public void SetMonsters(List<Creature> monsters)
        {
            _monsters = monsters;
        }

        public void AddMonster(Creature monster)
        {
            _monsters.Add(monster);
        }

        public void Trigger(List<Creature> party)
        {
            if (_monsters.Count == 0)
            {
                Console.WriteLine("What a relief! No monsters here.");
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
            Console.WriteLine("There are monsters, here. It's time to fight!");
            Initiative.Clear();
            Initiative.AddToInitiative(party);
            Initiative.AddToInitiative(_monsters);

            Creature next;
            while (party[0].GetStatus() != Status.DEAD && CheckMonsters())
            {
                next = Initiative.Next();
                if (next.GetStatus() != Status.OK)
                {
                    if (_monsters.Contains(next)) {
                        //TODO
                        _monsters.Remove(next);
                    }
                    continue;
                }
            }
            Console.WriteLine("You won the fight, well done!");
            _fought = true;
            //TODO
        }

        protected bool CheckMonsters()
        {
            //TODO
            return _monsters.Count > 0;
        }

        protected void Aftermath()
        {
            Console.WriteLine("You won a battle here against:");
            foreach (Creature monster in _monsters)
            {
                Console.WriteLine(monster.GetName());
            }
            Console.WriteLine("Their bodies lay here still, growing cold.");
            //TODO
        }
    }
}