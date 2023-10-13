namespace Cave
{
    public class Encounter 
    {
        protected bool Solved;
        public Encounter() { }

        public virtual void Trigger(Hero hero) {
            NoDanger(hero);
        }

        protected virtual void NoDanger(Hero hero) {
            Console.WriteLine("This room is safe, " + hero.GetName() + " has a chance to take a breath.");
        }
    }
}