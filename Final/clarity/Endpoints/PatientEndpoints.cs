using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using ClarityAPI.Data;
using Clarity.Shared.Models;
using Clarity.Validation;
using ClarityAPI.Lightspark;
using Microsoft.AspNetCore.SignalR;

namespace ClarityAPI.Endpoints
{
    public static class PatientEndpoints
    {
        public static void MapPatientEndpoints(this WebApplication app)
        {

            app.MapPost("/data/patients", async (ClarityData context, Patient record) =>
            {
                var pass = Garmr.NeoGuard(record);
                if (pass is not Accepted) return pass;
                record.Id = Garmr.NeoId(record);
                record.Admission = DateTime.Now;

                context.Patient.Add(record);
                await context.SaveChangesAsync();

                return Results.Created($"Patient record created successfully.",
                    new { url = $"/data/patients/{record.Id}", record.Id });
            });


            app.MapPatch("/data/patients/{id}", async (ClarityData context, string id, Patient record, IHubContext<Atom> hub) =>
            {
                var current = await context.Patient.FindAsync(id);
                if (current == null)
                    return Results.NotFound(new { Message = "Patient not found." });

                current.Name = record.Name ?? current.Name;
                current.Room = record.Room ?? current.Room;
                current.Wing = record.Wing ?? current.Wing;
                current.Measurements = record.Measurements ?? current.Measurements;
                
                await context.SaveChangesAsync();
                await hub.Clients.All.SendAsync("PatientGateway", current);
                return Results.Ok(new { Message = "Patient record updated successfully." });
            });

            app.MapGet("/data/patients", async (ClarityData context, int? page, int? size) =>
            {
                var records = await context.Patient
                    .AsNoTracking()
                    .OrderBy(s => s.Name)
                    .Skip((page.GetValueOrDefault(1) - 1) * size.GetValueOrDefault(10))
                    .Take(size.GetValueOrDefault(10))
                    .ToListAsync();
                
                return Results.Ok(records);
            });

            app.MapGet("/data/patients/{id}", async (ClarityData context, string id) =>
            {
                var record = await context.Patient
                    .AsNoTracking()
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (record == null)
                    return Results.NotFound(new { Message = "Patient record not found." });

                return Results.Ok(record);
            });

            app.MapDelete("/data/patients/{id}", async (ClarityData context, string id) =>
            {
                var record = await context.Patient.FindAsync(id);
                if (record == null)
                    return Results.NotFound(new { Message = "Sensor record not found." });

                context.Patient.Remove(record);
                await context.SaveChangesAsync();
                return Results.NoContent();
            });
        }
    }
}
