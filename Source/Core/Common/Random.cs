using System;

namespace Core
{
    /// <summary>
    /// A custom random number generator class with enhanced functionality
    /// </summary>
    public class RandomUtility
    {
        private readonly System.Random _random;
        private readonly object _lock = new object();

        /// <summary>
        /// Initializes a new instance of RandomUtility with a random seed
        /// </summary>
        public RandomUtility()
        {
            _random = new System.Random();
        }

        /// <summary>
        /// Initializes a new instance of RandomUtility with a specific seed
        /// </summary>
        /// <param name="seed">The seed value for random generation</param>
        public RandomUtility(int seed)
        {
            _random = new System.Random(seed);
        }

        /// <summary>
        /// Returns a random double between minValue and maxValue (inclusive)
        /// </summary>
        /// <param name="minValue">Minimum value</param>
        /// <param name="maxValue">Maximum value</param>
        /// <returns>A random double in the specified range</returns>
        /// <exception cref="ArgumentException">Thrown when minValue is greater than maxValue</exception>
        public double NextDouble(double minValue, double maxValue)
        {
            if (double.IsNaN(minValue) || double.IsNaN(maxValue))
            {
                throw new ArgumentException("Values cannot be NaN");
            }

            if (minValue > maxValue)
            {
                throw new ArgumentException("minValue must be less than or equal to maxValue");
            }

            lock (_lock)
            {
                return minValue + (_random.NextDouble() * (maxValue - minValue));
            }
        }

        /// <summary>
        /// Returns a random integer between minValue and maxValue (inclusive)
        /// </summary>
        /// <param name="minValue">Minimum value</param>
        /// <param name="maxValue">Maximum value</param>
        /// <returns>A random integer in the specified range</returns>
        public int NextInt(int minValue, int maxValue)
        {
            if (minValue > maxValue)
            {
                throw new ArgumentException("minValue must be less than or equal to maxValue");
            }

            lock (_lock)
            {
                return _random.Next(minValue, maxValue + 1);
            }
        }

        /// <summary>
        /// Returns a random boolean value
        /// </summary>
        /// <returns>True or False with equal probability</returns>
        public bool NextBool()
        {
            lock (_lock)
            {
                return _random.Next(0, 2) == 1;
            }
        }

        /// <summary>
        /// Fills the specified byte array with random bytes
        /// </summary>
        /// <param name="buffer">The array to fill with random bytes</param>
        /// <exception cref="ArgumentNullException">Thrown when buffer is null</exception>
        public void NextBytes(byte[] buffer)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }

            lock (_lock)
            {
                _random.NextBytes(buffer);
            }
        }

        /// <summary>
        /// Returns a random element from the specified array
        /// </summary>
        /// <typeparam name="T">Type of elements in the array</typeparam>
        /// <param name="array">The array to select from</param>
        /// <returns>A random element from the array</returns>
        /// <exception cref="ArgumentNullException">Thrown when array is null</exception>
        /// <exception cref="ArgumentException">Thrown when array is empty</exception>
        public T GetRandomElement<T>(T[] array)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }
            if (array.Length == 0)
            {
                throw new ArgumentException("Array cannot be empty");
            }

            lock (_lock)
            {
                return array[_random.Next(0, array.Length)];
            }
        }
    }
}
