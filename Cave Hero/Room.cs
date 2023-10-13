namespace Cave
{
    public class Room
    {
        private bool _captorFlag = false;

        protected Dictionary<string, Room> Paths;
        protected Encounter Enc;

        protected Room()
        {
            Paths = new Dictionary<string, Room>(); Enc = new Encounter();
        }

        public Room(Room last, Encounter e)
        {
            Paths = new Dictionary<string, Room>() { { "Back", last } };
            Enc = e;
        }

        public bool AddPath(string dir, Room room)
        {
            if (Paths.ContainsKey(dir))
            {
                return false;
            }

            Paths.Add(dir, room);
            return true;
        }

        public virtual Encounter Enter()
        {
            return Enc;
        }

        public Room? ChoosePath()
        {
            try
            {
                Console.WriteLine("Where to next?");
                while (true)
                {
                    //TODO universal text entry
                    OfferPaths();
                    string? dir = Console.ReadLine();
                    if (dir != null)
                    {
                        if (Paths.ContainsKey(dir))
                        {
                            return Paths[dir];
                        }
                        else
                        {
                            Console.Write("'" + dir + "' is not a valid option.");
                        }
                    }
                }
            }
            catch (IOException e)
            {
                TextWriter errorWriter = Console.Error;
                errorWriter.WriteLine(e.Message);
                return null;
            }
        }

        private void OfferPaths()
        {
            foreach (KeyValuePair<string, Room> path in Paths)
            {
                Console.Write("[" + path.Key + "] ");
            }
            Console.WriteLine();
        }

        public int GenPaths(int rooms)
        {
            //TODO random decide direction too
            rooms = GenDir("Left", rooms);
            Console.Write("rooms: " + rooms);
            rooms = GenDir("Straight", rooms);
            Console.Write("rooms: " + rooms);
            rooms = GenDir("Right", rooms);
            Console.Write("rooms: " + rooms);

            return rooms;
        }

        private int GenDir(string dir, int rooms)
        {
            if (Paths.ContainsKey(dir) || rooms <= 0)
            {
                return rooms;
            }

            Random rnd = new();
            if (rnd.Next(0, 4) > 1)
            {
                rooms--;
                //TODO more robust event generation/room selection
                Encounter nEnc = (rnd.Next(0, 2) == 1) ? Atk(rnd) : new SimpleTrap();
                Room nRoom = new(this, nEnc);

                Paths.Add(dir, nRoom);
                return nRoom.GenPaths(rooms);
            }

            return rooms;
        }

        private Encounter Atk(Random rnd)
        {
            if (_captorFlag)
            {
                return new MonsterAttack();
            }

            //TODO improve this
            Creature captive = new("Sibling", 2, 0, 2);
            _captorFlag = true;
            return (rnd.Next(0, 2) == 1) ? new MonsterAttack() : new CaptorAttack(captive);
        }
    }
}