using CaveHero.Server;

using CaveHero.CHCreature;
using CaveHero.CHCave;
using CaveHero.CHDie;
using CaveHero.CHRoom;


namespace CaveHero
{
    public class Game 
    {
        [ThreadStatic] public static IOBuffer IO;

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

        public void Start(IOBuffer io, CancellationToken token)
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
                return;
            }

            Room? curRoom = ent;
            while (hero.GetStatus() != Status.DEFEATED && hero.GetStatus() != Status.WIN)
            {
                if (token.IsCancellationRequested) {
                    token.ThrowIfCancellationRequested();
                }
                curRoom = curRoom.ChoosePath();
                if (curRoom == null)
                {
                    return;
                }

                curRoom.Enter(hero);
            } 

            string endMsg = (hero.GetStatus() == Status.WIN) ? "Congratulations, Cave Hero!" : "Too bad, you lose!";

            Thread.Sleep(1000);
            Game.IO.WriteEnd(endMsg);
            Thread.Sleep(1000);
        } 
    }
}