namespace Cave
{
    public struct Pathways
    {
        private Dictionary<Dir, Room?> _paths;
        private int _pCount;

        public Pathways()
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
                if (path.Value != null && path.Value.GetName() == "EXIT")
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

                if (path.Value.GetName() == "EXIT")
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
}