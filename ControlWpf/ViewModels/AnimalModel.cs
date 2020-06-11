namespace ControlWpf.ViewModels
{
	public class AnimalModel
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public int IdentificationNumber { get; set; }
		public int SpeciesId { get; set; }
		public string SpeciesName { get; set; }

		public string Race { get; set; }

		public Sexe AnimalSex { get; set; }
		public int Age { get; set; }

		public string Location { get; set; }
	}
}