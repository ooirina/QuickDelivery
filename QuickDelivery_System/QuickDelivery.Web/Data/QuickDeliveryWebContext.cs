using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuickDelivery.Web.Models;

namespace QuickDelivery.Web.Data
{
    public class QuickDeliveryWebContext : DbContext
    {
        public QuickDeliveryWebContext (DbContextOptions<QuickDeliveryWebContext> options)
            : base(options)
        {
        }

        public DbSet<QuickDelivery.Web.Models.Client> Client { get; set; } = default!;
        public DbSet<QuickDelivery.Web.Models.Comanda> Comanda { get; set; } = default!;
        public DbSet<QuickDelivery.Web.Models.Produs> Produs { get; set; } = default!;
        public DbSet<QuickDelivery.Web.Models.Restaurant> Restaurant { get; set; } = default!;
        public DbSet<QuickDelivery.Web.Models.Recenzie> Recenzii { get; set; } = default!;
        public DbSet<QuickDelivery.Web.Models.Categorie> Categorie { get; set; } = default!;
    }
}
