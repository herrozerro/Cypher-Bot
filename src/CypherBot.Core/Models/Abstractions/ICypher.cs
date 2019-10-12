namespace CypherBot.Core.Models.Abstractions
{
    public interface ICypher
    {
        string Effect { get; set; }
        int LevelBonus { get; set; }
        int LevelDie { get; set; }
        string Name { get; set; }
        string Source { get; set; }
        string Type { get; set; }
    }
}