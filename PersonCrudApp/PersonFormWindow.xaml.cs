using PersonCrudApp.Models;
using System.Windows;

namespace PersonCrudApp
{
    /// <summary>
    /// Interaction logic for FormWindow.xaml
    /// </summary>
    public partial class PersonFormWindow : Window
    {
        public PersonFormWindow()
        {
            InitializeComponent();
        }

        enum FormType
        {
            New, Update
        }

        private readonly Person? Person;
        private readonly FormType Type;

        public PersonFormWindow(Person? person) : this()
        {

            if (person == null)
            {
                Person = new Person
                {
                    FirstName = "",
                    LastName = "",
                    Address = "",
                    TaxNumber = "",
                };
                Type = FormType.New;
            }
            else
            {
                Person = person;
                Type = FormType.Update;
            }

            Container.DataContext = Person;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (Type == FormType.New)
            {
                App.dbContext.People.Add(Person!);
            }
            else if (Type == FormType.Update)
            {
                App.dbContext.People.Update(Person!);
            }
            App.dbContext.SaveChanges();

            this.DialogResult = true;
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
