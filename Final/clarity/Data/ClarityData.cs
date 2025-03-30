using Clarity.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace ClarityAPI.Data
{
    public class ClarityData(DbContextOptions<ClarityData> options) : DbContext(options)
    {
        public DbSet<Patient> Patient { get; set; }
    }
}
