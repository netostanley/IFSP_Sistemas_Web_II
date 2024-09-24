using TP01.Models;
using TP01.Routes;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

var app = builder.Build();

IWebHost host = new WebHostBuilder().UseKestrel().UseStartup<Rotas>().Build();
host.Run();

Rotas r = new Rotas();

r.Configure(app);