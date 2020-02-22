namespace CypherBot.Core.Models
{
    public class CypherEffectOption
    {
        public int EffectOptionId { get; set; }
        public int CypherId { get; set; }
        public int StartRange { get; set; }
        public int EndRange { get; set; }
        public string EffectDescription { get; set; }

        public Cypher Cypher { get; set; }
    }
}
