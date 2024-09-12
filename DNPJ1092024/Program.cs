var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.                         
if (app.Environment.IsDevelopment())                            
{                                                               
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

var productos = new List<Producto>();

app.MapGet("/productos", () =>
{
    return productos;
});

app.MapGet("/productos/{id}", (int id) =>
{
    var producto = productos.FirstOrDefault(p => p.Id == id);
    return producto;
});


app.MapPost("/productos", (Producto producto) =>
{
    productos.Add(producto);
    return Results.Ok();
});
 
app.MapPut("/productos/{id}", (int id, Producto producto) =>
{
    var existingProducto = productos.FirstOrDefault(p => p.Id == id);
    if (existingProducto != null)
    {
        existingProducto.Nombre = producto.Nombre;
        existingProducto.Precio = producto.Precio;
        return Results.Ok();
    }
    else
    {
        return Results.NotFound();
    }
});

app.MapDelete("/productos/{id}", (int id) =>
{
    var existingProductos = productos.FirstOrDefault(p => p.Id == id);
    if (existingProductos != null)
    {
        productos.Remove(existingProductos);
        return Results.Ok();
    }
    else
    {
        return Results.NotFound();
    }
});

app.Run();

    internal class Producto
    {
        public int Id { get; set; }

        public string? Nombre { get; set; }

        public decimal Precio { get; set; }
    }

