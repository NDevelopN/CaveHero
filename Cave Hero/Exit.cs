namespace Cave
{

    //TODO remove
    public class Exit : Room
    {
        public Encounter Enter()
        {
            return new Escape();
        }
    }
}