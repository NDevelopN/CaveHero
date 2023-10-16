namespace Cave {
    public interface IFeature 
    {
        void Trigger(List<Creature> party);
        string Mention();
    }
}