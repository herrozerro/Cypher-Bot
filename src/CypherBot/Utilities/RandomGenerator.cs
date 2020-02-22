using System;
using System.Collections.Generic;
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
    }
}
