namespace Cave
{
    //TODO remove
    public class Entrance : Room 
    {
        public Entrance() { }

        public Encounter Enter()
        {
            return new Encounter();
        }
    }
}