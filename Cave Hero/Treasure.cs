using Server;

namespace Cave
{
    public class Treasure : IFeature
    {
        private bool _found;

        private List<Item> _haul;

        public Treasure(List<Item> haul)
        {
            _haul = haul;
            _found = false;
        }

        public void AddItem(Item item)
        {
            _haul.Add(item);
        }

        public void Trigger(List<Creature> party)
        {
            if (_found)
            {
                Recall();
            }
            else
            {
                Reward((Hero)party[0]);
            }
        }

        public string Mention()
        {
            return "There is a treasure chest here.";
        }

        private void Reward(Hero hero)
        {
            Game.IO.WriteMsg("You found a treasure chest! Contained within...");
            if (_haul.Count == 0)
            {
                Game.IO.WriteMsg("Nothing... bummer");
                return;
            }

            foreach (Item item in _haul)
            {
                Game.IO.WriteMsg("" + item.ToString());
                hero.AddItem(item);
            }

            _found = true;
        }

        private void Recall()
        {
            Game.IO.WriteMsg("There was treasure here before. \nYou found:");
            foreach (Item item in _haul)
            {
                Game.IO.WriteMsg(item.GetName() + " (" + item.GetUses() + ")");
            }
            Game.IO.WriteMsg("No, you don't get to grab it again.");
        }
    }
}