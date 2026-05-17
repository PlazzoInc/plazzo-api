using Microsoft.EntityFrameworkCore;
public static class AgencyRoutes
{
    public static void MapAgencyRoutes(this WebApplication app)
    {
        app.MapGet("/agencies", async (PlazzoContext db) =>
        {
            return await db.Agencies.ToListAsync();
        }).WithName("GetAgencies");

        app.MapGet("/agencies/{id}", async (int id, PlazzoContext db) =>
        {
            return await db.Agencies.FindAsync(id);
        }).WithName("GetAgencyById");

        app.MapPost("/agencies", async (Agencies agency, PlazzoContext db) =>
        {
            db.Agencies.Add(agency);
            await db.SaveChangesAsync();
            return Results.Created($"/agencies/{agency.Id}", agency);
        }).WithName("CreateAgency");

        app.MapPut("/agencies/{id}", async (int id, Agencies updatedAgency, PlazzoContext db) =>
        {
            var agency = await db.Agencies.FindAsync(id);
            if (agency is null) return Results.NotFound();
            agency.Name = updatedAgency.Name;
            agency.Address = updatedAgency.Address;
            agency.City = updatedAgency.City;
            agency.Postal_Code = updatedAgency.Postal_Code;
            agency.Phone = updatedAgency.Phone;
            await db.SaveChangesAsync();
            return Results.NoContent();
        }).WithName("UpdateAgency");

        app.MapDelete("/agencies/{id}", async (int id, PlazzoContext db) =>
        {
            var agency = await db.Agencies.FindAsync(id);
            if (agency is null) return Results.NotFound();
            db.Agencies.Remove(agency);
            await db.SaveChangesAsync();
            return Results.NoContent();
        }).WithName("DeleteAgency");
    }
}