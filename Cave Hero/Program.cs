namespace Cave
{
    public class Program
    {
        static void Main(string[] args)
        {
            Hero hero = new();

            Console.WriteLine("Welcome to the Cave!");
            Console.WriteLine("Nasty monsters took your sibling into this Cave.");
            Console.WriteLine("You must hurry to save them, before it's too late!");

            Entrance ent = new();
            Exit exit = new();
            ent.AddPath("Escape", exit);

            ent.GenPaths(5);

            Room? curRoom = ent;
            while (hero.GetStatus() != Status.DEAD && hero.GetStatus() != Status.WIN)
            {
                curRoom = curRoom.ChoosePath();
                if (curRoom == null)
                {
                    return;
                }

                Encounter enc = curRoom.Enter();
                enc.Trigger(hero);
            }

            string endMsg = (hero.GetStatus() == Status.WIN) ? "Congratulations, Cave Hero!" : "Too bad, you lose!";

            Thread.Sleep(1000);
            Console.WriteLine(endMsg);
            Thread.Sleep(1000);
        }
    }
}
