namespace Cave
{
    public class Entrance : Room 
    {
        public Entrance() { }

        public override Encounter Enter()
        {
            return new Encounter();
        }
    }
}