namespace ACC.Common.Random
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class RandomGenerator
    {
        private readonly Random _random;

        public RandomGenerator()
        {
            _random = new Random();
        }

        public RandomGenerator(int seed)
        {
            _random = new Random(seed);
        }

        public T RandEnum<T>() where T : Enum
        {
            var values = (T[])Enum.GetValues(typeof(T));

            return RandItem(values);
        }

        public int RandInt(int min = int.MinValue, int max = int.MaxValue)
        {
            return _random.Next(min, max);
        }

        public int RandNonNegative(int max = int.MaxValue)
        {
            return _random.Next(max);
        }

        public T RandItem<T>(IEnumerable<T> sequence)
        {
            var items = sequence.ToArray();

            return items[RandNonNegative(items.Length)];
        }
    }
}