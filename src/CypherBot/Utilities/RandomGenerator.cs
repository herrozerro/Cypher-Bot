using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CypherBot.Utilities
{
    public static class RandomGenerator
    {
        private static Random rnd = null;

        public static Random GetRandom()
        {
            if (rnd == null)
            {
                rnd = new Random();
            }

            return rnd;
        }

        public static string GetRandomDesination(int length)
        {
            var rnd = GetRandom();

            var des = rnd.Next(10000).ToString("0000");

            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            return des + "-" + new string(Enumerable.Repeat(chars, length)
             .Select(s => s[rnd.Next(s.Length)]).ToArray());

        }
    }
}
