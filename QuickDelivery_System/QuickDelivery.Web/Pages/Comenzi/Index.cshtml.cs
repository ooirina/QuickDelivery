using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QuickDelivery.Web.Data;
using QuickDelivery.Web.Models;

namespace QuickDelivery.Web.Pages.Comenzi
{
    public class IndexModel : PageModel
    {
        private readonly QuickDelivery.Web.Data.QuickDeliveryWebContext _context;

        public IndexModel(QuickDelivery.Web.Data.QuickDeliveryWebContext context)
        {
            _context = context;
        }

        public IList<Comanda> Comanda { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Comanda = await _context.Comanda
                .Include(c => c.Client).ToListAsync();
        }
    }
}
