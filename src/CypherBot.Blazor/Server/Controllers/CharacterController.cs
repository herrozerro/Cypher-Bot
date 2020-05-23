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

        [HttpGet("Character/{id}")]
        public async Task<Character> GetCharacter([FromRoute]int id)
        {
            var chr = await cypherContext.Characters
                    .Include(x => x.Cyphers)
                    .Include(x => x.Inventory)
                    .Include(x => x.RecoveryRolls)
                    .Include(x => x.Pools)
                    .Include(x => x.Abilities)
                    .FirstOrDefaultAsync(x => x.CharacterId == id);

            return chr;
        }

        [HttpPut("Character/{id}")]
        public async Task<IActionResult> UpdateCharacter([FromRoute] int id, [FromBody] Character character)
        {
            if (await cypherContext.Characters.AnyAsync(x => x.CharacterId == id))
            {
                try
                {
                    cypherContext.Attach(character).State = EntityState.Modified;

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

        [HttpPost("Character")]
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
