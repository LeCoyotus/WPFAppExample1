using System.Windows;
using System.Windows.Controls;
using ControlWpf.ViewModels;

namespace ControlWpf
{
	/// <summary>
	/// Logique d'interaction pour SpecieFrame.xaml
	/// </summary>
	public partial class SpecieFrame : Page
	{
		public SpecieFrame()
		{
			InitializeComponent();

			DataContext = new SpeciesViewModel();
		}
	}
}
