using RecruitmentManager.Web.ApiClients;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var baseUrl = builder.Configuration["ApiBaseUrl"] ?? "https://localhost:7186/";

builder.Services.AddHttpClient<ICandidatesApiClient, CandidatesApiClient>(client =>
{
    client.BaseAddress = new Uri(baseUrl);
});

builder.Services.AddHttpClient<IStatesApiClient, StatesApiClient>(client =>
{
    client.BaseAddress = new Uri(baseUrl);
});

builder.Services.AddHttpClient<ICountriesApiClient, CountriesApiClient>(client =>
{
    client.BaseAddress = new Uri(baseUrl);
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
