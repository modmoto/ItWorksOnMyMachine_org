using ItWorksOnMyMachine_org.Pages;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddSingleton(_ =>
{
    var connectionString = Environment.GetEnvironmentVariable("MONGO_DB_CONNECTION_STRING");
    return new MongoClient(connectionString);
});

builder.Services.AddScoped<ImageRepository>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();