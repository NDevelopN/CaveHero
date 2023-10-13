namespace Cave
{
    public class Hero : Creature
    {
        public Hero() : base()
        {
            Name = "Hero";
            Hp = 3;
            Atk = 1;
            Spd = new Die(4);
            Items = new Dictionary<string, Item>() { { "Potion", new Potion(6, 1) } };
        }

        public void UseItem(string name)
        {
            if (!Items.ContainsKey(name))
            {
                Console.WriteLine("No " + name + " in Inventory.");
                return;
            }

            Items[name].Use(this);

            int uses = Items[name].GetUses();
            Console.Write("There are " + uses + " uses remaining. ");
            if (uses == 0)
            {
                Console.WriteLine("It is all gone!");
                Items.Remove(name);
            }
            else
            {
                Console.WriteLine();
            }
        }

        public override void Damage(int val)
        {
            base.Damage(val);
            if (Status != Status.DEAD)
            {

                if (Hp <= 2)
                {
                    Console.WriteLine("Health is low, using Potion.");
                    UseItem("Potion");
                }
            }
        }
    }
}