namespace Cave
{
    public class Escape : Encounter 
    {
        public override void Trigger(Hero hero)
        {
            Console.WriteLine("You have escaped from the cave, but what did " + hero.GetName() + " leave behind?");
            hero.Damage(hero.GetHP());
        }
    }
}