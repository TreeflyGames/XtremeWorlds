using System;
using System.Collections.Generic;

namespace Core
{
    /// <summary>
    /// A custom random number generator class with enhanced functionality, including support for various distributions and utility methods.
    /// </summary>
    public class RandomUtility
    {
        private readonly System.Random _random;
        private readonly object _lock = new object();
        private double _cachedGaussian;
        private bool _hasCachedGaussian = false;

        /// <summary>
        /// Initializes a new instance of RandomUtility with a random seed.
        /// </summary>
        public RandomUtility()
        {
            _random = new System.Random();
        }

        /// <summary>
        /// Initializes a new instance of RandomUtility with a specific seed.
        /// </summary>
        /// <param name="seed">The seed value for random generation.</param>
        public RandomUtility(int seed)
        {
            _random = new System.Random(seed);
        }

        /// <summary>
        /// Returns a random double between minValue and maxValue (inclusive).
        /// </summary>
        /// <param name="minValue">Minimum value.</param>
        /// <param name="maxValue">Maximum value.</param>
        /// <returns>A random double in the specified range.</returns>
        /// <exception cref="ArgumentException">Thrown when minValue is greater than maxValue or values are NaN.</exception>
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
        /// Returns a random integer between minValue and maxValue (inclusive).
        /// </summary>
        /// <param name="minValue">Minimum value.</param>
        /// <param name="maxValue">Maximum value.</param>
        /// <returns>A random integer in the specified range.</returns>
        /// <exception cref="ArgumentException">Thrown when minValue is greater than maxValue.</exception>
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
        /// Returns a random boolean value.
        /// </summary>
        /// <returns>True or False with equal probability.</returns>
        public bool NextBool()
        {
            lock (_lock)
            {
                return _random.Next(0, 2) == 1;
            }
        }

        /// <summary>
        /// Fills the specified byte array with random bytes.
        /// </summary>
        /// <param name="buffer">The array to fill with random bytes.</param>
        /// <exception cref="ArgumentNullException">Thrown when buffer is null.</exception>
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
        /// Returns a random element from the specified array.
        /// </summary>
        /// <typeparam name="T">Type of elements in the array.</typeparam>
        /// <param name="array">The array to select from.</param>
        /// <returns>A random element from the array.</returns>
        /// <exception cref="ArgumentNullException">Thrown when array is null.</exception>
        /// <exception cref="ArgumentException">Thrown when array is empty.</exception>
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

        /// <summary>
        /// Returns a random double from a normal (Gaussian) distribution with the specified mean and standard deviation.
        /// </summary>
        /// <param name="mean">The mean of the distribution.</param>
        /// <param name="stdDev">The standard deviation of the distribution.</param>
        /// <returns>A random double from the normal distribution.</returns>
        /// <exception cref="ArgumentException">Thrown when stdDev is negative.</exception>
        public double NextGaussian(double mean, double stdDev)
        {
            if (stdDev < 0)
            {
                throw new ArgumentException("Standard deviation must be non-negative");
            }

            lock (_lock)
            {
                if (_hasCachedGaussian)
                {
                    _hasCachedGaussian = false;
                    return _cachedGaussian * stdDev + mean;
                }
                else
                {
                    double u1, u2, s;
                    do
                    {
                        u1 = _random.NextDouble();
                        u2 = _random.NextDouble();
                        s = u1 * u1 + u2 * u2;
                    } while (s >= 1 || s == 0);
                    double z0 = Math.Sqrt(-2 * Math.Log(s) / s) * u1;
                    double z1 = Math.Sqrt(-2 * Math.Log(s) / s) * u2;
                    _cachedGaussian = z1;
                    _hasCachedGaussian = true;
                    return z0 * stdDev + mean;
                }
            }
        }

        /// <summary>
        /// Returns a random double from an exponential distribution with the specified rate parameter (lambda).
        /// </summary>
        /// <param name="lambda">The rate parameter of the distribution.</param>
        /// <returns>A random double from the exponential distribution.</returns>
        /// <exception cref="ArgumentException">Thrown when lambda is not positive.</exception>
        public double NextExponential(double lambda)
        {
            if (lambda <= 0)
            {
                throw new ArgumentException("Lambda must be positive");
            }

            lock (_lock)
            {
                double u = _random.NextDouble();
                return -Math.Log(1 - u) / lambda;
            }
        }

