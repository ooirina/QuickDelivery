using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QuickDelivery.Web.Data;
using QuickDelivery.Web.Models;
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddRazorPages();
builder.Services.AddAuthorization();
builder.Services.AddDbContext<QuickDeliveryWebContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("QuickDeliveryWebContext") ?? throw new InvalidOperationException("Connection string 'QuickDeliveryWebContext' not found.")));


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<QuickDeliveryWebContext>();

    // Se asigură că baza de date și tabelele sunt create
    context.Database.EnsureCreated();

    // 1. SEED CLIENTI 
    if (!context.Client.Any())
    {
        context.Client.Add(new Client
        {
            Nume = "Ion Popescu",
            Email = "ion.popescu@gmail.com",
            Telefon = "0744444444"
        });
        context.SaveChanges();
    }

    // 2. SEED CATEGORII 
    if (!context.Categorie.Any())
    {
        context.Categorie.AddRange(
            new Categorie { Nume = "Pizza", Iconita = "e50695_pizza.png" },
            new Categorie { Nume = "Sushi", Iconita = "cee7e7_sushi.png" },
            new Categorie { Nume = "Pui", Iconita = "c075a3_fried-chicken.png" },
            new Categorie { Nume = "Burger", Iconita = "923b65_hamburger.png" },
            new Categorie { Nume = "Salata", Iconita = "062a79_salad.png" },
            new Categorie { Nume = "Supa", Iconita = "45eb74_soup-plate.png" },
            new Categorie { Nume = "Desert", Iconita = "7db943_cake.png" },
            new Categorie { Nume = "Peste", Iconita = "d9f4bb_fish-food.png" },
            new Categorie { Nume = "Vita", Iconita = "131b6bc5_steak-medium.png" },
            new Categorie { Nume = "Porc", Iconita = "66420f_pig-face-emoji.png" },
            new Categorie { Nume = "Paste", Iconita = "b1afcc_emoji_u1f35d.png" }
        );
        context.SaveChanges();
    }

    // 3. SEED RESTAURANTE 
    if (!context.Restaurant.Any())
    {
        context.Restaurant.AddRange(
            new Restaurant { Nume = "Amadeus", Adresa = "Strada Motilor, Cluj-Napoca", ImagineUrl = "/images/35acdaa3-1dd5-43f5-b3b1-3cfed7f3e210_ghent-apr-28-exterior-view-of-the-famous-amadeus-restaurant-on-apr-28-2018-at-ghent-belgium-PJKJ5R.jpg" },
            new Restaurant { Nume = "Marty", Adresa = "Strada Alexandru Vaida Voevod 53-55, Cluj-Napoca", ImagineUrl = "/images/2bcd603b-4ce1-89f5-5e67feb5af19_185677-marty-restaurant.jpg" },
            new Restaurant { Nume = "Samsara", Adresa = "Strada Cardinal Iuliu Hossu 3, Cluj-Napoca", ImagineUrl = "/images/4fc560ae-4bcc-4af1-b2fc-eb7e9b89faf1_2018_10_samsara_catalinhladi_9618-big.jpg" },
            new Restaurant { Nume = "Meat Up", Adresa = "Strada Gheorghe Sincai 14, Cluj-Napoca", ImagineUrl = "/images/f545f59a-5c59-45cb-8b87-59faa9489169_12-big.jpg" },
            new Restaurant { Nume = "Roata", Adresa = "Strada Alexandru Ciurea 6, Cluj-Napoca", ImagineUrl = "/images/199dc2b1-37e7-4639-8f76-3e8873765175_roata-10668-big.jpg" },
            new Restaurant { Nume = "Livada", Adresa = "Strada Clinicilor 14, Cluj-Napoca", ImagineUrl = "/images/975df9f7-d302-4be1-9053-bcfa1f306529_restaurant-livada-10135-big.jpg" }
        );
        context.SaveChanges();
    }

    // 4. SEED PRODUSE 
    if (!context.Produs.Any())
    {

        var idLivada = context.Restaurant.First(r => r.Nume == "Livada").Id;
        var idAmadeus = context.Restaurant.First(r => r.Nume == "Amadeus").Id;
        var idMarty = context.Restaurant.First(r => r.Nume == "Marty").Id;

        var idCatPui = context.Categorie.First(c => c.Nume == "Pui").Id;
        var idCatPeste = context.Categorie.First(c => c.Nume == "Peste").Id;

        context.Produs.AddRange(
            new Produs { Nume = "Degetele de pui", Pret = 33.00m, Descriere = "Piept de pui panetat 250g, cartofi prajiti 150g, sos remoulade 50g (cu gluten, lapte)", RestaurantId = idAmadeus, CategorieId = idCatPui },
            new Produs { Nume = "Quesadilla de pui", Pret = 42.00m, Descriere = "Piept de pui 150g, cartofi prajiti 150g, intr-o lipie cu porumb 30g, ardei gras 30g, mozzarella, smantana, sos picant, dulceata de ardei iute", RestaurantId = idAmadeus, CategorieId = idCatPui },
            new Produs { Nume = "Kentucky Wings", Pret = 38.00m, Descriere = "Aripioare de pui glazurate 250g, cartofi prajiti 150g, dulceata de ardei iute 50g", RestaurantId = idLivada, CategorieId = idCatPui },
            new Produs { Nume = "Salmone Kempinski", Pret = 49.00m, Descriere = "220g file de somon, 60g creveti, sos roze, 160g orez basmati, 40ml smantana, legume, condimente", RestaurantId = idMarty, CategorieId = idCatPeste }
        );
        context.SaveChanges();
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
