using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QuickDelivery.Web.Data;
using QuickDelivery.Web.Models;

namespace QuickDelivery.Web.Pages.Restaurante
{
    public class IndexModel : PageModel
    {
        private readonly QuickDelivery.Web.Data.QuickDeliveryWebContext _context;

        public IndexModel(QuickDelivery.Web.Data.QuickDeliveryWebContext context)
        {
            _context = context;
        }

        public IList<Restaurant> Restaurant { get; set; } = default!;

        // Adăugăm proprietatea pentru string-ul de căutare
        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public async Task OnGetAsync()
        {
            // 1. Plecăm de la interogarea de bază
            var restaurante = from r in _context.Restaurant
                              select r;

            // 2. Dacă utilizatorul a scris ceva în bara de căutare, filtrăm rezultatele
            if (!string.IsNullOrEmpty(SearchString))
            {
                restaurante = restaurante.Where(s => s.Nume.Contains(SearchString)
                                               || s.Adresa.Contains(SearchString));
            }

            // 3. Executăm interogarea și populăm lista
            Restaurant = await restaurante.ToListAsync();
        }
    }
}