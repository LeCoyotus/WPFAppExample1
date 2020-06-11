using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ControlWpf.ViewModels;

namespace ControlWpf.Services
{
	public class AnimalDataService
	{
		private readonly GestionAnimalDbContextFactory _contextFactory;

		public AnimalDataService(GestionAnimalDbContextFactory contextFactory)
		{
			_contextFactory = contextFactory;
		}

		public async Task<Animal> CreateAsync(Animal entity)
		{
			using (GestionAnimalDbContext context = _contextFactory.CreateDbContext())
			{
				Animal createdEntity = context.Animals.Add(entity);
				await context.SaveChangesAsync();

				return createdEntity;
			}
		}

		public async Task<IEnumerable<AnimalModel>> GetAllAnimalsDisplayableAsync()
		{
			using (GestionAnimalDbContext context = _contextFactory.CreateDbContext())
			{
				IEnumerable<AnimalModel> entities = await context.Animals.Select(animal => new AnimalModel
				{
					Id = animal.Id,
					Name = animal.Name,
					Age = animal.Age,
					AnimalSex = animal.AnimalSex,
					IdentificationNumber = animal.IdentificationNumber,
					Location = animal.Location,
					Race = animal.Race,
					SpeciesId = animal.SpeciesId,
					SpeciesName = animal.Species.Name
				}).ToListAsync();

				return entities;
			}
		}

		public async Task<IEnumerable<AnimalModel>> GetAllAnimalsBySpeciesAsync(int idspecies)
		{
			using (GestionAnimalDbContext context = _contextFactory.CreateDbContext())
			{
				IEnumerable<AnimalModel> entities = await context.Animals
					.Where(x => x.SpeciesId == idspecies)
					.Select(animal => new AnimalModel
				{
					Id = animal.Id,
					Name = animal.Name,
					Age = animal.Age,
					AnimalSex = animal.AnimalSex,
					IdentificationNumber = animal.IdentificationNumber,
					Location = animal.Location,
					Race = animal.Race,
					SpeciesId = animal.SpeciesId,
					SpeciesName = animal.Species.Name
				}).ToListAsync();

				return entities;
			}
		}

		public async Task<IEnumerable<string>> GetAllLocationAsync()
		{
			using (GestionAnimalDbContext context = _contextFactory.CreateDbContext())
			{
				return await context.Animals.Select(x => x.Location).Distinct().ToListAsync();
			}
		}

		public async Task<IEnumerable<string>> GetAllRaceAsync()
		{
			using (GestionAnimalDbContext context = _contextFactory.CreateDbContext())
			{
				return await context.Animals.Select(x => x.Race).Distinct().ToListAsync();
			}
		}

		public async Task<IEnumerable<AnimalModel>> GetAllAnimalFilteredAsync(List<Expression<Func<Animal, bool>>> filters)
		{
			using (GestionAnimalDbContext context = _contextFactory.CreateDbContext())
			{
				var query = context.Animals.AsQueryable();

				foreach (var filter in filters)
				{
					query = query.Where(filter);
				}

				return await query
					.Select(animal => new AnimalModel
					{
						Id = animal.Id,
						Name = animal.Name,
						IdentificationNumber = animal.IdentificationNumber,
						Age = animal.Age,
						AnimalSex = animal.AnimalSex,
						SpeciesId = animal.SpeciesId,
						SpeciesName = animal.Species.Name,
						Race = animal.Race,
						Location = animal.Location
					}).ToListAsync();
			}
		}

		public async Task<bool> DeleteAsync(int id)
		{
			using (GestionAnimalDbContext context = _contextFactory.CreateDbContext())
			{
				Animal entity = await context.Set<Animal>().FirstOrDefaultAsync(x => x.Id == id);
				if (entity != null)
				{
					context.Animals.Remove(entity);
				}
				await context.SaveChangesAsync();

				return true;
			}
		}
		public async Task<Animal> UpdateAsync(int id, Animal entity)
		{
			using (GestionAnimalDbContext context = _contextFactory.CreateDbContext())
			{
				entity.Id = id;
				context.Animals.AddOrUpdate(entity);
				await context.SaveChangesAsync();

				return entity;
			}
		}
	}
}