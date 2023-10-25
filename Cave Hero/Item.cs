using Server;

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

        public virtual bool Use()
        {
            Uses--;
            if (Uses <= 0)
            {
                return true;
            }
            return false;
        }

        public virtual bool Use(Creature? target)
        {
            if (target == null)
            {
                return Use();
            }

            Game.IO.WriteMsg("You can't target a creature with " + Name);
            return false;
        }

        public virtual bool AddUses(int uses)
        {
            Uses += uses;
            return true;
        }
    }
}