        /// <summary>
        /// Returns a random integer from a Poisson distribution with the specified mean (lambda).
        /// </summary>
        /// <param name="lambda">The mean of the distribution.</param>
        /// <returns>A random integer from the Poisson distribution.</returns>
        /// <exception cref="ArgumentException">Thrown when lambda is negative.</exception>
        public int NextPoisson(double lambda)
        {
            if (lambda < 0)
            {
                throw new ArgumentException("Lambda must be non-negative");
            }

            lock (_lock)
            {
                double L = Math.Exp(-lambda);
                int k = 0;
                double p = 1;
                do
                {
                    k++;
                    double u = _random.NextDouble();
                    p *= u;
                } while (p > L);
                return k - 1;
            }
        }

        /// <summary>
        /// Returns a random point inside a circle with the specified radius.
        /// </summary>
        /// <param name="radius">The radius of the circle.</param>
        /// <returns>A tuple representing the (x, y) coordinates of the point.</returns>
        /// <exception cref="ArgumentException">Thrown when radius is negative.</exception>
        public (double, double) NextPointInCircle(double radius)
        {
            if (radius < 0)
            {
                throw new ArgumentException("Radius must be non-negative");
            }

            lock (_lock)
            {
                double r = Math.Sqrt(_random.NextDouble()) * radius;
                double theta = _random.NextDouble() * 2 * Math.PI;
                return (r * Math.Cos(theta), r * Math.Sin(theta));
            }
        }

        /// <summary>
        /// Returns a random point on the circumference of a circle with the specified radius.
        /// </summary>
        /// <param name="radius">The radius of the circle.</param>
        /// <returns>A tuple representing the (x, y) coordinates of the point.</returns>
        /// <exception cref="ArgumentException">Thrown when radius is negative.</exception>
        public (double, double) NextPointOnCircle(double radius)
        {
            if (radius < 0)
            {
                throw new ArgumentException("Radius must be non-negative");
            }

            lock (_lock)
            {
                double theta = _random.NextDouble() * 2 * Math.PI;
                return (radius * Math.Cos(theta), radius * Math.Sin(theta));
            }
        }

        /// <summary>
        /// Returns a random point inside a sphere with the specified radius.
        /// </summary>
        /// <param name="radius">The radius of the sphere.</param>
        /// <returns>A tuple representing the (x, y, z) coordinates of the point.</returns>
        /// <exception cref="ArgumentException">Thrown when radius is negative.</exception>
        public (double, double, double) NextPointInSphere(double radius)
        {
            if (radius < 0)
            {
                throw new ArgumentException("Radius must be non-negative");
            }

            lock (_lock)
            {
                double u = _random.NextDouble();
                double r = Math.Cbrt(u) * radius;
                double theta = _random.NextDouble() * 2 * Math.PI;
                double phi = Math.Acos(2 * _random.NextDouble() - 1);
                double x = r * Math.Sin(phi) * Math.Cos(theta);
                double y = r * Math.Sin(phi) * Math.Sin(theta);
                double z = r * Math.Cos(phi);
                return (x, y, z);
            }
        }

        /// <summary>
        /// Returns a random point on the surface of a sphere with the specified radius.
        /// </summary>
        /// <param name="radius">The radius of the sphere.</param>
        /// <returns>A tuple representing the (x, y, z) coordinates of the point.</returns>
        /// <exception cref="ArgumentException">Thrown when radius is negative.</exception>
        public (double, double, double) NextPointOnSphere(double radius)
        {
            if (radius < 0)
            {
                throw new ArgumentException("Radius must be non-negative");
            }

            lock (_lock)
            {
                double theta = _random.NextDouble() * 2 * Math.PI;
                double phi = Math.Acos(2 * _random.NextDouble() - 1);
                double x = radius * Math.Sin(phi) * Math.Cos(theta);
                double y = radius * Math.Sin(phi) * Math.Sin(theta);
                double z = radius * Math.Cos(phi);
                return (x, y, z);
            }
        }

