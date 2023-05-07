using ToDoApi;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ToDoDbContext>(opt => opt.UseMySql(ServerVersion.AutoDetect(builder.Configuration["ToDoDB"])));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
           {
               builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
           }));
var app = builder.Build();
// if (app.Environment.IsDevelopment())
// {
    app.UseSwagger();
    app.UseSwaggerUI();
// }
app.UseCors("corsapp");
app.UseHttpsRedirection();
app.MapGet("/items", async (ToDoDbContext context) => { System.Console.WriteLine("I am here" ); return await context.Items.ToListAsync();} );
app.MapGet("/",()=>"ToDoList Server is running");
app.MapPost("/items", async (ToDoDbContext context, Item item) =>
{
    EntityEntry<Item> itemToReturn = context.Items.Add(item); await context.SaveChangesAsync();
    return itemToReturn.Entity;
});
app.MapPut("/items", async (ToDoDbContext context, Item item) =>
{
    var entity = context.Items.Update(item);
    await context.SaveChangesAsync();
    return entity.Entity;
});
app.MapDelete("/items", async (ToDoDbContext context, int id) =>
{
    context.Items.Remove(context.Items.FirstOrDefault(item => item.Id == id));
    await context.SaveChangesAsync();
});
app.Run();
