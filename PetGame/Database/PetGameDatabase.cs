using Microsoft.EntityFrameworkCore;
using PetGame.Database.Tables;

namespace PetGame.Database;

public sealed class PetGameDatabase: DbContext
{
    public DbSet<Pet> Pets => Set<Pet>();
    public DbSet<Player> Players => Set<Player>();

    public PetGameDatabase(DbContextOptions<PetGameDatabase> options) : base(options)
    {

    }
}