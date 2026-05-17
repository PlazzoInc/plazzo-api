using Microsoft.EntityFrameworkCore;
public static class UserRoutes
{
    public static void MapUserRoutes(this WebApplication app)
    {
        app.MapGet("/users", async (PlazzoContext db) =>
        {
            return await db.Users.ToListAsync();
        });

        app.MapGet("/users/{id}", async (int id, PlazzoContext db) =>
        {
            return await db.Users.FindAsync(id)
                is Users user
                    ? Results.Ok(user)
                    : Results.NotFound();
        });

        app.MapPost("/users", async (Users user, PlazzoContext db) =>
        {
            db.Users.Add(user);
            await db.SaveChangesAsync();

            return Results.Created($"/users/{user.Id}", user);
        });

        app.MapPut("/users/{id}", async (int id, Users inputUser, PlazzoContext db) =>
        {
            var user = await db.Users.FindAsync(id);

            if (user is null) return Results.NotFound();

            user.Agence_Id = inputUser.Agence_Id;
            user.Role = inputUser.Role;
            user.Name = inputUser.Name;
            user.Surname = inputUser.Surname;
            user.Email = inputUser.Email;
            user.Password = inputUser.Password;
            user.Phone = inputUser.Phone;
            user.updated_at = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            await db.SaveChangesAsync();

            return Results.NoContent();
        });

        app.MapDelete("/users/{id}", async (int id, PlazzoContext db) =>
        {
            if (await db.Users.FindAsync(id) is Users user)
            {
                db.Users.Remove(user);
                await db.SaveChangesAsync();
                return Results.Ok(user);
            }

            return Results.NotFound();
        });
    }
}