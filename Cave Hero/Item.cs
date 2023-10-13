namespace Cave
{
    public abstract class Item
    {
        protected string Name;

        protected int Uses;

        protected Item(string name, int uses)
        {
            Name = name;
            Uses = uses;
        }

        public string GetName()
        {
            return Name;
        }

        public int GetUses()
        {
            return Uses;
        }

        protected virtual void Use()
        {
            Uses--;
        }

        public virtual void Use(Creature target)
        {

            Use();
        }

        public virtual bool AddUses(int uses)
        {
            Uses += uses;
            return true;
        }
    }
}