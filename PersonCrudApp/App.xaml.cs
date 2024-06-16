using PersonCrudApp.Data;
using System.Windows;

namespace PersonCrudApp;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public static PeopleDbContext dbContext = new();

    public static PeopleDbContext GetContext()
    {
        return dbContext;
    }
}
