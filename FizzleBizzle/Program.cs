using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FizzleBizzle
{
    interface IFizzleBizzle
    {
        int Fizz { set; }
        int Buzz { set; }

        /// <summary>
        /// FizzBuzz generates an array of strings representing the consecutive sequence of
        /// integers from start to end. When the integer is a multiple of Fizz, the string
        /// "Fizz" is added instead. Likewise, for multiples of Buzz, "Buzz" is added. For
        /// multiples of both Fizz and Buzz, "FizzBuzz" is added.
        /// (e.g. With Fizz = 3, Buzz = 5, start = 1, and end = 15; the array looks like:
        /// ["1", "2", "Fizz", "4", "Buzz", "Fizz", "7", "8", ... , "14", "FizzBuzz"])
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        string[] FizzBuzz(int start, int end);

        /// <summary>
        /// FizzBuzzBazz returns the same result as FizzBuzz, except that instances of "FizzBuzz"
        /// are "FizzBuzzBazz" where the Predicate bazz is true.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="bazz"></param>
        /// <returns></returns>
        string[] FizzBuzzBazz(int start, int end, Predicate<int> bazz);
    }

    public class FizzBizz : IFizzleBizzle
    {
        private int _Fizz;
        private int _Buzz;

        public int Fizz { set { _Fizz = value; } }
        public int Buzz { set { _Buzz = value; } }

        public bool FizzDiv(int input)
        {
            return input % _Fizz == 0;
        }

        public bool BuzzDiv(int input)
        {
            return input % _Buzz == 0;
        }

        public bool FizzBuzzDiv(int input)
        {
            return input % _Fizz == 0 && input % _Buzz == 0;
        }

        public int CheckStartValue(int start, int end)
        {
            if (start >= end)
            {
                throw new System.ArgumentOutOfRangeException("START value has to be < than END value");
            }
            if (start == 0)
            {
                ++start;
            }
            return start;
        }

        public string[] FizzBuzz(int start, int end)
        {
            start = CheckStartValue(start, end);

            List<string> listFizzBuzz = new List<string>();

            for (int i = start; i <= end; i++)
            {
                if (FizzBuzzDiv(i))
                {
                    listFizzBuzz.Add("FizzBuzz");
                }
                else if (FizzDiv(i))
                {
                    listFizzBuzz.Add("Fizz");
                }
                else if (FizzDiv(i))
                {
                    listFizzBuzz.Add("Buzz");
                }
                else
                {
                    listFizzBuzz.Add(i.ToString());
                }
            }
            return listFizzBuzz.ToArray();
        }

        public string[] FizzBuzzBazz(int start, int end, Predicate<int> bazz)
        {
            start = CheckStartValue(start, end);

            Dictionary<int, string> dictFizzBuzz = new Dictionary<int, string>();

            for (int i = start; i <= end; i++)
            {
                if (FizzBuzzDiv(i))
                {
                    dictFizzBuzz.Add(i, "FizzBuzz");
                }
                else if (FizzDiv(i))
                {
                    dictFizzBuzz.Add(i, "Fizz");
                }
                else if (BuzzDiv(i))
                {
                    dictFizzBuzz.Add(i, "Buzz");
                }
                else
                {
                    dictFizzBuzz.Add(i, i.ToString());
                }
            }
            List<int> outputList = new List<int>();
            var smth = dictFizzBuzz.Keys.ToList();
            outputList = smth.FindAll(bazz);

            foreach (var item in outputList)
            {
                dictFizzBuzz[item] = "FizzBuzzBazz";
            }
            string[] fizzBuzzBazz = dictFizzBuzz.Select(d => d.Value.ToString()).ToArray();

            return fizzBuzzBazz;
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            FizzBizz _fizzBizz = new FizzBizz();

            Console.WriteLine("Please pass START value for a sequence: ");
            int startInput = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Please pass END value for a sequence: ");
            int endInput = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Please pass value for 'Fizz': ");
            _fizzBizz.Fizz = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Please pass value for 'Buzz': ");
            _fizzBizz.Buzz = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("\nFizzBuzz Function");

            string[] fizzBuzz = _fizzBizz.FizzBuzz(startInput, endInput);
            foreach (var item in fizzBuzz)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("\nPress any key to see FizzBuzzBazz Function");
            Console.Read();

            Predicate<int> bazz = new Predicate<int>(_fizzBizz.FizzBuzzDiv);
            string[] fizzBuzzBazz = _fizzBizz.FizzBuzzBazz(startInput, endInput, bazz);
            foreach (var item in fizzBuzzBazz)
            {
                Console.WriteLine(item);
            }
            Console.ReadKey();
        }
    }
}
