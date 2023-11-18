using Azure.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PosterMaker.API.Data;
using PosterMaker.API.Models;
using PosterMaker.API.ViewModel;
using System.IO.Compression;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);



string ConnectionString = @"Server=AEADLT19726;Initial Catalog=poster_db;MultipleActiveResultSets=true;User ID=sa;Password=Maqta@7788;TrustServerCertificate=True";
builder
    .Services
    .AddDbContextFactory<AppDbContext>(opt => opt.UseSqlServer(ConnectionString));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});

// CORS
//services cors
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/test", () => "Test");

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

    // New Addition
    //using (var memoryStream = new MemoryStream())
    //{
    //    await file.CopyToAsync(memoryStream);

    //    // Upload the file if less than 2 MB
    //    if (memoryStream.Length < 2097152)
    //    {
    //        //create a AppFile mdoel and save the image into database.
    //        var appObj = new App()
    //        {
    //            Name = file.FileName,
    //            Content = memoryStream.ToArray()
    //        };

    //        await db.Apps.AddAsync(appObj);
    //        db.SaveChanges();
    //    }
    //    else
    //    {
    //        return Results.Ok("Invalid Image");
    //    }
    //}

    //// Return image file

    //var returndata = db.Apps
    //    .Where(c => c.Name == file.FileName)
    //    .Select(c => new ReturnData()
    //    {
    //        Name = c.Name,
    //        ImageBase64 = String.Format("data:image/png;base64,{0}", Convert.ToBase64String(c.Content))
    //    }).FirstOrDefault();
    //return Results.Ok(returndata);
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
        .Select(x => new {
            x.App,
            x.Id,
            x.Name,
            x.AppId,
            x.CreatedAt,
            x.ThumbnailPath,
            x.IsDeleted            
        })
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

    return Results.Created($"/Item/{category.Id}", category);

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

app.MapGet("/api/app-categories/{id}", async (int id, AppDbContext db) =>
{
    var appCategories = await db.Categories
    .AsNoTracking()
    .Where(x => x.AppId == id)
    .Select(x => new {        
        x.App,
        x.Id,
        x.Name,
        x.CreatedAt,
        x.ThumbnailPath
    })
    .ToListAsync();

    return appCategories;
})
.WithName("GetCategoriesByAppId");

app.MapGet("/api/category-items/{id}", async (int id, AppDbContext db) =>
{
    var categoryItems = await db.Items
    .AsNoTracking()    
    .Where(x => x.CategoryId == id)
    .Select(x => new { 
        x.Category.Name,
        x.ItemName,
        x.ThumbnailPath,
        x.JsonPath,
        x.Id
    })
    .ToListAsync();

    return categoryItems;
})
.WithName("GetItemsByCategoryId");

#endregion

#region Login
app.MapPost("/api/login",async([FromBody] User user, AppDbContext db) =>
{
    var userObj = await db.Users
                  .FirstOrDefaultAsync(x => x.Username == user.Username && x.Password == user.Password);
    
    if (userObj is not null)
    {
        return Results.Ok(userObj);
    }
    return Results.NotFound();
})
.WithName("login");

#endregion

app.UseCors("corsapp");

app.Run();

