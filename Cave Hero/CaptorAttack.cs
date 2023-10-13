namespace Cave
{
    public class CaptorAttack : MonsterAttack
    {
        private Creature? _captive;

        public CaptorAttack(Creature captive) : base()
        {
            _captive = captive;
        }

        protected override void Combat(Hero hero)
        {
            if (_captive != null)
            {
                if (Solved)
                {

                }
                Console.WriteLine("Behind " + Enemy.GetName() + " is a captive. Defeat the enemy to free them!");
            }

            base.Combat(hero);
        }

        protected override void Reward(Hero hero)
        {
            base.Reward(hero);
            if (_captive != null)
            {
                if (hero.Join(_captive))
                {
                    Console.WriteLine(_captive.GetName() + " is now free, and joins " + hero.GetName() + ".");
                    _captive = null;
                }
                else
                {
                    Console.WriteLine(hero.GetName() + " has freed " + _captive.GetName() + " but cannot lead any more people.");
                }
            }
        }
    }
}