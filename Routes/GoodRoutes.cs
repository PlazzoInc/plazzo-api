using Microsoft.EntityFrameworkCore;
public static class GoodRoutes
{
    public static void MapGoodRoutes(this WebApplication app)
    {
        app.MapGet("/goods", async ( PlazzoContext db) =>
        {
            return await db.Goods.ToListAsync();
        });

        app.MapGet("/goods/{id}", async (int id, PlazzoContext db) =>
        {
            return await db.Goods.FindAsync(id)
                is Goods good
                    ? Results.Ok(good)
                    : Results.NotFound();
        });

        app.MapPost("/goods", async (Goods good,PlazzoContext db) =>
        {
            db.Goods.Add(good);
            await db.SaveChangesAsync();

            return Results.Created($"/goods/{good.Id}", good);
        });

        app.MapPut("/goods/{id}", async (int id, Goods inputGood, PlazzoContext db) =>
        {
            var good = await db.Goods.FindAsync(id);

            if (good is null) return Results.NotFound();

            good.Agency_Id = inputGood.Agency_Id;
            good.User_Id = inputGood.User_Id;
            good.Type = inputGood.Type;
            good.Status = inputGood.Status;
            good.Deed = inputGood.Deed;
            good.Description = inputGood.Description;
            good.Price = inputGood.Price;
            good.Surface = inputGood.Surface;
            good.Rooms = inputGood.Rooms;
            good.Bathrooms = inputGood.Bathrooms;
            good.Bedrooms = inputGood.Bedrooms;
            good.Floor = inputGood.Floor;
            good.Construction_Date = inputGood.Construction_Date;
            good.DPE = inputGood.DPE;

            await db.SaveChangesAsync();

            return Results.NoContent();
        });

        app.MapDelete("/goods/{id}", async (int id, PlazzoContext db) =>
        {
            if (await db.Goods.FindAsync(id) is Goods good)
            {
                db.Goods.Remove(good);
                await db.SaveChangesAsync();
                return Results.Ok(good);
            }

            return Results.NotFound();
        });
    }
}