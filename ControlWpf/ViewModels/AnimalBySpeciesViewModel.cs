using System.Collections.ObjectModel;
using ControlWpf.Services;

namespace ControlWpf.ViewModels
{
	public class AnimalBySpeciesViewModel : ObservableObject
	{
		private readonly AnimalDataService _animalService = new AnimalDataService(new GestionAnimalDbContextFactory());

		private ObservableCollection<AnimalModel> _animalsModel;
		public ObservableCollection<AnimalModel> AnimalsModel
		{
			get => _animalsModel;
			set
			{
				_animalsModel = value;
				OnPropertyChanged();
			}
		}

		public AnimalBySpeciesViewModel(int idspecies)
		{
			InitializeAsync(idspecies);
		}

		public async void InitializeAsync(int idspecies)
		{
			AnimalsModel = new ObservableCollection<AnimalModel>(await _animalService.GetAllAnimalsBySpeciesAsync(idspecies));
		}
	}
}