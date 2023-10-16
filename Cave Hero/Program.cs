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

            Cave cave = new(6, 6, 12);
            Entrance? ent = cave.Generate();
            if (ent == null) {
                Console.WriteLine("Entrance returned was null");
                Environment.Exit(1);

            }

            Room? curRoom = ent;
            while (hero.GetStatus() != Status.DEAD && hero.GetStatus() != Status.WIN)
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
            Console.WriteLine(endMsg);
            Thread.Sleep(1000);
        }
    }
}
