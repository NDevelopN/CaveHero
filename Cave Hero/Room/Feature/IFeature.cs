using CaveHero.CHCreature;

namespace CaveHero.CHRoom.Feature {
    public interface IFeature 
    {
        void Trigger(List<Creature> party);
        string Mention();
    }
}