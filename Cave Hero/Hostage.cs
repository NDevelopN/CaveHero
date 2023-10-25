using Server;

namespace Cave
{
    public class Hostage : IFeature
    {
        private CreatureDirector _cDir;
        private CreatureBuilder _cBuild;
        private bool _rescued;
        private Creature _hostage;

        public Hostage()
        {
            _cDir = new();
            _cBuild = new();

            _hostage = _cDir.CreateHostage(_cBuild, null);
            _rescued = false;
        }

        public string Mention()
        {
            return "There seems a be a hostage kept in this room.";
        }

        public void Trigger(List<Creature> party)
        {
            if (_rescued)
            {
                IOBuffer.WriteMsg("This is where you found: " + _hostage.GetName());
                return;
            }

            //TODO check for party space, etc
            Hero hero = (Hero)party[0];
            hero.Join(_hostage);
            IOBuffer.WriteMsg(_hostage.GetName() + " joins the party!");
            _rescued = true;
        }
    }
}