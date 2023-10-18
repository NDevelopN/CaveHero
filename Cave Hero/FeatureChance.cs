namespace Cave
{
    public struct FeatureChance
    {
        int _baseChance;
        int _saturationFactor;
        int _roomCount, _featureCount;
        int _influenceFactor;
        List<Type> _posInf;
        List<Type> _negInf;

        public FeatureChance(int baseChance, int satFact)
        {
            _baseChance = baseChance;
            _saturationFactor = satFact;
            _roomCount = 0;
            _featureCount = 0;
            _influenceFactor = 0;
            _posInf = new();
            _negInf = new();
        }

        public FeatureChance(int baseChance, int satFact, int infFact, List<Type> posInf, List<Type> negInf)
        {
            _baseChance = baseChance;
            _saturationFactor = satFact;
            _roomCount = 0;
            _featureCount = 0;
            _influenceFactor = infFact;
            _posInf = posInf;
            _negInf = negInf;
        }


        public void IncreaseRoomCount(){
            _roomCount++;
        }
        
        public void IncreaseFeatureCount() {
            _featureCount++;
        }

        private int CalcSat() {
            if (_roomCount == 0 || _featureCount == 0) {
                return 0;
            }
            
            float saturation = _featureCount / _roomCount;
            return (int) (_saturationFactor * saturation);
        }


        public int GetChance(List<IFeature> features)
        {
            int chance = _baseChance - CalcSat(); 
            foreach (IFeature feature in features)
            {
                Type type = feature.GetType();
                if (_posInf.Contains(type))
                {
                    chance += _influenceFactor;
                }
                else if (_negInf.Contains(type))
                {
                    chance -= _influenceFactor;
                }
            }
            return chance;
        }
    }
}