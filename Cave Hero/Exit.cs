namespace Cave
{
    public class Exit : Room
    {
        public override Encounter Enter()
        {
            return new Escape();
        }
    }
}