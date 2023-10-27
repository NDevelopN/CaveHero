using CaveHero.CHDie;
using CaveHero.CHItem;
using CaveHero.CHRoom.Feature;

namespace CaveHero.CHRoom
{
    public class RoomFactory
    {
        public Room Create(string type)
        {
            Room room = new();
            switch (type)
            {
                case "Trap":
                    room.AddFeature(CreateTrap());
                    break;
                case "Combat":
                    room.AddFeature(CreateCombat());
                    break;
                case "Hostage":
                    //Always combat in hostage room
                    room.AddFeature(CreateCombat());
                    room.AddFeature(CreateHostage());
                    break;
                case "Treasure":
                    room.AddFeature(CreateTreasure());
                    break;
                case "Empty":
                    break;
            }

            return room;
        }

        public Room CreateEntrance(Dir dir) {
            Room entrance = new();
            entrance.SetName("Entrance");

            entrance.AddPath(dir, CreateExit());

            return entrance;
        }

        public Room CreateExit() {
            Room exit = new();
            exit.SetName("EXIT");
            exit.AddFeature(new Escape());
            return exit;
        }



//TODO add some customization to creation
        private IFeature CreateTrap()
        {
            Die dmg = new(4, 1);
            Die spd = new(2, 2);
            Die count = new(6, 1);

            return new Trap(dmg, spd, count);
        }

        private IFeature CreateCombat()
        {
            return new Combat(40);
        }

        private IFeature CreateHostage()
        {
            return new Hostage();
        }

        private IFeature CreateTreasure()
        {
            List<Item> haul = new() { new Potion(6, 1, 1) };
            return new Treasure(haul);
        }
    }
} 