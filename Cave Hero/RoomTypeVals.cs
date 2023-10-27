namespace Cave
{
    public struct RoomTypeVals
    {
        public double Percent;
        public int FCount;
        public int Max;

        public RoomTypeVals(double percent, int count, int max)
        {
            Percent = percent;
            FCount = count;
            Max = max;
        }

        public override string ToString()
        {
            return "Percent: " + Percent + ", FCount: " + FCount + ", Max: " + Max;
        }
    }
}
