using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QuickDelivery.Web.Data;
using QuickDelivery.Web.Models;

namespace QuickDelivery.Web.Pages.Recenzii
{
    public class IndexModel : PageModel
    {
        private readonly QuickDelivery.Web.Data.QuickDeliveryWebContext _context;

        public IndexModel(QuickDelivery.Web.Data.QuickDeliveryWebContext context)
        {
            _context = context;
        }

        public IList<Recenzie> Recenzie { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Recenzie = await _context.Recenzii
                .Include(r => r.Client)
                .Include(r => r.Restaurant).ToListAsync();
        }
    }
}
