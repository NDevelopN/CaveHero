namespace Cave
{
    public class CreatureDirector
    {
        public Creature CreateHostage(CreatureBuilder builder, string? name)
        {
            builder.Name = name ?? "Sibling";
            builder.HP = 5;
            builder.Atk = new Die(0, 0);
            builder.Spd = new Die(3, 1);

            return builder.Build();
        }

        // Monsters
        public Creature CreateGoblin(CreatureBuilder builder)
        {
            builder.Name = "Goblin";
            builder.HP = new Die(2, 3).Roll();
            builder.Atk = new Die(2, 1);
            builder.Spd = new Die(3, 2);

            return builder.Build();
        }

        public Creature CreateWolf(CreatureBuilder builder)
        {
            builder.Name = "Wolf";
            builder.HP = new Die(3, 3).Roll();
            builder.Atk = new Die(6, 1);
            builder.Spd = new Die(4, 1);

            return builder.Build();
        }
    }
}