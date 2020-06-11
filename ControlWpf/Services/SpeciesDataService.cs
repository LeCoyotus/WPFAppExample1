using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using ControlWpf.ViewModels;

namespace ControlWpf.Services
{
	public class SpeciesDataService
	{
		private readonly GestionAnimalDbContextFactory _contextFactory;

		public SpeciesDataService(GestionAnimalDbContextFactory contextFactory)
		{
			_contextFactory = contextFactory;
		}
		public async Task<Species> CreateAsync(Species entity)
		{
			using (GestionAnimalDbContext context = _contextFactory.CreateDbContext())
			{
				Species createdEntity = context.Species.Add(entity);
				await context.SaveChangesAsync();

				return createdEntity;
			}
		}

		public async Task<Dictionary<int, string>> GetSpeciesDictionaryAsync()
		{
			using (GestionAnimalDbContext context = _contextFactory.CreateDbContext())
			{
				return await context.Species.ToDictionaryAsync(x => x.Id, x => x.Name);
			}
		}

		public async Task<IEnumerable<SpeciesModel>> GetAllSpeciesDisplayableAsync()
		{
			using (GestionAnimalDbContext context = _contextFactory.CreateDbContext())
			{
				IEnumerable<SpeciesModel> entities = await context.Species.Select(species => new SpeciesModel
				{
					Id = species.Id,
					Name = species.Name,
					AnimalCount = species.Animals.Count,
					AuthorizedKills = species.NbMaxAuthorizedKills
				}) .ToListAsync();

				return entities;
			}
		}
		public async Task<bool> DeleteAsync(int id)
		{
			using (GestionAnimalDbContext context = _contextFactory.CreateDbContext())
			{
				Species entity = await context.Set<Species>().FirstOrDefaultAsync(x => x.Id == id);
				if (entity != null)
				{
					context.Species.Remove(entity);
				}
				await context.SaveChangesAsync();

				return true;
			}
		}
		public async Task<Species> UpdateAsync(int id, Species entity)
		{
			using (GestionAnimalDbContext context = _contextFactory.CreateDbContext())
			{
				entity.Id = id;
				context.Species.AddOrUpdate(entity);
				await context.SaveChangesAsync();

				return entity;
			}
		}
	}
}