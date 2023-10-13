namespace Cave
{
    public class Die
    {
        protected int _sides;

        public Die(int sides)
        {
            _sides = sides;
        }

        public void SetSides(int sides)
        {
            _sides = sides;
        }

        public int GetSides() {
            return _sides;
        }

        public int Roll(int count, bool print)
        {
            Random rnd = new();

            int total = 0;
            for (int i = 0; i < count; i++)
            {
                int result = rnd.Next(1, _sides + 1);
                if (print)
                {
                    Console.Write("[" + result + "/" + _sides + "]");
                }
                total += result;
            }

            if (print)
            {
                Console.WriteLine(" = " + total);
            }

            return total;
        }
    }
}