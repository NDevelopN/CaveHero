namespace Cave {
    class Combat : IFeature
    {
        private List<Creature> _monsters;

        private bool _fought;

        public Combat() {
            _monsters = new List<Creature>();
            _fought = false;
        }

        public void SetMonsters(List<Creature> monsters) {
            _monsters = monsters;
        }

        public void AddMonster(Creature monster) {
            _monsters.Add(monster);
        }


        public void Trigger(List<Creature> party)
        {
            if (_monsters.Count == 0) {
                return;
            }

            if (_fought)
            {
                Aftermath();
            } else {
                Fight(party);
            }
        }

        public string Mention() {
            return "There are monsters in this room, ready to fight!";
        }

        protected void Fight(List<Creature> party) {
            Console.WriteLine("This is where combat would have happened");
            _fought = true;
            //TODO
        }

        protected void Aftermath() {
            Console.WriteLine("You won a battle here against:");
            foreach (Creature monster in _monsters) {
                Console.WriteLine(monster.GetName());
            }
            Console.WriteLine("Their bodies lay here still, growing cold.");
            //TODO
        }
    }

}