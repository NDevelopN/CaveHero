using Server;

namespace Cave
{
    public class Game 
    {
        [ThreadStatic] public static Server.IOBuffer IO;

        private Hero CreateHero()
        {
            HeroBuilder builder = new()
            {
                Name = "Hero",
                HP = 20,
                Atk = new Die(6, 2),
                Spd = new Die(3, 3),
                MaxCompanions = 3
            };

            return builder.Build();
        }

        public void Start(IOBuffer io)
        {
            IO = io;
            Hero hero = CreateHero();

            Game.IO.WriteMsg("Welcome to the Cave!");
            Game.IO.WriteMsg("Nasty monsters took your sibling into this Cave.");
            Game.IO.WriteMsg("You must hurry to save them, before it's too late!");

            Cave cave = new(6, 6, 12);
            Room? ent = cave.Generate();
            if (ent == null)
            {
                Console.WriteLine("Entrance returned was null");
                Environment.Exit(1);
            }

            Room? curRoom = ent;
            while (hero.GetStatus() != Status.DEFEATED && hero.GetStatus() != Status.WIN)
            {
                curRoom = curRoom.ChoosePath();
                if (curRoom == null)
                {
                    return;
                }

                curRoom.Enter(hero);
            }

            string endMsg = (hero.GetStatus() == Status.WIN) ? "Congratulations, Cave Hero!" : "Too bad, you lose!";

            Thread.Sleep(1000);
            Game.IO.WriteMsg(endMsg);
            Thread.Sleep(1000);
        }
    }
}