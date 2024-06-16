using System.Windows;
using System.Windows.Controls;
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

        SelectedPersonData.DataContext = PeopleList.SelectedItem;
    }

    private void UpdatePeople(int newIndex = 0)
    {
        People = App.GetContext().People.ToList<Person>();
        PeopleList.ItemsSource = People;

        if (People.Count > 0)
        {
            PeopleList.SelectedIndex = newIndex;
            UpdateSelectedPersonText((Person)PeopleList.SelectedItem);
        }
        else
        {
            UpdateSelectedPersonText(null);
        }
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

        SelectedPersonData.DataContext = PeopleList.SelectedItem;
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

        if (result == MessageBoxResult.Yes)
        {
            App.dbContext.People.Remove(selectedPerson);
            App.dbContext.SaveChanges();
            UpdatePeople();
        }
    }

    private void PeopleList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
        SelectedPersonData.DataContext = PeopleList.SelectedItem;
        UpdateSelectedPersonText((Person) PeopleList.SelectedItem);
    }

    private void UpdateSelectedPersonText(Person? person)
    {
        bool exists = person != null;

        FirstName.Text = exists ? person!.FirstName : "";
        LastName.Text = exists ? person!.LastName: "";
        Address.Text = exists ? person!.Address: "";
        TexNumber.Text = exists ? person!.TaxNumber: "";
    }
}
