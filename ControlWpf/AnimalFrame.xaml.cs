using System.Windows;
using System.Windows.Controls;
using ControlWpf.ViewModels;

namespace ControlWpf
{
	/// <summary>
	/// Logique d'interaction pour AnimalFrame.xaml
	/// </summary>
	public partial class AnimalFrame : Page
	{
		public AnimalFrame()
		{
			InitializeComponent();

			DataContext = new AnimalViewModel();
		}
	}
}
