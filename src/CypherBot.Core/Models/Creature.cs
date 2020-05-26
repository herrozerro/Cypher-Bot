using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CypherBot.Core.Models
{
    public class Creature
    {
        public int CreatureId { get; set; }
        
        public string Name { get; set; }
        public int Level { get; set; }
        public string Motive { get; set; }
        public string Environment { get; set; }
        public int Health { get; set; }
        public int DamageInflicted { get; set; }
        public int Armor { get; set; }
        public string Movement { get; set; }
        public string Modifications { get; set; }
        public string Combat { get; set; }
        public string Interaction { get; set; }
        public string Use { get; set; }
        public string GMIntrusions { get; set; }
    }
}
