using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using QuickDelivery.Web.Data;
using QuickDelivery.Web.Models;

var builder = WebApplication.CreateBuilder(args);

// --- CONFIGURARE BAZE DE DATE ---

/// 2. Contextul pentru Securitate (Identity) - SCHIMBĂ ACEASTĂ LINIE:
builder.Services.AddDbContext<IdentityContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityContextConnection")));
// 2. Contextul pentru Datele Aplicației (Restaurante) - Baza ta
builder.Services.AddDbContext<QuickDeliveryWebContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("QuickDeliveryWebContext")));

// --- CONFIGURARE IDENTITY ---

builder.Services.AddDefaultIdentity<IdentityUser>(options => {
    options.SignIn.RequireConfirmedAccount = true; // Necesită EmailConfirmed = 1 în SQL
})
.AddRoles<IdentityRole>() // Esențial pentru roluri (Admin/Client)
.AddEntityFrameworkStores<IdentityContext>();

builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddAuthorization();

var app = builder.Build();

// --- LOGICĂ DE PORNIRE (SEED) ---

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    // Asigurăm crearea rolului Admin
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    if (!await roleManager.RoleExistsAsync("Admin"))
    {
        await roleManager.CreateAsync(new IdentityRole("Admin"));
    }

    // Populare Restaurante în Baza TA
    var context = services.GetRequiredService<QuickDeliveryWebContext>();
    context.Database.EnsureCreated(); // Creează tabelele dacă nu există în baza ta

    if (!context.Restaurant.Any())
    {
        context.Restaurant.AddRange(
            new Restaurant { Nume = "Amadeus", Adresa = "Strada Motilor, Cluj-Napoca", Latitude = 46.7712, Longitude = 23.5897, ImagineUrl = "/images/amadeus.jpg" },
            new Restaurant { Nume = "Marty", Adresa = "Strada Alexandru Vaida Voevod, Cluj-Napoca", Latitude = 46.7765, Longitude = 23.6212, ImagineUrl = "/images/marty.jpg" }
            // Adaugă restul aici...
        );
        context.SaveChanges();
    }
}

// --- MIDDLEWARE ---

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication(); // Activează Login-ul
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers(); // Necesar pentru API-ul mobil

app.Run();