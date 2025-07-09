using API_assistencia_tecnica.DataContexts;
using API_assistencia_tecnica.Services;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Adiciona o DbContext com MySQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 32)) // use sua vers�o do MySQL
    )
);

builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<EquipamentoService>();
builder.Services.AddScoped<FornecedorService>();
builder.Services.AddScoped<OrcamentoService>();
builder.Services.AddScoped<PecaService>();
builder.Services.AddScoped<ReparoService>();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Configure o pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
