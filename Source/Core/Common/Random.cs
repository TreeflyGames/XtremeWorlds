using System;

namespace Core
{
    public class Random
    {
        public System.Random Flicker = new System.Random();

        public double NextDouble(double minValue, double maxValue)
        {
            if (minValue > maxValue)
            {
                throw new ArgumentException("minValue must be less than or equal to maxValue.");
            }

            // Generate a random double within the given range
            double randomValue = Flicker.NextDouble();
            return minValue + randomValue * (maxValue - minValue);
        }

    }
}