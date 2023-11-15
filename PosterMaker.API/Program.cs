using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PosterMaker.API.Data;
using PosterMaker.API.Models;

var builder = WebApplication.CreateBuilder(args);



string ConnectionString = @"Server=AEADLT19726;Initial Catalog=posterdb;MultipleActiveResultSets=true;User ID=sa;Password=Maqta@7788;TrustServerCertificate=True";
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

#region "App"
app.MapGet("/api/app-list", async (AppDbContext db) =>
{
    try
    {
        return await db.Apps
        .AsNoTracking()
        .ToListAsync();
    }
    catch (Exception)
    {
        throw;
    }

})
.WithName("GetApps");



app.MapGet("/api/app/{id}", async (int id, AppDbContext db) =>
{
    return await db.Apps.FirstOrDefaultAsync(x => x.Id == id);
})
.WithName("GetAppById");


app.MapPost("/api/create-app", async ([FromBody] App app, AppDbContext db) =>
{
    await db.Apps.AddAsync(app);
    db.SaveChanges();
    return Results.Created($"/Item/{app.Id}", app);

})
.WithName("CreateApp");

app.MapPut("/api/update-app/{id}", async (int id, [FromBody] App app, AppDbContext db) =>
{
    var appObj = await db.Apps.FirstOrDefaultAsync(x => x.Id == id);
    appObj.Name = app.Name;
    await db.SaveChangesAsync();
    return appObj;
})
.WithName("UpdateApp");


#endregion


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

app.MapPost("/api/create-category", async ([FromBody] Category category, AppDbContext db) =>
{
    await db.Categories.AddAsync(category);
    db.SaveChanges();
    return Results.Created($"/Category/{category.Id}", category);

})
.WithName("CreateCategory");

app.MapGet("/api/category/{id}", async (int id, AppDbContext db) =>
{
    return await db.Categories.FirstOrDefaultAsync(x => x.Id == id);
})
.WithName("GetCategorysById");

app.MapPut("/api/update-category/{id}", async (int id, [FromBody] Category category, AppDbContext db) =>
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

app.MapGet("/api/item/{id}", async (int id, AppDbContext db) =>
{
    return await db.Items.FirstOrDefaultAsync(x => x.Id == id);
})
.WithName("GetItemById");


app.MapPost("/api/create-item", async ([FromBody] Item item, AppDbContext db) =>
{
    await db.Items.AddAsync(item);
    db.SaveChanges();
    return Results.Created($"/Item/{item.Id}", item);

})
.WithName("CreateItem");

app.MapPut("/api/update-Item/{id}", async (int id, [FromBody] Item item, AppDbContext db) =>
{
    var itemObj = await db.Items.FirstOrDefaultAsync(x => x.Id == id);
    itemObj.ItemName = item.ItemName;
    await db.SaveChangesAsync();
    return itemObj;
})
.WithName("UpdateItem");
#endregion


#region Others


#endregion

app.MapGet("/api/app-categories/{id}", async (int appId, AppDbContext db) =>
{
    var appCategories = await db.Categories.Where(x => x.AppId == appId).ToListAsync();
    return appCategories;
})
.WithName("GetCategoriesByAppId");

app.MapGet("/api/category-items/{id}", async (int categoryId, AppDbContext db) =>
{
    var categoryItems = await db.Items.Where(x => x.CategoryId == categoryId).ToListAsync();
    return categoryItems;
})
.WithName("GetItemsByCategoryId");

app.Run();

