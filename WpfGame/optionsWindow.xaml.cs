using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfGame
{
	/// <summary>
	/// Логика взаимодействия для optionsWindow.xaml
	/// </summary>
	public partial class optionsWindow : Window
	{
		Options _options;
		public optionsWindow(Options opts)
		{
			InitializeComponent();
			
			_options = opts;
		}
		private void OkButton_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = true;
			if(Easy_RButton.IsChecked == true)
			{
				_options.countOfMines = 2;
				_options.weight = 10;
			}
			if (Normal_RButton.IsChecked == true)
			{
				_options.countOfMines = 4;
				_options.weight = 15;
			}
			if (Hard_RButton.IsChecked == true)
			{
				_options.countOfMines = 5;
				_options.weight = 20;
			}
			Close();
		}
	}

}