        /// <summary>
        /// Shuffles the elements of the specified array in place.
        /// </summary>
        /// <typeparam name="T">The type of elements in the array.</typeparam>
        /// <param name="array">The array to shuffle.</param>
        /// <exception cref="ArgumentNullException">Thrown when array is null.</exception>
        public void Shuffle<T>(T[] array)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            lock (_lock)
            {
                for (int i = array.Length - 1; i > 0; i--)
                {
                    int j = _random.Next(0, i + 1);
                    T temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                }
            }
        }

        /// <summary>
        /// Shuffles the elements of the specified list in place.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="list">The list to shuffle.</param>
        /// <exception cref="ArgumentNullException">Thrown when list is null.</exception>
        public void Shuffle<T>(List<T> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            lock (_lock)
            {
                for (int i = list.Count - 1; i > 0; i--)
                {
                    int j = _random.Next(0, i + 1);
                    T temp = list[i];
                    list[i] = list[j];
                    list[j] = temp;
                }
            }
        }

        /// <summary>
        /// Returns a random index based on the specified weights.
        /// </summary>
        /// <param name="weights">An array of non-negative weights.</param>
        /// <returns>The index of the selected weight.</returns>
        /// <exception cref="ArgumentNullException">Thrown when weights is null.</exception>
        /// <exception cref="ArgumentException">Thrown when weights array is empty, contains negative values, or total weight is zero.</exception>
        public int NextWeightedIndex(double[] weights)
        {
            if (weights == null)
            {
                throw new ArgumentNullException(nameof(weights));
            }
            if (weights.Length == 0)
            {
                throw new ArgumentException("Weights array cannot be empty");
            }

            double total = 0;
            foreach (double w in weights)
            {
                if (w < 0)
                {
                    throw new ArgumentException("Weights must be non-negative");
                }
                total += w;
            }
            if (total == 0)
            {
                throw new ArgumentException("Total weight cannot be zero");
            }

            lock (_lock)
            {
                double r = _random.NextDouble() * total;
                double sum = 0;
                for (int i = 0; i < weights.Length; i++)
                {
                    sum += weights[i];
                    if (sum >= r)
                    {
                        return i;
                    }
                }
                return weights.Length - 1; // Fallback
            }
        }

        /// <summary>
        /// Returns a random subset of k distinct integers from 0 to n-1.
        /// </summary>
        /// <param name="n">The upper bound (exclusive) of the range.</param>
        /// <param name="k">The size of the subset.</param>
        /// <returns>An array of k distinct integers from 0 to n-1.</returns>
        /// <exception cref="ArgumentException">Thrown when n or k is negative, or k is greater than n.</exception>
        public int[] NextSubset(int n, int k)
        {
            if (n < 0 || k < 0 || k > n)
            {
                throw new ArgumentException("Invalid parameters for subset generation");
            }

            int[] array = new int[n];
            for (int i = 0; i < n; i++)
            {
                array[i] = i;
            }
            this.Shuffle(array);
            return array.Take(k).ToArray();
        }

        /// <summary>
        /// Generates a random string of the specified length using the given characters.
        /// </summary>
        /// <param name="length">The length of the string to generate.</param>
        /// <param name="characters">The characters to use in the string. Defaults to alphanumeric characters.</param>
        /// <returns>A random string of the specified length.</returns>
        /// <exception cref="ArgumentException">Thrown when length is negative or characters string is empty.</exception>
        public string NextString(int length, string characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789")
        {
            if (length < 0)
            {
                throw new ArgumentException("Length must be non-negative");
            }
            if (string.IsNullOrEmpty(characters))
            {
                throw new ArgumentException("Characters string cannot be empty");
            }

            char[] result = new char[length];
            lock (_lock)
            {
                for (int i = 0; i < length; i++)
                {
                    result[i] = characters[_random.Next(0, characters.Length)];
                }
            }
            return new string(result);
        }
    }
}
