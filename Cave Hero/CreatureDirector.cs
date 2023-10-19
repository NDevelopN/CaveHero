namespace Cave
{
    public class CreatureDirector
    {
        private int _creatureNum;

        public CreatureDirector()
        {
            _creatureNum = 0;
        }

        public Creature CreateHostage(CreatureBuilder builder, string? name)
        {
            builder.Reset();
            builder.Name = name ?? "Sibling";
            builder.HP = 5;
            builder.Atk = new Die(2, 1);
            builder.Spd = new Die(3, 1);

            return builder.Build();
        }

        // Monsters
        public Creature CreateRandomMonster(CreatureBuilder builder)
        {
            Random rnd = new();
            switch (rnd.Next(0, 2))
            {
                case 0:
                    return CreateGoblin(builder);
                default:
                    return CreateWolf(builder);
            }
        }

        public Creature CreateGoblin(CreatureBuilder builder)
        {
            builder.Reset();
            builder.Name = "Goblin_" + _creatureNum;
            builder.HP = new Die(2, 3).Roll();
            builder.Atk = new Die(2, 1);
            builder.Spd = new Die(3, 2);

            _creatureNum++;

            return builder.Build();
        }

        public Creature CreateWolf(CreatureBuilder builder)
        {
            builder.Reset();
            builder.Name = "Wolf_" + _creatureNum;
            builder.HP = new Die(3, 3).Roll();
            builder.Atk = new Die(6, 1);
            builder.Spd = new Die(4, 1);

            _creatureNum++;

            return builder.Build();
        }
    }
}