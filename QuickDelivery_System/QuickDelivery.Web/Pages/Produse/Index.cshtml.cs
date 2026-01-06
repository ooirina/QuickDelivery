using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QuickDelivery.Web.Data;
using QuickDelivery.Web.Models;

namespace QuickDelivery.Web.Pages.Produse
{
    public class IndexModel : PageModel
    {
        private readonly QuickDelivery.Web.Data.QuickDeliveryWebContext _context;

        public IndexModel(QuickDelivery.Web.Data.QuickDeliveryWebContext context)
        {
            _context = context;
        }

        public IList<Produs> Produs { get; set; } = default!;
        public IList<QuickDelivery.Web.Models.Categorie> ListaCategorii { get; set; } = default!;
        public async Task OnGetAsync(string? SearchString)
        {
            ListaCategorii = await _context.Categorie.ToListAsync();
            var produse = from p in _context.Produs.Include(p => p.Restaurant)
                                                   .Include(p => p.Categorie)
                                                   select p;

            if (!string.IsNullOrEmpty(SearchString))
            {
                // Filtrăm produsele unde numele sau descrierea conțin categoria selectată
                produse = produse.Where(s => s.Categorie.Nume == SearchString);
            }

            Produs = await produse.ToListAsync();
        }
    }
}
