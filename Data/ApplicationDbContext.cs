// Kod tillhörande lösningen till projektuppgiften i kurs DT191G, skapat av Cecilia Edvardsson

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Hairsalon.Models;

namespace Hairsalon.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

public DbSet<Hairsalon.Models.BookingModel> BookingModel { get; set; } = default!;

public DbSet<Hairsalon.Models.TreatmentModel> TreatmentModel { get; set; } = default!;

public DbSet<Hairsalon.Models.StylistModel> StylistModel { get; set; } = default!;
}
