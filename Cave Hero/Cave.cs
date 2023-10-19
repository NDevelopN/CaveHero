namespace Cave
{
    public class Cave {
        protected struct Coord
        {
            public int X;
            public int Y;

            public Coord(int x, int y)
            {
                X = x;
                Y = y;
            }

            public override string ToString()
            {
                return "(" + X + "," + Y + ")";
            }
        }

        private RoomBuilder _builder;

        private Dictionary<Coord, Room> _grid;
        private Room? _entrance;

        private int _PATHMOD = 10;

        protected int MaxX, MaxY;
        protected int Size;

        public Cave(int x, int y, int size)
        {
            _grid = new Dictionary<Coord, Room>();
            MaxX = x;
            MaxY = y;

            Size = size;

            _builder = new RoomBuilder();
        }

        public Room? Generate()
        {
            Stack<Coord> stack = new();

            int roomCount = 0;
            Coord curLoc = SelectEntrance();

            stack.Push(curLoc);

            while (stack.Count > 0)
            {
                curLoc = stack.Pop();

                Room room = _grid[curLoc];
                int pCount = room.GetPCount();

                //While all 4 paths have not been accounted for, and while there are more rooms to add
                while (pCount < 4 && roomCount < Size)
                {

                    Random rnd = new();
                    int res = rnd.Next(0, 100);
                    int rChance = RoomChance(room.GetPCount(), roomCount);
                    if (res > rChance)
                    {
                        pCount++;
                        continue;
                    }

                    Coord nextLoc = ContinuePath(curLoc);

                    if (nextLoc.Equals(curLoc))
                    {
                        continue;
                    }

                    Room nRoom;

                    if (_grid.ContainsKey(nextLoc))
                    {
                        nRoom = _grid[nextLoc];
                        pCount++;
                    }
                    else
                    {
                        roomCount++;

                        nRoom = _builder.CreateRoom("(" + nextLoc.X + ", " + nextLoc.Y + ")");
                        _grid.Add(nextLoc, nRoom);

                        stack.Push(nextLoc);
                    }

                    if (curLoc.X == nextLoc.X)
                    {
                        if (curLoc.Y < nextLoc.Y)
                        {
                            room.AddPath(Dir.NORTH, nRoom);
                            nRoom.AddPath(Dir.SOUTH, room);
                        }
                        else
                        {
                            room.AddPath(Dir.SOUTH, nRoom);
                            nRoom.AddPath(Dir.NORTH, room);
                        }
                    }
                    else
                    {
                        if (curLoc.X > nextLoc.X)
                        {
                            room.AddPath(Dir.WEST, nRoom);
                            nRoom.AddPath(Dir.EAST, room);
                        }
                        else
                        {
                            room.AddPath(Dir.EAST, nRoom);
                            nRoom.AddPath(Dir.WEST, room);
                        }
                    }

                    pCount++;
                    continue;
                }
            }

            if (roomCount < Size)
            {
                Console.WriteLine("Did not generate all intended rooms: " + roomCount + "/" + Size);
            }

            CreateMap();

            return _entrance;
        }

        private void CreateMap()
        {
            List<List<string>> map = new();
            List<string> row = new();

            for (int i = 0; i <= MaxY; i++)
            {
                row = new();
                for (int j = 0; j <= MaxX; j++)
                {
                    row.Add("[/]");
                }
                map.Add(row);
            }

            foreach (KeyValuePair<Coord, Room> cell in _grid)
            {
                Coord coord = cell.Key;
                row = map[coord.Y];
                row[coord.X] = "[" + cell.Value.GetTopFeature() + "]";
                map[coord.Y] = row;
            }

            Console.Write("  ");
            for (int x = 0; x <= MaxX; x++) {
                Console.Write(" " + x + " ");
            }
            Console.WriteLine();

            for (int y = MaxY; y >= 0; y--)
            {
                row = map[y];
                Console.Write(y + "|");
                for (int x = 0; x <= MaxX; x++)
                {
                    Console.Write(row[x]);
                }
                Console.WriteLine();
            }
        }

        protected Coord SelectEntrance()
        {
            int x, y;
            Coord entCoord;

            Random rnd = new();
            Dir dir = (Dir)rnd.Next(0, 4);
            switch (dir)
            {
                case Dir.WEST:
                    x = 0;
                    y = rnd.Next(0, MaxY + 1);
                    entCoord = new Coord(x, y);
                    break;
                case Dir.NORTH:
                    x = rnd.Next(0, MaxX + 1);
                    y = 0;
                    entCoord = new Coord(x, y);
                    break;
                case Dir.EAST:
                    x = MaxX;
                    y = rnd.Next(0, MaxY + 1);
                    entCoord = new Coord(x, y);
                    break;
                case Dir.SOUTH:
                    x = rnd.Next(0, MaxX + 1);
                    y = MaxY;
                    entCoord = new Coord(x, y);
                    break;
                default:
                    Console.WriteLine("[ERROR] Invalid starting direction: " + dir);
                    x = 0;
                    y = rnd.Next(0, MaxY + 1);
                    entCoord = new Coord(x, y);
                    break;
            }

            _entrance = _builder.CreateEntranceRoom(dir);
            _grid.Add(entCoord, _entrance);

            return entCoord;
        }

        protected Coord ContinuePath(Coord curLoc)
        {
            Random rnd = new();

            int res = rnd.Next(0, 4);

            Coord nextCoord;
            switch (res)
            {
                case 0:
                    //West
                    if (curLoc.X <= 0)
                    {
                        return curLoc;
                    }
                    nextCoord = new Coord(curLoc.X - 1, curLoc.Y);
                    break;
                case 1:
                    //North
                    if (curLoc.Y >= MaxY)
                    {
                        return curLoc;
                    }
                    nextCoord = new Coord(curLoc.X, curLoc.Y + 1);
                    break;
                case 2:
                    //East
                    if (curLoc.X >= MaxX)
                    {
                        return curLoc;
                    }
                    nextCoord = new Coord(curLoc.X + 1, curLoc.Y);
                    break;
                case 3:
                    //South
                    if (curLoc.Y <= 0)
                    {
                        return curLoc;
                    }
                    nextCoord = new Coord(curLoc.X, curLoc.Y - 1);
                    break;
                default:
                    Console.WriteLine("Invalid direction: " + res);
                    nextCoord = curLoc;
                    break;
            }

            return nextCoord;
        }

        /**
         *  Generate a probability of creating a new room given
         *      -The number of existing paths from the current room
         *      -The percentage of rooms yet to be created 
         *  The result should be higher chances for early rooms and rooms with fewer paths
        **/
        protected int RoomChance(int pCount, int roomCount)
        {
            int chance = 70;
            int pChance = _PATHMOD * pCount;
            int rChance = (int)(decimal.Divide(Size - roomCount, Size) * 50);

            int total = chance - pChance + rChance;

            return total;
        }
    }
}