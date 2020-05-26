using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CypherBot.Core.DataAccess.Repos;
using CypherBot.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CypherBot.Blazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly CypherContext cypherContext = null;

        public CharacterController(CypherContext cypherContext)
        {
            this.cypherContext = cypherContext;
        }

        [HttpGet("{id}")]
        public async Task<Character> GetCharacter([FromRoute] int id)
        {
            var chr = await cypherContext.Characters
                    .Include(x => x.Cyphers)
                    .Include(x => x.Artifacts)
                    .Include(x => x.Inventory)
                    .Include(x => x.RecoveryRolls)
                    .Include(x => x.Pools)
                    .Include(x => x.Abilities)
                    .FirstOrDefaultAsync(x => x.CharacterId == id);

            return chr;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCharacter([FromRoute] int id, [FromBody] Character character)
        {
            var existingModel = await cypherContext.Characters
                    .Include(x => x.Cyphers)
                    .Include(x => x.Artifacts)
                    .Include(x => x.Inventory)
                    .Include(x => x.RecoveryRolls)
                    .Include(x => x.Pools)
                    .Include(x => x.Abilities)
                    .FirstOrDefaultAsync(x => x.CharacterId == id);

            if (existingModel != null)
            {
                try
                {
                    //update character
                    cypherContext.Entry(existingModel).CurrentValues.SetValues(character);

                    //delete ability not in saved character list
                    foreach (var ability in existingModel.Abilities)
                    {
                        if (!character.Abilities.Any(c => c.CharacterAbilityId == ability.CharacterAbilityId))
                            cypherContext.CharacterAbilities.Remove(ability);
                    }

                    //Update and add ability
                    foreach (var ability in character.Abilities)
                    {
                        var existingAbility = existingModel.Abilities
                            .Where(x => x.CharacterAbilityId == ability.CharacterAbilityId)
                            .SingleOrDefault();

                        //Update Cypher
                        if (existingAbility != null)
                        {
                            cypherContext.Entry(existingAbility).CurrentValues.SetValues(ability);

                        }
                        //Add Cypher
                        else
                        {
                            existingModel.Abilities.Add(ability);
                        }
                    }

                    //delete inventory not in saved character list
                    foreach (var inventory in existingModel.Inventory)
                    {
                        if (!character.Inventory.Any(c => c.InventoryId == inventory.InventoryId))
                            cypherContext.CharacterInventories.Remove(inventory);
                    }

                    //Update and add inventory
                    foreach (var inventory in character.Inventory)
                    {
                        var existingInventory = existingModel.Inventory
                            .Where(x => x.InventoryId == inventory.InventoryId)
                            .SingleOrDefault();

                        //Update Cypher
                        if (existingInventory != null)
                        {
                            cypherContext.Entry(existingInventory).CurrentValues.SetValues(inventory);

                        }
                        //Add Cypher
                        else
                        {
                            existingModel.Inventory.Add(inventory);
                        }
                    }

                    //delete cyphers not in saved character list
                    foreach (var cypher in existingModel.Cyphers)
                    {
                        if (!character.Cyphers.Any(c => c.CypherId == cypher.CypherId))
                            cypherContext.CharacterCyphers.Remove(cypher);
                    }

                    //Update and add cyphers
                    foreach (var cypher in character.Cyphers)
                    {
                        var existingCypher = existingModel.Cyphers
                            .Where(x => x.CypherId == cypher.CypherId)
                            .SingleOrDefault();

                        //Update Cypher
                        if (existingCypher != null)
                        {
                            cypherContext.Entry(existingCypher).CurrentValues.SetValues(cypher);

                        }
                        //Add Cypher
                        else
                        {
                            existingModel.Cyphers.Add(cypher);
                        }
                    }

                    //delete artifacts not in saved character list
                    foreach (var artifact in existingModel.Artifacts)
                    {
                        if (!character.Pools.Any(c => c.PoolId == artifact.ArtifactId))
                            cypherContext.CharacterArtifacts.Remove(artifact);
                    }

                    //Update and add artifacts
                    foreach (var artifact in character.Artifacts)
                    {
                        var existingArtifact = existingModel.Artifacts
                            .Where(x => x.ArtifactId == artifact.ArtifactId)
                            .SingleOrDefault();

                        //Update Cypher
                        if (existingArtifact != null)
                        {
                            cypherContext.Entry(existingArtifact).CurrentValues.SetValues(artifact);

                        }
                        //Add Cypher
                        else
                        {
                            existingModel.Artifacts.Add(artifact);
                        }
                    }

                    //delete pools not in saved character list
                    foreach (var pool in existingModel.Pools)
                    {
                        if (!character.Pools.Any(c => c.PoolId == pool.PoolId))
                            cypherContext.CharacterPools.Remove(pool);
                    }

                    //Update and add pools
                    foreach (var pool in character.Pools)
                    {
                        var existingPool = existingModel.Pools
                            .Where(x => x.PoolId == pool.PoolId)
                            .SingleOrDefault();

                        //Update Cypher
                        if (existingPool != null)
                        {
                            cypherContext.Entry(existingPool).CurrentValues.SetValues(pool);

                        }
                        //Add Cypher
                        else
                        {
                            existingModel.Pools.Add(pool);
                        }
                    }

                    //delete recovery rolls not in saved character list
                    foreach (var recoveryRoll in existingModel.RecoveryRolls)
                    {
                        if (!character.RecoveryRolls.Any(c => c.RecoveryRollId == recoveryRoll.RecoveryRollId))
                            cypherContext.CharacterRecoveryRolls.Remove(recoveryRoll);
                    }

                    //Update and add pools
                    foreach (var recoveryRoll in character.RecoveryRolls)
                    {
                        var existingRecoveryRoll = existingModel.RecoveryRolls
                            .Where(x => x.RecoveryRollId == recoveryRoll.RecoveryRollId)
                            .SingleOrDefault();

                        //Update Cypher
                        if (existingRecoveryRoll != null)
                        {
                            cypherContext.Entry(existingRecoveryRoll).CurrentValues.SetValues(recoveryRoll);

                        }
                        //Add Cypher
                        else
                        {
                            existingModel.RecoveryRolls.Add(recoveryRoll);
                        }
                    }

                    await cypherContext.SaveChangesAsync();

                    return Ok(character);
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }

            return BadRequest();
        }

        [HttpPost()]
        public async Task<IActionResult> CreateCharacter([FromBody] Character character)
        {
            if (character.CharacterId != 0)
            {
                return BadRequest();
            }

            try
            {
                cypherContext.Add(character);
                await cypherContext.SaveChangesAsync();
                return Ok(character);
            }
            catch (Exception)
            {
                return BadRequest();
                throw;
            }
        }
    }
}
