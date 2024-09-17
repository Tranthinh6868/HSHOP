
//using Microsoft.AspNetCore.Mvc.Infrastructure;
//using Microsoft.Extensions.DependencyInjection.Extensions;
using WebApp.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<VnPayment>(builder.Configuration.GetSection("Payments:VnPayment"));
builder.Services.AddScoped<VnPaymentService>(); 
builder.Services.AddHttpContextAccessor();
builder.Services.AddMvc();
var app = builder.Build();
app.UseStaticFiles();
app.MapDefaultControllerRoute();
app.Run();
