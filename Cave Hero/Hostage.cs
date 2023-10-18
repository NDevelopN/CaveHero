
namespace Cave {
    public class Hostage : IFeature
    {
        private bool _rescued;
        private Creature _hostage;

        public Hostage(Creature hostage) {
            _hostage = hostage;
            _rescued = false;
        }

        public string Mention()
        {
            return "There seems a be a hostage kept in this room.";
        }

        public void Trigger(List<Creature> party)
        {
            if (_rescued) {
                Console.Write("This is where you found: " + _hostage.GetName());
                return;
            }

            //TODO check for party space, etc
            Hero hero = (Hero)party[0];
            hero.Join(_hostage);
            Console.WriteLine(_hostage.GetName() + " joins the party!");
            _rescued = true;
        }
    }
}