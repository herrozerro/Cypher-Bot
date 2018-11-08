using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace CypherBot.DataAccess.Utilities
{
    public class StringHelpers
    {

        public static string CleanFileName(string fileName)
        {
            return Path.GetInvalidFileNameChars().Aggregate(fileName, (current, c) => current.Replace(c.ToString(), string.Empty));
        }

        public static string CleanPathName(string pathName)
        {
            return Path.GetInvalidPathChars().Aggregate(pathName, (current, c) => current.Replace(c.ToString(), string.Empty));
        }
    }
}
