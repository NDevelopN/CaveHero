namespace Cave
{
    public class HeroBuilder : CreatureBuilder
    {

        public int MaxCompanions { get; set; }

        public HeroBuilder() : base()
        {
            Name = "Hero";
            MaxCompanions = 0;
        }

        public override Hero Build()
        {
            return new Hero(Name, HP, Atk, Spd, MaxCompanions);
        }
    }
}