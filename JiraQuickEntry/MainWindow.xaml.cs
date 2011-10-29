using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace JiraQuickEntry
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private JiraSoapServiceClient _client;
		private List<RemoteIssueType> _issueTypes = new List<RemoteIssueType>();
		private List<RemoteProject> _projects = new List<RemoteProject>();
		private string _token;
		private string _username;

		public MainWindow()
		{
			InitializeComponent();

			Loaded += MainWindow_Loaded;
			KeyDown += MainWindow_KeyDown;
		}

		private void MainWindow_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter && Keyboard.Modifiers == ModifierKeys.Control)
			{
				SubmitIssue();
			}
		}

		private void MainWindow_Loaded(object sender, RoutedEventArgs e)
		{
			var login = new Login();
			login.ShowDialog();

			_token = login.Token;
			_username = login.User;
			_client = new JiraSoapServiceClient("jirasoapservice-v2");

			_projects = _client.getProjectsNoSchemes(_token).ToList();

			Project.ItemsSource = _projects;
			Project.SelectedIndex = 0;

			IssueType.ItemsSource = _issueTypes;
			UpdateIssueTypes();
		}

		private void SubmitIssue()
		{
			if (!String.IsNullOrEmpty(Issue.Text) && !String.IsNullOrEmpty(Project.Text))
			{
				try
				{
					var issue = new RemoteIssue();
					issue.project = ((RemoteProject)Project.SelectedItem).key;
					issue.summary = Issue.Text;
					issue.description = Description.Text;

					// Defaulting to New Feature for the moment
					issue.type = IssueType.SelectedValue.ToString();

					// Assumes assigning to yourself
					issue.assignee = _username;

					RemoteIssue result = _client.createIssue(_token, issue);

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

		private void UpdateIssueTypes()
		{
			try
			{
				RemoteProject project = _client.getProjectById(_token, long.Parse(Project.SelectedValue.ToString()));

				_issueTypes = _client.getIssueTypesForProject(_token, project.id).ToList();
				IssueType.Items.Refresh();
				IssueType.SelectedIndex = 0;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void Project_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			UpdateIssueTypes();
		}
	}
}