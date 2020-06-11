using System.Collections.Generic;

namespace ControlWpf
{
	public class Species : DomainObject
	{
		public string Name { get; set; }
		public int NbMaxAuthorizedKills { get; set; }
		public ICollection<Animal> Animals { get; set; }
	}
}