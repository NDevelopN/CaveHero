using Server;

namespace Cave
{
    public class CaveHero
    {

        private static Hero CreateHero()
        {
            HeroBuilder builder = new()
            {
                Name = "Hero",
                HP = 12,
                Atk = new Die(3, 2),
                Spd = new Die(3, 3),
                MaxCompanions = 3
            };

            return builder.Build();
        }

        public static void Game()
        {
            Hero hero = CreateHero();

            IOBuffer.WriteMsg("Welcome to the Cave!");
            IOBuffer.WriteMsg("Nasty monsters took your sibling into this Cave.");
            IOBuffer.WriteMsg("You must hurry to save them, before it's too late!");

            Cave cave = new(6, 6, 12);
            Room? ent = cave.Generate();
            if (ent == null)
            {
                IOBuffer.WriteMsg("Entrance returned was null");
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
            IOBuffer.WriteMsg(endMsg);
            Thread.Sleep(1000);
        }
    }
}