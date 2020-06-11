using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using ControlWpf.Services;

namespace ControlWpf.ViewModels
{
	public class AddAnimalViewModel : ObservableObject
	{
		private readonly AnimalDataService _animalService = new AnimalDataService(new GestionAnimalDbContextFactory());
		private readonly SpeciesDataService _speciesService = new SpeciesDataService(new GestionAnimalDbContextFactory());

		private AnimalViewModel _animalViewModel;

		private Dictionary<int, string> _speciesDictionary;
		public Dictionary<int, string> SpeciesDictionary
		{
			get => _speciesDictionary;
			set
			{
				_speciesDictionary = value;
				OnPropertyChanged();
			}
		}

		private KeyValuePair<int, string> _currentSpeciesSelection;

		public KeyValuePair<int, string> CurrentSpeciesSelection
		{
			get => _currentSpeciesSelection;
			set
			{
				_currentSpeciesSelection = value;
				OnPropertyChanged();
			}
		}

		public IEnumerable<Sexe> AnimalSex => Enum.GetValues(typeof(Sexe)).Cast<Sexe>();

		private Sexe _sexeSelection;

		public Sexe SexeSelection
		{
			get => _sexeSelection;
			set
			{
				_sexeSelection = value;
				OnPropertyChanged();
			}
		}
		public string Name { get; set; }
		public string IdentificationNumber { get; set; }
		public string Race { get; set; }
		public string Age { get; set; }
		public string Location { get; set; }

		public RelayCommand CancelCommand { get; set; }
		public RelayCommand OkCommand { get; set; }

		private Animal _animal;
		public AddAnimalViewModel(AnimalViewModel animalViewModel)
		{
			InitializeAsync();
			_animalViewModel = animalViewModel;
			_animal = new Animal();
			CancelCommand = new RelayCommand(Cancel);
			OkCommand = new RelayCommand(AddAnimal, CanAddAnimal);
		}

		public async void AddAnimal(object parameter)
		{
			Window addSpeciesWindow = (Window)parameter;

			int.TryParse(IdentificationNumber, out int idNumber);
			int.TryParse(Age, out int age);

			_animal.Name = Name;
			_animal.IdentificationNumber = idNumber;
			_animal.SpeciesId = CurrentSpeciesSelection.Key;
			_animal.AnimalSex = SexeSelection;
			_animal.Race = Race;
			_animal.Age = age;
			_animal.Location = Location;

			await _animalService.CreateAsync(_animal);

			_animalViewModel.AnimalsModel.Add(new AnimalModel
			{
				Id = _animal.Id,
				Name = _animal.Name,
				IdentificationNumber = _animal.IdentificationNumber,
				Age = _animal.Age,
				SpeciesId = _animal.SpeciesId,
				AnimalSex = _animal.AnimalSex,
				Race = _animal.Race,
				Location = _animal.Location,
				SpeciesName = CurrentSpeciesSelection.Value
			});

			addSpeciesWindow.Close();
		}

		public bool CanAddAnimal(object parameter)
		{
			return  CurrentSpeciesSelection.Value != null &&
			        int.TryParse(IdentificationNumber, out int idNumber) &&
			        int.TryParse(Age, out int age) &&
					SexeSelection != Sexe.None;
		}
		public void Cancel(object parameter)
		{
			Window addSpeciesWindow = (Window)parameter;
			addSpeciesWindow.Close();
		}

		public async void InitializeAsync()
		{
			SpeciesDictionary = await _speciesService.GetSpeciesDictionaryAsync();
		}
	}
}