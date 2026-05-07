using System;
using System.Linq;

namespace StatisticsAssignment
{
    class Program
    {
        static void Main(string[] args)
        {
            double[] data = {
                115, 182, 191, 31, 196, 1099, 5, 172, 10, 179,
                83, 21, 20, 21, 186, 177, 195, 193, 188, 199,
                62, 109, 105, 183, 110
            };

            Array.Sort(data);

            int n = data.Length;

            double sum = data.Sum();
            double mean = sum / n;

            double median = Median(data);

            double mode = data.GroupBy(x => x)
                              .OrderByDescending(g => g.Count())
                              .First().Key;

            double variance = data.Sum(x => Math.Pow(x - mean, 2)) / n;
            double stdDev = Math.Sqrt(variance);

            double p20 = Percentile(data, 20);
            double p50 = Percentile(data, 50);

            double q1 = Percentile(data, 25);
            double q2 = Percentile(data, 50);
            double q3 = Percentile(data, 75);

            double range = data[n - 1] - data[0];
            double iqr = q3 - q1;

            Console.WriteLine("Mean = " + mean);
            Console.WriteLine("Mode = " + mode);
            Console.WriteLine("Median = " + median);
            Console.WriteLine("Variance = " + variance);
            Console.WriteLine("P20 = " + p20);
            Console.WriteLine("P50 = " + p50);
            Console.WriteLine("Q1 = " + q1);
            Console.WriteLine("Q2 = " + q2);
            Console.WriteLine("Q3 = " + q3);
            Console.WriteLine("Range = " + range);
            Console.WriteLine("Interquartile Range = " + iqr);
            Console.WriteLine("Standard Deviation = " + stdDev);
            Console.WriteLine("Summation = " + sum);

            Console.WriteLine("\nOutliers:");

            double lower = q1 - (1.5 * iqr);
            double upper = q3 + (1.5 * iqr);

            foreach (double x in data)
            {
                if (x < lower || x > upper)
                    Console.WriteLine(x + " Outlier");
                else
                    Console.WriteLine(x + " Normal");
            }

            Console.ReadKey();
        }

        static double Median(double[] arr)
        {
            int n = arr.Length;

            if (n % 2 == 0)
                return (arr[n / 2] + arr[n / 2 - 1]) / 2.0;
            else
                return arr[n / 2];
        }

        static double Percentile(double[] arr, double p)
        {
            double pos = (p / 100.0) * (arr.Length - 1);

            int lower = (int)Math.Floor(pos);
            int upper = (int)Math.Ceiling(pos);

            if (lower == upper)
                return arr[lower];

            return arr[lower] + (arr[upper] - arr[lower]) * (pos - lower);
        }
    }
}

