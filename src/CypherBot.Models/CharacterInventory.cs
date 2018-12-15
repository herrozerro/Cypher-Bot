using System;
using System.Collections.Generic;
using System.Text;

namespace CypherBot.Models
{
    public class CharacterInventory
    {
        public int InventoryId { get; set; }
        public string ItemName { get; set; }
        public int Qty { get; set; }
    }
}
