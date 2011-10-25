using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace JiraQuickEntry
{
	/// <summary>
	/// Interaction logic for Login.xaml
	/// </summary>
	public partial class Login : Window
	{
		public Login()
		{
			InitializeComponent();
		}

		public string Token { get; set; }
		public string User { get; set; }

		private void button1_Click(object sender, RoutedEventArgs e)
		{
			var c = new JiraSoapServiceClient("jirasoapservice-v2");
			try
			{
				Token = c.login(Username.Text, Password.Text);
				User = Username.Text;
				DialogResult = true;
				Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
	}
}
