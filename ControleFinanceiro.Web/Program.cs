using ControleFinanceiro.Aplicacao.Services;
using ControleFinanceiro.Application.Interfaces;
using ControleFinanceiro.Infra.Data;
using ControleFinanceiro.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Adiciona serviços do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    //Inclui comentários XML (para gerar descrições automáticas no Swagger)
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
    if (File.Exists(xmlPath))
        options.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
});

// DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositories
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<ITransacaoRepository, TransacaoRepository>();

// Services
builder.Services.AddScoped<CategoriaService>();
builder.Services.AddScoped<TransacaoService>();
builder.Services.AddScoped<DashboardService>();
builder.Services.AddScoped<RelatorioService>();

var app = builder.Build();

// --- Middleware swagger ---
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.DocumentTitle = "Controle Financeiro API";
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Controle Financeiro - API v1");
        options.RoutePrefix = "swagger"; 
    });
}

// --- Middleware padrão ---
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
