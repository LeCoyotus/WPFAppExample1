using System.Windows;
using ControlWpf.Services;
using ControlWpf.ViewModels;

namespace ControlWpf
{
	/// <summary>
	/// Logique d'interaction pour AddSpecies.xaml
	/// </summary>
	public partial class AddSpecies : Window
	{
		public AddSpecies(SpeciesViewModel speciesViewModel)
		{
			InitializeComponent();

			DataContext = new AddSpeciesViewModel(speciesViewModel);
		}
	}
}
