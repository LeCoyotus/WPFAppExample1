namespace ControlWpf
{
	public class GestionAnimalDbContextFactory
	{
		public GestionAnimalDbContext CreateDbContext()
		{
			return new GestionAnimalDbContext();
		}
	}
}