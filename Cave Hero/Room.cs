namespace Cave
{
    public class Room
    {
        private string _name;

        protected Pathways Paths;
        protected List<IFeature> Features;

        public Room()
        {
            Paths = new();
            Features = new List<IFeature>();
            _name = MakeName();
        }

        //TODO remove this
        private string MakeName()
        {
            Random rnd = new();
            int res = rnd.Next(1000, 9999);
            return "Room [" + res + "]";
        }

        public string GetName()
        {
            return _name;
        }

        public void SetName(string name){
            _name = name;
        }

        public void AddPath(Dir dir, Room room)
        {
            if (!Paths.SetPath(dir, room))
            {
                Console.WriteLine("Attempted to create room where there was already a path.");
            }
            else
            {
                Console.WriteLine("Added " + room.GetName() + " to " + dir.ToString() + " of " + _name);
            }
        }

        public int GetPCount()
        {
            return Paths.GetPCount();
        }

        public Room? ChoosePath()
        {
            try
            {
                Console.WriteLine("You are in " + _name);
                Console.WriteLine("Where to next?");
                while (true)
                {
                    //TODO universal text entry
                    Console.Write(Paths.PrintOut());
                    string? dir = Console.ReadLine();
                    if (dir == null)
                    {
                        Console.WriteLine("No input received.");
                        continue;
                    }

                    dir = dir.ToUpper();

                    return dir switch
                    {
                        "WEST" => Paths.GetPath(Dir.WEST),
                        "NORTH" => Paths.GetPath(Dir.NORTH),
                        "EAST" => Paths.GetPath(Dir.EAST),
                        "SOUTH" => Paths.GetPath(Dir.SOUTH),
                        "ESCAPE" => Paths.GetExit(),
                        _ => null,
                    };
                }
            }
            catch (IOException e)
            {
                TextWriter errorWriter = Console.Error;
                errorWriter.WriteLine(e.Message);
                return null;
            }
        }

        public void AddFeature(IFeature feature)
        {
            Features.Add(feature);
        }

        public string GetTopFeature() {
            if (_name == "Entrance") return "E";
            if (_name == "EXIT") return "e";

            int top = 0;
            foreach (IFeature feature in Features)
            {
                if (feature.GetType().Name == "Hostage")
                {
                    return "H";
                }
                else if (feature.GetType().Name == "Combat")
                {
                    top = 3;
                }
                else if (feature.GetType().Name == "Trap")
                {
                    if (top < 2)
                    {
                        top = 2;
                    }
                }
                else if (feature.GetType().Name == "Treasure")
                {
                    if (top < 1)
                    {
                        top = 1;
                    }
                }
            }

            return top switch
            {
                3 => "C",
                2 => "T",
                1 => "$",
                _ => " ",
            };
        }

        public void Enter(Hero hero)
        {
            Console.WriteLine("Entering " + _name);
            foreach (IFeature feature in Features)
            {
                Console.WriteLine("Feature: " + feature.GetType().Name);
                feature.Trigger(hero.GetParty());
                if (hero.GetStatus() == Status.DEAD)
                {
                    return;
                }
            }
        }
    }
}