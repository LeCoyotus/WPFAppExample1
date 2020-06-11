using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using ControlWpf.Services;
using ControlWpf.ViewModels;

namespace ControlWpf
{
	/// <summary>
	/// Logique d'interaction pour AddAnimal.xaml
	/// </summary>
	public partial class AddAnimal : Window
	{
		public AddAnimal(AnimalViewModel animalViewModel)
		{
			InitializeComponent();

			DataContext = new AddAnimalViewModel(animalViewModel);
		}
	}
}
