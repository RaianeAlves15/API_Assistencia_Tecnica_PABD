using API_assistencia_tecnica.DataContexts;
using API_assistencia_tecnica.Services;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;

Env.Load(); // Carrega o .env automaticamente

var builder = WebApplication.CreateBuilder(args);

// Recupera a connection string do .env
var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION");

// Configura o DbContext com MySQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 32)))
);

// Serviços
builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<EquipamentoService>();
builder.Services.AddScoped<FornecedorService>();
builder.Services.AddScoped<OrcamentoService>();
builder.Services.AddScoped<PecaService>();
builder.Services.AddScoped<ReparoService>();
builder.Services.AddScoped<OrcamentoPecaService>();
builder.Services.AddScoped<ReparoEquipamentoService>();
builder.Services.AddScoped<FornecedorPecasService>();

// Controllers e Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// Pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
