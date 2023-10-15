namespace Cave
{
    public class Room
    {
        protected struct RoomPaths
        {
            private Dictionary<Dir, Room?> _paths;
            private int _pCount;

            public RoomPaths()
            {
                _paths = new Dictionary<Dir, Room?> {
                    {Dir.WEST, null},
                    {Dir.NORTH, null},
                    {Dir.EAST, null},
                    {Dir.SOUTH, null}
                };

                _pCount = 0;
            }

            public bool SetPath(Dir dir, Room room)
            {
                if (_paths[dir] != null)
                {
                    return false;
                }

                _paths[dir] = room;
                _pCount++;
                return true;
            }

            public Room? GetPath(Dir dir)
            {
                return _paths[dir];
            }

            public Room? GetExit()
            {
                foreach (KeyValuePair<Dir, Room?> path in _paths)
                {
                    if (path.Value is Exit)
                    {
                        return path.Value;
                    }
                }

                return null;
            }

            public string PrintOut()
            {
                string options = "Paths: ";

                foreach (KeyValuePair<Dir, Room?> path in _paths)
                {
                    if (path.Value == null)
                    {
                        continue;
                    }

                    if (path.Value is Exit)
                    {
                        options += "[ESCAPE] ";
                    }
                    else
                    {
                        options += "[" + path.Key.ToString() + "] ";
                    }
                }

                options += "\n";

                return options;
            }

            public int GetPCount()
            {
                return _pCount;
            }
        }

        private string _name;

        private bool _captorFlag = false;

        protected RoomPaths Paths;
        protected Encounter Enc;

        public Room()
        {
            Paths = new(); ;
            Enc = new Encounter();
            _name = MakeName();
        }

//TODO remove this
        private string MakeName() {
            Random rnd = new();
            int res = rnd.Next(1000, 9999);
            return "Room [" + res +"]";

        }
        
        public string GetName() {
            return _name;
        }

        public void AddPath(Dir dir, Room room) {
            if (!Paths.SetPath(dir, room)) {
                Console.WriteLine("Attempted to create room where there was already a path.");
            } else {
                Console.WriteLine("Added " + room.GetName() + " to " + dir.ToString() + " of " + _name);
            }
        }

        public int GetPCount()
        {
            return Paths.GetPCount();
        }

        public virtual Encounter Enter()
        {
            return Enc;
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
                    if (dir == null) {
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
    }
}