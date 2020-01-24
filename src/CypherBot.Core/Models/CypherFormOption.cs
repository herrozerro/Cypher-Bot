namespace CypherBot.Core.Models
{
    public class CypherFormOption
    {
        public int FormOptionId { get; set; }
        public int CypherId { get; set; }
        public string Form { get; set; }
        public string FormDescription { get; set; }

        public Cypher Cypher { get; set; }
    }
}
