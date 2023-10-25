using Server;

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
            IOBuffer.WriteMsg(Mention());
            Hero hero = (Hero)party[0];
            if (hero.CheckSuccess("Sibling"))
            {
                IOBuffer.WriteMsg("You have succeeded and saved your sibling!");
                hero.SetStatus(Status.WIN);
            }
            else
            {
                IOBuffer.WriteMsg("There's no going back now, your Sibling is dead for sure...");
                hero.SetStatus(Status.DEFEATED);
            }
        }
    }
}