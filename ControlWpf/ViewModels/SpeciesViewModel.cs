using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ControlWpf.Services;

namespace ControlWpf.ViewModels
{
	public class SpeciesViewModel : ObservableObject
	{
		private readonly SpeciesDataService _speciesService = new SpeciesDataService(new GestionAnimalDbContextFactory());

		private ObservableCollection<SpeciesModel> _speciesModel;

		public ObservableCollection<SpeciesModel> SpeciesModel
		{
			get => _speciesModel;
			set
			{
				_speciesModel = value;
				OnPropertyChanged();
			}
		}
		public RelayCommand AddSpeciesCommand { get; set; }
		public RelayCommand DisplayAll { get; set; }

		public SpeciesViewModel()
		{
			InitializeAsync();
			AddSpeciesCommand = new RelayCommand(AddSpecies);
			DisplayAll = new RelayCommand(DisplayAllAnimalsBySpecies);
		}

		private async void InitializeAsync()
		{
			SpeciesModel = new ObservableCollection<SpeciesModel>(await _speciesService.GetAllSpeciesDisplayableAsync());
		}

		public void AddSpecies(object parameter)
		{
			AddSpecies addSpeciesWindow = new AddSpecies(this);
			addSpeciesWindow.Show();
		}

		public void DisplayAllAnimalsBySpecies(object parameter)
		{
			int idSpecies = (int) parameter;
			AnimalsBySpecies animalBySpeciesWindow = new AnimalsBySpecies(idSpecies);
			animalBySpeciesWindow.Show();
		}
	}
}