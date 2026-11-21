using Application.Interfaces;
using Application.Services;
using Infrastructure.DATABASE;
using Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IAsiento_Contable, Asiento_Contablerepository>();
builder.Services.AddScoped<Asiento_ContableService>();

builder.Services.AddScoped<ICliente, Clienterepository>();
builder.Services.AddScoped<ClienteServices>();

builder.Services.AddScoped<ICategoria, Categoriarepository>();
builder.Services.AddScoped<CategoriaServices>();

builder.Services.AddScoped<IDetalle_Comanda, Detalle_Comandarepository>();
builder.Services.AddScoped<Detalle_ComandaService>();

builder.Services.AddScoped<IDireccion, Direccionrepository>();
builder.Services.AddScoped<DireccionService>();

builder.Services.AddScoped<IFactura, Facturarepository>();
builder.Services.AddScoped<FacturaService>();

builder.Services.AddScoped<IKardex, Kardexrepository>();
builder.Services.AddScoped<KardexService>();

builder.Services.AddScoped<IMesa, Mesarepository>();
builder.Services.AddScoped<MesaService>();

builder.Services.AddScoped<IProducto, Productorepository>();
builder.Services.AddScoped<ProductoService>();

builder.Services.AddScoped<ITipo_de_Cuenta, Tipo_de_Cuentarepository>();
builder.Services.AddScoped<Tipo_de_CuentaService>();

builder.Services.AddScoped<IUsuario, Usuariorepository>();
builder.Services.AddScoped<UsuarioService>();

builder.Services.AddScoped<IZona, Zonarepository>();
builder.Services.AddScoped<ZonaService>();


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var connection = builder.Configuration.GetConnectionString("default");
builder.Services.AddSingleton(new DBConnectionfactory(connection!));



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(s =>
{
    s.SwaggerEndpoint("/swagger/v1/swagger.json", "Api del Bar");
    s.RoutePrefix = string.Empty;
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
