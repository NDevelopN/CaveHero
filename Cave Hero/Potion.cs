namespace Cave
{
    public class Potion : Item
    {
        private Die _die;
        private int _maxUses = 3;

        public Potion(int size, int count, int uses) : base("Potion", uses)
        {
            _die = new Die(size, count);
        }

        public override void Use(Creature target)
        {
            Use();
            target.Heal(_die.Roll());
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
