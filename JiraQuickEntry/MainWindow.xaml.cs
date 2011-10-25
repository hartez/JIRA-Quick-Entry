using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace JiraQuickEntry
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private string _token;
		private JiraSoapServiceClient _client;
		private string _username;

		public MainWindow()
		{
			InitializeComponent();

			Loaded += new RoutedEventHandler(MainWindow_Loaded);
			KeyDown += new System.Windows.Input.KeyEventHandler(MainWindow_KeyDown);
		}

		void MainWindow_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
		{
			if(e.Key == Key.Enter && Keyboard.Modifiers == ModifierKeys.Control)
			{
				SubmitIssue();
			}
		}

		void MainWindow_Loaded(object sender, RoutedEventArgs e)
		{
			var login = new Login();
			login.ShowDialog();

			_token = login.Token;
			_username = login.User;
			_client =  new JiraSoapServiceClient("jirasoapservice-v2");
		}

		private void SubmitIssue()
		{
			if (!String.IsNullOrEmpty(Issue.Text) && !String.IsNullOrEmpty(Project.Text))
			{
				try
				{
					RemoteIssue issue = new RemoteIssue();
					issue.project = Project.Text;
					issue.summary = Issue.Text;
					issue.description = Description.Text;

					// Defaulting to New Feature for the moment
					issue.type = "2";

					// Assumes assigning to yourself
					issue.assignee = _username;

					var result = _client.createIssue(_token, issue);

					IssuesCreated.Items.Add(result.summary);

					Issue.Text = String.Empty;
					Description.Text = String.Empty;
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}

				Issue.Focus();
			}
			else
			{
				MessageBox.Show("Please fill in the Project and Issue fields");
			}
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			SubmitIssue();
		}
	}
}