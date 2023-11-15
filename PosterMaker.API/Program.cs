using Microsoft.EntityFrameworkCore;
using PosterMaker.API.Data;
using PosterMaker.API.Models;

var builder = WebApplication.CreateBuilder(args);



string ConnectionString = @"Server=AEADLT19726;Initial Catalog=Poster_Db;MultipleActiveResultSets=true;User ID=sa;Password=Maqta@7788;TrustServerCertificate=True";
builder
    .Services
    .AddDbContextFactory<AppDbContext>(opt => opt.UseSqlServer(ConnectionString));


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

app.MapGet("/test", () =>  "Test");

#region "Category"

app.MapGet("/api/category-list", async (AppDbContext db) =>
{
    try
    {
        return await db.Categories
        .AsNoTracking()
        .ToListAsync();
    }
    catch (Exception)
    {
        throw;
    }

})
.WithName("GetCategories");

app.MapPost("/api/create-category", async (Category category, AppDbContext db) =>
{
    await db.Categories.AddAsync(category);
    db.SaveChanges();
    return Results.Created($"/Category/{category.Id}", category);

})
.WithName("CreateCategory");

app.MapGet("/api/Category/{id}", async (int id, AppDbContext db) =>
{
    return await db.Categories.FirstOrDefaultAsync(x => x.Id == id);
})
.WithName("GetCategorysById");

app.MapPut("/api/UpdateCategory/{id}", async (int id, Category category, AppDbContext db) =>
{
    var categoryObj = await db.Categories.FirstOrDefaultAsync(x => x.Id == id);
    categoryObj.Name = category.Name;
    await db.SaveChangesAsync();
    return categoryObj;
})
.WithName("UpdateCategory");
#endregion

#region "Item"


app.MapGet("/api/item-list", async (AppDbContext db) =>
{
    try
    {
        return await db.Items
        .AsNoTracking()
        .ToListAsync();
    }
    catch (Exception)
    {
        throw;
    }

})
.WithName("GetItems");



app.MapGet("/api/Item/{id}", async (int id, AppDbContext db) =>
{
    return await db.Items.FirstOrDefaultAsync(x => x.Id == id);
})
.WithName("GetItemById");


app.MapPost("/api/create-item", async (Item item, AppDbContext db) =>
{
    await db.Items.AddAsync(item);
    db.SaveChanges();
    return Results.Created($"/Item/{item.Id}", item);

})
.WithName("CreateItem");

app.MapPut("/api/UpdateItem/{id}", async (int id, Item item, AppDbContext db) =>
{
    var itemObj = await db.Items.FirstOrDefaultAsync(x => x.Id == id);
    itemObj.ItemName = item.ItemName;
    await db.SaveChangesAsync();
    return itemObj;
})
.WithName("UpdateItem");
#endregion



app.Run();

