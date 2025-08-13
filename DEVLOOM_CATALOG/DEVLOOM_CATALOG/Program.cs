using DEVLOOM_CATALOG.Domain;
using DEVLOOM_CATALOG.Infra.Interfaces;
using DEVLOOM_CATALOG.Infra.Repositories;
using DEVLOOM_CATALOG.Services.Interfaces;
using DEVLOOM_CATALOG.Services.Service;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Registrar AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Registrar repositories
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();

// Registrar services
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<IProdutoService, ProdutoService>();

// Registrar IDbConnection (exemplo usando SQL Server)
builder.Services.AddScoped<System.Data.IDbConnection>(sp =>
{
    var connectionString = builder.Configuration.GetConnectionString("Connection");
    return new SqlConnection(connectionString);
});

// Registrar Swagger/OpenAPI (opcional, mas útil)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
