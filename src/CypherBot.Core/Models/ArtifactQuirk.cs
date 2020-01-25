using System;
using System.Collections.Generic;
using System.Text;

namespace CypherBot.Core.Models
{
	public class ArtifactQuirk
	{
		public int ArtifactQuirkId { get; set; }
		public int StartRange { get; set; }
		public int EndRange { get; set; }
		public string Quirk { get; set; }
	}
}
