namespace Cave
{
    public class Cave
    {
        private RoomFactory _rFactory;

        private Dictionary<string, RoomTypeVals> _rtv;

        private double _trapPC = .5;
        private double _combatPC = .25;
        private double _treasurePC = .15;
        private double _hostagePC = 1;

        private int _trapMax;
        private int _combatMax;
        private int _treasureMax;
        private int _hostageMax = 1;

        private int _roomCount = 0;
        private int _tarRoomCount;

        private int _maxX;
        private int _maxY;

        private Dictionary<Coord, Room> _grid;
        private Room? _entrance;

        public Cave(int x, int y, int trc)
        {
            _maxX = x;
            _maxY = y;
            _tarRoomCount = trc;

            _trapMax = (int)(trc * _trapPC);
            _combatMax = (int)(trc * _combatPC);
            _treasureMax = (int)(trc * _treasurePC);

            _rtv = new()
            {
                { "Trap", new RoomTypeVals(_trapPC, 0, _trapMax) },
                { "Combat", new RoomTypeVals(_combatPC, 0, _combatMax) },
                { "Treasure", new RoomTypeVals(_treasurePC, 0, _treasureMax) },
                { "Hostage", new RoomTypeVals(_hostagePC, 0, _hostageMax) }
            };

            _grid = new();

            _rFactory = new();
        }

        public Room? Generate()
        {
            Stack<Coord> stack = new();
            stack.Push(SelectEntrance());

            ExpandRoom(stack);

            while (_roomCount < _tarRoomCount)
            {
                Random rnd = new();
                Coord nLoc;
                do
                {
                    nLoc = new(rnd.Next(0, _maxX), rnd.Next(0, _maxY));
                } while (!_grid.ContainsKey(nLoc));

                stack.Push(nLoc);

                ExpandRoom(stack);
            }

            CreateMap();

            return _entrance;
        }

        private void ExpandRoom(Stack<Coord> stack)
        {
            while (stack.Count > 0)
            {
                Coord curLoc = stack.Pop();

                Room room = _grid[curLoc];
                int pCount = room.GetPCount();

                //While all 4 paths have not been accounted for, and while there are more rooms to add
                while (pCount < 4 && _roomCount < _tarRoomCount)
                {
                    Random rnd = new();
                    int res = rnd.Next(0, 100);
                    int rChance = RoomChance(room.GetPCount());
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

                    pCount++;
                    Room nRoom;

                    if (_grid.ContainsKey(nextLoc))
                    {
                        nRoom = _grid[nextLoc];
                    }
                    else
                    {
                        _roomCount++;

                        nRoom = NextRoom();
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
        }

        private Room NextRoom()
        {
            RoomTypeVals htv = _rtv["Hostage"];
            if (_roomCount == _tarRoomCount - 1 && htv.FCount < htv.Max)
            {
                htv.FCount++;
                _rtv["Hostage"] = htv;
                return _rFactory.Create("Hostage");
            }

            foreach (KeyValuePair<string, RoomTypeVals> rtv in _rtv)
            {
                RoomTypeVals ctv = rtv.Value;
                if (CheckChance(ctv))
                {
                    ctv.FCount++;
                    _rtv[rtv.Key] = ctv;

                    return _rFactory.Create(rtv.Key);
                }
            }

            return _rFactory.Create("Empty");
        }

        private bool CheckChance(RoomTypeVals rtv)
        {
            if (rtv.FCount >= rtv.Max)
            {
                return false;
            }
            else if (rtv.Percent == 1)
            {
                return true;
            }

            int floor = (int)(((rtv.Max * rtv.FCount) / _roomCount) * 10);
            int nr = new Random().Next(floor, 100);
            if (nr > (int)(rtv.Percent * 100))
            {
                return false;
            }

            return true;
        }

        private Coord SelectEntrance()
        {
            int x,
                y;
            Coord entCoord;

            Random rnd = new();
            Dir dir = (Dir)rnd.Next(0, 4);
            switch (dir)
            {
                case Dir.WEST:
                    x = 0;
                    y = rnd.Next(0, _maxY + 1);
                    entCoord = new Coord(x, y);
                    break;
                case Dir.NORTH:
                    x = rnd.Next(0, _maxX + 1);
                    y = 0;
                    entCoord = new Coord(x, y);
                    break;
                case Dir.EAST:
                    x = _maxX;
                    y = rnd.Next(0, _maxY + 1);
                    entCoord = new Coord(x, y);
                    break;
                case Dir.SOUTH:
                    x = rnd.Next(0, _maxX + 1);
                    y = _maxY;
                    entCoord = new Coord(x, y);
                    break;
                default:
                    Console.WriteLine("[ERROR] Invalid starting direction: " + dir);
                    x = 0;
                    y = rnd.Next(0, _maxY + 1);
                    entCoord = new Coord(x, y);
                    break;
            }

            _entrance = _rFactory.CreateEntrance(dir);
            _grid.Add(entCoord, _entrance);

            return entCoord;
        }

        private Coord ContinuePath(Coord curLoc)
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
                    if (curLoc.Y >= _maxY)
                    {
                        return curLoc;
                    }
                    nextCoord = new Coord(curLoc.X, curLoc.Y + 1);
                    break;
                case 2:
                    //East
                    if (curLoc.X >= _maxX)
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
                    Console.WriteLine("[ERROR] Invalid direction: " + res);
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
        private int RoomChance(int pCount)
        {
            int chance = 70;
            int pChance = 10 * pCount;
            int rChance = (int)(decimal.Divide(_tarRoomCount - _roomCount, _tarRoomCount) * 50);

            int total = chance - pChance + rChance;

            return total;
        }

        private void CreateMap()
        {
            List<List<string>> map = new();
            List<string> row = new();

            for (int i = 0; i <= _maxY; i++)
            {
                row = new();
                for (int j = 0; j <= _maxX; j++)
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

            string mapString = "   ";
            for (int x = 0; x <= _maxX; x++)
            {
                mapString += " " + x + " ";
            }
            mapString += "\n";

            for (int y = _maxY; y >= 0; y--)
            {
                row = map[y];
                mapString += y + "|";
                for (int x = 0; x <= _maxX; x++)
                {
                    mapString += row[x];
                }
                mapString += "\n";
            }

            Game.IO.WriteMsg(mapString);
        }
    }
}