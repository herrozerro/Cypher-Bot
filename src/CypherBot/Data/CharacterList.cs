using System;
using System.Collections.Generic;
using System.Text;
using CypherBot.Models;

namespace CypherBot.Data
{
    public class CharacterList
    {
        public static List<Character> Characters { get; } = new List<Character>();

        private List<Character> _Characters = new List<Character>();
        private CharacterList()
        {
            // Initialize here
        }
    }
}
