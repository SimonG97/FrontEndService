using FrontEndService;
using FrontEndService.Services;
using FrontEndService.Services.IServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//configuring Httpclient
builder.Services.AddHttpClient<IProductService, ProductService>();
//adding the baseurl
SD.ProductAPIBase = builder.Configuration["ServiceUrls:ProductAPI"];
//configuring the productservice
builder.Services.AddScoped<IProductService, ProductService>();

//runtime changes for razor
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
