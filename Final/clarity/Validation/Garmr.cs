using Microsoft.AspNetCore.WebUtilities;
using Clarity.Shared.Models;

namespace Clarity.Validation
{
    public static class Garmr
    {
        public static IResult NeoGuard(Patient record)
        {
            // Validate age
            if (record.Age == 0)
                return Results.BadRequest(new { Message = "Age cannot be zero and is required." });

            if (record.Age < 0)
                return Results.BadRequest(new { Message = "Age cannot be a negative integer." });

            if (record.Age > 122)
                return Results.BadRequest(new { Message = "Age cannot exceed upper bound (122)." });

            // Validate gender
            if (record.Gender == '\0')
                return Results.BadRequest(new { Message = "Gender is required." });

            if (!"MF".Contains(char.ToUpper(record.Gender)))
                return Results.BadRequest(new { Message = "Gender must be either M or F." });

            // Validate severity

            if (record.Severity == 0)
                return Results.BadRequest(new { Message = "Severity cannot be zero and is required." });

            if (record.Severity < 0)
                return Results.BadRequest(new { Message = "Severity cannot be a negative integer." });

            if (record.Severity > 5)
                return Results.BadRequest(new { Message = "Severity must be a value between 1 and 5." });

            return Results.Accepted();
        }


        public static string NeoId(Patient record)
        {
            var vUId = Base64UrlTextEncoder.Encode(Guid.NewGuid().ToByteArray())[..8].ToUpper();

            return $"P{record.Age:D2}{(Gender)record.Gender}-{(Severity)record.Severity}-{vUId}";
        }
    }
}