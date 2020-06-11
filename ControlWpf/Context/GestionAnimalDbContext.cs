using System.Data.Entity;

namespace ControlWpf
{
	public class GestionAnimalDbContext : DbContext
	{
		public DbSet<Species> Species { get; set; }
		public DbSet<Animal> Animals { get; set; }

	}
}