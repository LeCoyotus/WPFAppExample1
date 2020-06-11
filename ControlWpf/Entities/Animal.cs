namespace ControlWpf
{
	public enum Sexe{
		None,
		Male,
		Female
	}
	public class Animal : DomainObject
	{
		public string Name { get; set; }

		public int IdentificationNumber { get; set; }
		public int SpeciesId { get; set; }
		public Species Species { get; set; }

		public string Race { get; set; }

		public Sexe AnimalSex { get; set; }
		public int Age { get; set; }

		public string Location { get; set; }
	}
}