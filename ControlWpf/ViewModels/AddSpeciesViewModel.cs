using System.Windows;
using ControlWpf.Services;

namespace ControlWpf.ViewModels
{
	public class AddSpeciesViewModel
	{
		private readonly SpeciesDataService _speciesService = new SpeciesDataService(new GestionAnimalDbContextFactory());
		public string SpeciesName { get; set; }
		public string AuthorizedKills { get; set; }
		public RelayCommand CancelCommand { get; set; }
		public RelayCommand OkCommand { get; set; }

		private readonly Species _species;
		private SpeciesViewModel _speciesViewModel;
		public AddSpeciesViewModel(SpeciesViewModel speciesViewModel)
		{
			_speciesViewModel = speciesViewModel;
			_species = new Species();

			OkCommand = new RelayCommand(AddSpecies, CanAddSpecies);
			CancelCommand = new RelayCommand(Cancel);
		}

		public async void AddSpecies(object parameter)
		{
			Window addSpeciesWindow = (Window) parameter;
			int.TryParse(AuthorizedKills, out int nbKill);

			_species.Name = SpeciesName;
			_species.NbMaxAuthorizedKills = nbKill;

			await _speciesService.CreateAsync(_species);

			_speciesViewModel.SpeciesModel.Add(new SpeciesModel
			{
				Id = _species.Id,
				Name = _species.Name,
				AnimalCount = 0,
				AuthorizedKills = _species.NbMaxAuthorizedKills
			});

			addSpeciesWindow.Close();
		}

		public void Cancel(object parameter)
		{
			Window addSpeciesWindow = (Window) parameter;
			addSpeciesWindow.Close();
		}
		public bool CanAddSpecies(object parameter)
		{
			bool isOk = int.TryParse(AuthorizedKills, out int nbKill);
			return SpeciesName != null && isOk;
		}
	}
}