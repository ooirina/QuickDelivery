using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QuickDelivery.Web.Data;
using QuickDelivery.Web.Models;
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddRazorPages();
builder.Services.AddControllers();
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
            new Categorie { Nume = "Pizza", Iconita = "41a45a4a-4820-4640-9326-9fad7de50695_pizza.png" },
            new Categorie { Nume = "Sushi", Iconita = "7aa57afe-7e78-4277-a92c-f41559cee7e7_sushi.png" },
            new Categorie { Nume = "Pui", Iconita = "80b79d8f-809d-4dfc-82ee-3de842c075a3_fried-chicken.png" },
            new Categorie { Nume = "Burger", Iconita = "1cb30fe9-7558-4c1f-a97a-61250c923b65_hamburger.png" },
            new Categorie { Nume = "Salata", Iconita = "998de229-ce67-42dd-bffe-8de307062a79_salad.png" },
            new Categorie { Nume = "Supa", Iconita = "a19fd42d-35c4-4218-af4c-b7520845eb74_soup-plate.png" },
            new Categorie { Nume = "Desert", Iconita = "7ac8efb5-3202-4e18-88a7-2f6abf7db943_cake.png" },
            new Categorie { Nume = "Peste", Iconita = "5cb6fa0f-8596-4570-8963-64cf88d9f4bb_fish-food.png" },
            new Categorie { Nume = "Vita", Iconita = "c18510a3-da4b-4d24-9649-060a131b6bc5_steak-medium.png" },
            new Categorie { Nume = "Porc", Iconita = "b70b026e-2fef-4645-b6fd-7e6e8966420f_pig-face-emoji.png" },
            new Categorie { Nume = "Paste", Iconita = "77687b7a-08f3-4c64-afaa-61e3f8b1afcc_emoji_u1f35d.png" }
        );
        context.SaveChanges();
    }

    // 3. SEED RESTAURANTE 
    if (!context.Restaurant.Any())
    {
        context.Restaurant.AddRange(
            new Restaurant { Nume = "Amadeus", Adresa = "Strada Motilor, Cluj-Napoca", ImagineUrl = "/images/35acdaa3-1dd5-43f5-b3b1-3cfed7f3e210_ghent-apr-28-exterior-view-of-the-famous-amadeus-restaurant-on-apr-28-2018-at-ghent-belgium-PJKJ5R.jpg" },
            new Restaurant { Nume = "Marty", Adresa = "Strada Alexandru Vaida Voevod 53-55, Cluj-Napoca", ImagineUrl = "/images/2bcd603b-47c1-4ce1-89f5-5e67feb5af19_185677-marty-restaurant.jpg" },
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

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.Run();
