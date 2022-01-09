using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Options;
using MudBlazor.Services;
using WePing;
using WePing.Service.Spid;
using WePing.Service;
using WePing.Gir;
using WeMediatCrud;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<Navigation>();
builder.Services.AddMudServices();
builder.Services.Configure<SpidOptions>(builder.Configuration.GetSection(SpidOptions.SPID));
builder.Services.Configure<GirOptions>(builder.Configuration.GetSection(GirOptions.GIR));
builder.Services.AddSpid((a) => { } );

var connectionString=builder.Configuration.GetConnectionString("spiddb");
var types=builder.Services.AddGir(connectionString);

builder.Services.AddMediatCrud(new List<Assembly>() {typeof(Temp).Assembly, typeof(GirContext).Assembly });

var app = builder.Build();

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

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
