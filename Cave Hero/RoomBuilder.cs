namespace Cave
{
    public class RoomBuilder
    {

        public Room CreateRoom()
        {
            Room room = new();
            //Add Features
            return room;
        }

        public Room CreateEntrance(Dir dir) {
            Room entrance = new();

            entrance.AddPath(dir, CreateExit());

            return entrance;
        }

        public Room CreateExit() {
            Room exit = new("EXIT");
            exit.AddFeature(new Escape());
            return exit;
        }
    }
} 