namespace Cave
{
    public class Program
    {
        static void Main(string[] args)
        {
            Hero hero = new();

            Console.WriteLine("Welcome to the Cave!");

            Entrance ent = new();
            Exit exit = new();
            ent.AddPath("Escape", exit);

            ent.GenPaths(5);

            Room? curRoom = ent;
            while (hero.GetStatus() != Status.DEAD)
            {
                curRoom = curRoom.ChoosePath();
                if (curRoom == null)
                {
                    return;
                }

                Encounter enc = curRoom.Enter();
                enc.Trigger(hero);
            }

            Thread.Sleep(1000);
            Console.WriteLine("Too bad, you lose!");
            Thread.Sleep(1000);
        }
    }
} 
