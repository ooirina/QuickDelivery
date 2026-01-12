using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuickDelivery.Web.Data;
using QuickDelivery.Web.Models;
using DTO = QuickDelivery.Web.Models;



namespace QuickDelivery.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestauranteController : ControllerBase
    {
        private readonly QuickDeliveryWebContext _context;

        public RestauranteController(QuickDeliveryWebContext context)
        {
            _context = context;
        }

        

        // GET: /Restaurante/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Restaurant>> GetRestaurant(int id)
        {
            var restaurant = await _context.Restaurant.FindAsync(id);

            if (restaurant == null)
            {
                return NotFound();
            }

            return restaurant;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetRestaurant()
        {
            var baseUrl = $"{Request.Scheme}://{Request.Host}";

            var restaurants = await _context.Restaurant
            .Select(r => new DTO.RestaurantDto
            {
                Id = r.Id,
                Nume = r.Nume,
                Adresa = r.Adresa,
                ImagineUrl = r.ImagineUrl.EndsWith(".jpg") || r.ImagineUrl.EndsWith(".png")
                             ? baseUrl + r.ImagineUrl
                             : baseUrl + r.ImagineUrl + ".jpg",
                Latitude = r.Latitude,
                Longitude = r.Longitude,
            })
            .ToListAsync();


            return Ok(restaurants);
        }




        [HttpPost]
        public async Task<ActionResult<Restaurant>> PostRestaurant(Restaurant restaurant)
        {
            _context.Restaurant.Add(restaurant);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRestaurant", new { id = restaurant.Id }, restaurant);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurant(int id)
        {
            var restaurant = await _context.Restaurant.FindAsync(id);
            if (restaurant == null)
            {
                return NotFound();
            }

            _context.Restaurant.Remove(restaurant);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RestaurantExists(int id)
        {
            return _context.Restaurant.Any(e => e.Id == id);
        }
    }
}