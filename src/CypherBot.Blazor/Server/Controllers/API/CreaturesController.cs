using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CypherBot.Core.DataAccess.Repos;
using CypherBot.Core.Models;
using Microsoft.AspNetCore.Authorization;

namespace CypherBot.Blazor.Server.Controllers.API
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CreaturesController : ControllerBase
    {
        private readonly CypherContext _context;

        public CreaturesController(CypherContext context)
        {
            _context = context;
        }

        // GET: api/Creatures
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Creature>>> GetCreatures()
        {
            return await _context.Creatures.ToListAsync();
        }

        // GET: api/Creatures/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Creature>> GetCreature(int id)
        {
            var creature = await _context.Creatures.FindAsync(id);

            if (creature == null)
            {
                return NotFound();
            }

            return creature;
        }

        // PUT: api/Creatures/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCreature(int id, Creature creature)
        {
            if (id != creature.CreatureId)
            {
                return BadRequest();
            }

            _context.Entry(creature).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CreatureExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Creatures
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Creature>> PostCreature(Creature creature)
        {
            _context.Creatures.Add(creature);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCreature", new { id = creature.CreatureId }, creature);
        }

        // DELETE: api/Creatures/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Creature>> DeleteCreature(int id)
        {
            var creature = await _context.Creatures.FindAsync(id);
            if (creature == null)
            {
                return NotFound();
            }

            _context.Creatures.Remove(creature);
            await _context.SaveChangesAsync();

            return creature;
        }

        private bool CreatureExists(int id)
        {
            return _context.Creatures.Any(e => e.CreatureId == id);
        }
    }
}
