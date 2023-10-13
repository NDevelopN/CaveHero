namespace Cave
{
    public class Potion : Item
    {
        private Die _hp;
        private int _maxUses = 3;

        public Potion(int hp, int uses) : base("Potion", uses)
        {
            _hp = new Die(hp);
        }

        public override void Use(Creature target)
        {
            Use();
            target.Heal(_hp.Roll(1, true));
        }

        public override bool AddUses(int uses)
        {
            int total = uses + Uses;
            if (total > _maxUses)
            {
                Uses = _maxUses;
                return false;
            }

            Uses = total;
            return true;
        }
    }
}
