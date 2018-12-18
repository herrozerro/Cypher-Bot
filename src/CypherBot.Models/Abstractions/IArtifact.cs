using System;
using System.Collections.Generic;
using System.Text;

namespace CypherBot.Models.Abstractions
{
    interface IArtifact
    {
        int ArtifactId { get; set; }
        string Name {get; set;}
        string Form {get; set;}
        string Genre {get; set;}

        int Level {get; set;}
        int LevelDie {get; set;}
        int LevelBonus {get; set;}

        string Effect {get; set;}
        string Source {get; set;}
        string Depletion {get; set;}

    }
}
