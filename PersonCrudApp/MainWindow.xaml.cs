using System.Windows;
using PersonCrudApp.Models;

namespace PersonCrudApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public List<Person> People = new List<Person>();

    public MainWindow()
    {
        InitializeComponent();
        UpdatePeople();
    }

    private void UpdatePeople(int newIndex = 0)
    {
        People = App.GetContext().People.ToList<Person>();
        PeopleList.ItemsSource = People;

        if (People.Count > 0)
        {
            PeopleList.SelectedIndex = newIndex;
        }

        SelectedPersonData.DataContext = PeopleList.SelectedItem;
    }

    private void New_Click(object sender, RoutedEventArgs e)
    {
        PersonFormWindow win = new(null);
        bool? result = win.ShowDialog();

        if (result == true)
        {
            UpdatePeople(Math.Max(PeopleList.SelectedIndex, 0));
        }
    }

    private void Edit_Click(object sender, RoutedEventArgs e)
    {
        if (PeopleList.SelectedItem == null)
        {
            MessageBox.Show("No person was selected for editing", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        PersonFormWindow win = new((Person)PeopleList.SelectedItem);
        bool? result = win.ShowDialog();

        if (result == true)
        {
            UpdatePeople(PeopleList.SelectedIndex);
        }
    }

    private void Delete_Click(object sender, RoutedEventArgs e)
    {
        if (PeopleList.SelectedItem == null)
        {
            MessageBox.Show("No person was selected for deleting", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        MessageBoxResult result = MessageBox.Show("Do you want to delete the selected person?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

        Person selectedPerson = (Person)PeopleList.SelectedItem;
        Person? foundPerson = App.dbContext.People.Find(selectedPerson.Id);
        bool personExists = foundPerson != null;

        if (!personExists)
        {
            MessageBox.Show("Selected person does not exist.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        if (result.HasFlag(MessageBoxResult.Yes))
        {
            App.dbContext.People.Remove(selectedPerson);
            App.dbContext.SaveChanges();
            UpdatePeople();
        }
    }

    private void PeopleList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
        SelectedPersonData.DataContext = PeopleList.SelectedItem;
    }
}
