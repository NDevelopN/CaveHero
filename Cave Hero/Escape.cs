namespace Cave
{
    public class Escape : Encounter
    {
        public override void Trigger(Hero hero)
        {
            if (hero.CheckSuccess("Sibling"))
            {
                Console.WriteLine("You have succeeded and saved your sibling!");
                hero.SetStatus(Status.WIN);
            }
            else
            {
                Console.WriteLine("There's no going back now, your Sibling is dead for sure...");
                hero.SetStatus(Status.DEAD);
            }
        }
    }
}