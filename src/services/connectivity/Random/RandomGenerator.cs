namespace ACC.Services.VehicleConnectivity.Random
{
    public class RandomGenerator
    {
        private readonly System.Random _random;

        public RandomGenerator()
        {
            _random = new System.Random();
        }

        public RandomGenerator(int seed)
        {
            _random = new System.Random(seed);
        }

        public int RandInt(int min = int.MinValue, int max = int.MaxValue)
        {
            return _random.Next(min, max);
        }

        public bool RandBool()
        {
            return RandInt() % 2 == 0;
        }
    }
}