using System;
using System.Collections.Generic;
using System.Text;

namespace CypherBot.Models
{
    public class Descriptor
    {
        public int DescriptorId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<DescriptorAbility> DescriptorAbilities { get; set; }
    }
}
