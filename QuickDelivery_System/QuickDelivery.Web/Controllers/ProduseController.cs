using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuickDelivery.Web.Data;
using QuickDelivery.Web.Models;

namespace QuickDelivery.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProduseController : ControllerBase
    {
        private readonly QuickDeliveryWebContext _context;

        public ProduseController(QuickDeliveryWebContext context)
        {
            _context = context;
        }

        // GET: api/Produse
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produs>>> GetProdus()
        {
            return await _context.Produs.ToListAsync();
        }

        // GET: api/Produse/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Produs>> GetProdus(int id)
        {
            var produs = await _context.Produs.FindAsync(id);

            if (produs == null)
            {
                return NotFound();
            }

            return produs;
        }

        // PUT: api/Produse/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProdus(int id, Produs produs)
        {
            if (id != produs.Id)
            {
                return BadRequest();
            }

            _context.Entry(produs).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdusExists(id))
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

        // POST: api/Produse
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Produs>> PostProdus(Produs produs)
        {
            _context.Produs.Add(produs);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProdus", new { id = produs.Id }, produs);
        }

        // DELETE: api/Produse/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProdus(int id)
        {
            var produs = await _context.Produs.FindAsync(id);
            if (produs == null)
            {
                return NotFound();
            }

            _context.Produs.Remove(produs);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProdusExists(int id)
        {
            return _context.Produs.Any(e => e.Id == id);
        }

        // GET: api/Produse/ByRestaurant/5
        [HttpGet("ByRestaurant/{restaurantId}")]
        public async Task<ActionResult<IEnumerable<Produs>>> GetProduseByRestaurant(int restaurantId)
        {
            // Filtrează produsele în funcție de ID-ul restaurantului selectat pe mobil
            return await _context.Produs
                .Where(p => p.RestaurantId == restaurantId)
                .ToListAsync();
        }
    }
}
