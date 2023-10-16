namespace Cave
{
    public class Escape : IFeature 
    {
        public string Mention()
        {
            return "The air is clean out here, you've escaped from the cave. \nWhat did you bring with you?";
        }

//TODO variable goals
        public void Trigger(List<Creature> party)
        {
            //TODO remove
            Console.WriteLine(Mention());
            Hero hero = (Hero)party[0];
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