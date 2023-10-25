using Server;

namespace Cave
{
    public class Die
    {
        private int _sides;
        private int _count; 

        public Die(int sides, int count) {
            _sides = sides;
            _count = count;
        }


        public int GetSides() {
            return _sides;
        }

        public int GetCount() {
            return _count;
        }

        public int Roll() {
            return Roll(false);
        }

        public int OpenRoll() {
            return Roll(true);
        }

        public int GetMax() {
            return _sides * _count;
        }

        public int GetMin() {
            return _count;
        }

        public int GetAvg() {
            return (_count + GetMax()) / 2;
        }


        private int Roll(bool print)
        {
            Random rnd = new();

            int total = 0;
            for (int i = 0; i < _count; i++)
            {
                int result = rnd.Next(1, _sides + 1);
                if (print)
                {
                    Game.IO.WriteMsg("Roll" + (i+1) + ": [" + result + "/" + _sides + "]");
                }
                total += result;
            }

            if (print)
            {
                Game.IO.WriteMsg("Total = " + total);
            }

            return total;
        }
    }
}