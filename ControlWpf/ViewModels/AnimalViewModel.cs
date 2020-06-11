using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Windows;
using ControlWpf.Services;

namespace ControlWpf.ViewModels
{
	public class AnimalViewModel : ObservableObject
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

		private Animal _selectedAnimal;
		public Animal SelectedAnimal
		{
			get => _selectedAnimal;
			set
			{
				_selectedAnimal = value;
				OnPropertyChanged();
			}
		}

		private IEnumerable<string> _location;
		public IEnumerable<string> Location
		{
			get => _location;
			set
			{
				_location = value;
				OnPropertyChanged();
			}
		}

		private string _selectedLocation;

		public string SelectedLocation
		{
			get => _selectedLocation;
			set
			{
				_selectedLocation = value;
				OnPropertyChanged();
				ApplyFiltersAsync();
			}
		}
		private IEnumerable<string> _race;
		public IEnumerable<string> Race
		{
			get => _race;
			set
			{
				_race = value;
				OnPropertyChanged();
			}
		}

		private string _selectedRace;

		public string SelectedRace
		{
			get => _selectedRace;
			set
			{
				_selectedRace = value;
				OnPropertyChanged();
				ApplyFiltersAsync();
			}
		}

		public IEnumerable<Sexe> AnimalSex => Enum.GetValues(typeof(Sexe)).Cast<Sexe>();

		private Sexe _selectedSexe;

		public Sexe SelectedSexe
		{
			get => _selectedSexe;
			set
			{
				_selectedSexe = value;
				OnPropertyChanged();
				ApplyFiltersAsync();
			}
		}
		public RelayCommand AddAnimalCommand { get; set; }
		public RelayCommand ResetSelectedLocation { get; set; }
		public RelayCommand ResetSelectedRace { get; set; }
		public RelayCommand ResetSelectedSexe { get; set; }
		public AnimalViewModel()
		{
			InitializeAsync();
			AddAnimalCommand = new RelayCommand(AddAnimal);
			ResetSelectedLocation = new RelayCommand(ResetLocation);
			ResetSelectedRace = new RelayCommand(ResetRace);
			ResetSelectedSexe = new RelayCommand(ResetSexe);
		}

		private async void InitializeAsync()
		{
			AnimalsModel = new ObservableCollection<AnimalModel>(await _animalService.GetAllAnimalsDisplayableAsync());
			Location = await _animalService.GetAllLocationAsync();
			Race = await _animalService.GetAllRaceAsync();
		}
		public void AddAnimal(object parameter)
		{
			AddAnimal addAnimalWindow = new AddAnimal(this);
			addAnimalWindow.Show();
		}

		public void ResetLocation(object parameter)
		{
			SelectedLocation = null;
		}
		public void ResetRace(object parameter)
		{
			SelectedRace = null;
		}
		public void ResetSexe(object parameter)
		{
			SelectedSexe = Sexe.None;
		}

		public async void ApplyFiltersAsync()
		{
			var filters = new List<Expression<Func<Animal, bool>>>();

			if (SelectedLocation != null)
			{
				filters.Add(x => x.Location == SelectedLocation);
			}
			if (SelectedRace != null)
			{
				filters.Add(x => x.Race == SelectedRace);
			}

			if (SelectedSexe != Sexe.None)
			{
				filters.Add(x => x.AnimalSex == SelectedSexe);
			}

			AnimalsModel = new ObservableCollection<AnimalModel>(await _animalService.GetAllAnimalFilteredAsync(filters));
		}
	}
}