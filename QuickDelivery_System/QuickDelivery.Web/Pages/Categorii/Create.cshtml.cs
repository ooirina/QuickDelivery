using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
using QuickDelivery.Web.Data;
using QuickDelivery.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickDelivery.Web.Pages.Categorii
{
    public class CreateModel : PageModel
    {
        private readonly QuickDelivery.Web.Data.QuickDeliveryWebContext _context;
        private readonly IWebHostEnvironment _environment;
        public CreateModel(QuickDelivery.Web.Data.QuickDeliveryWebContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Categorie Categorie { get; set; } = default!;

       
        public async Task<IActionResult> OnPostAsync(IFormFile? imagineIncarcata)
        {
            if (!ModelState.IsValid || imagineIncarcata == null)
            {
                return Page();
            }
            // 1. Generăm un nume unic pentru fișier ca să nu se suprascrie
            string numeFisier = Guid.NewGuid().ToString() + "_" + imagineIncarcata.FileName;

            // 2. Stabilim calea unde salvăm (în folderul pe care l-ai creat deja)
            string caleSalvare = Path.Combine(_environment.WebRootPath, "images", "categorii", numeFisier);

            // 3. Salvăm fizic fișierul pe disc
            using (var stream = new FileStream(caleSalvare, FileMode.Create))
            {
                await imagineIncarcata.CopyToAsync(stream);
            }

            // 4. Salvăm în baza de date doar numele fișierului
            Categorie.Iconita = numeFisier;

            _context.Categorie.Add(Categorie);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
