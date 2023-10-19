namespace Cave
{
    public class RoomBuilder
    {

        private Dictionary<string, FeatureChance> chances;

        private FeatureChance trapChance = new(50, 50);
        private FeatureChance combatChance = new(50, 70, 10,
                new List<Type>(),
                new List<Type> { typeof(Trap) });
        private FeatureChance treasureChance = new(25, 25, 20,
                new List<Type>(),
                new List<Type> { typeof(Combat), typeof(Trap) });
        private FeatureChance hostageChance = new FeatureChance(10, 100, 20,
                new List<Type> { typeof(Combat) },
                new List<Type> { typeof(Trap), typeof(Treasure) });

        private CreatureDirector _cDir;
        private CreatureBuilder _cBuild;


        public RoomBuilder()
        {

            _cDir = new();
            _cBuild = new();

            chances = new Dictionary<string, FeatureChance>
            {
                { "Trap", trapChance },
                { "Combat", combatChance },
                { "Treasure", treasureChance },
                { "Hostage", hostageChance }
            };
        }

        public Room CreateRoom()
        {
            Room room = new();
            List<IFeature> features = new();
            IFeature? nFeature = null;
            Random rnd = new();
            foreach (KeyValuePair<string, FeatureChance> fc in chances)
            {
                int chance = fc.Value.GetChance(features);
                int rand = rnd.Next(0, 100);
                Console.WriteLine(fc.Key + " chance: " + chance + " / " + rand);

                if (chance > rand)
                {
                    //if (fc.Value.GetChance(features) > rnd.Next(0, 100)) {
                    switch (fc.Key)
                    {
                        case "Trap":
                            nFeature = CreateTrap();
                            break;
                        case "Combat":
                            nFeature = CreateCombat();
                            break;
                        case "Treasure":
                            nFeature = CreateTreasure();
                            break;
                        case "Hostage":
                            nFeature = CreateHostage();
                            break;
                    }

                    fc.Value.IncreaseFeatureCount();

                    if (nFeature != null)
                    {
                        features.Add(nFeature);
                        room.AddFeature(nFeature);
                        nFeature = null;
                    }
                }

                fc.Value.IncreaseRoomCount();

            }
            return room;
        }

        public Room CreateRoom(string name)
        {
            Room room = CreateRoom();
            room.SetName(name);
            return room;
        }

        protected Trap CreateTrap()
        {
            //TODO
            Die dmg = new(4, 1);
            Die spd = new(2, 2);
            Die count = new(6, 1);

            return new Trap(dmg, spd, count);
        }

        protected Combat CreateCombat()
        {
            //TODO
            return new Combat();
        }
        protected Treasure CreateTreasure()
        {
            //TODO
            List<Item> haul = new() { new Potion(6, 1, 1) };
            return new Treasure(haul);

        }
        protected Hostage CreateHostage()
        {
            //TODO
            Creature hostage = _cDir.CreateHostage(_cBuild, null); 
            return new Hostage(hostage);
        }

        public Room CreateEntranceRoom(Dir dir)
        {
            Room entrance = new();
            entrance.SetName("Entrance");

            entrance.AddPath(dir, CreateExitRoom());

            return entrance;
        }

        public Room CreateExitRoom()
        {
            Room exit = new();
            exit.SetName("EXIT");
            exit.AddFeature(new Escape());
            return exit;
        }
    }
}