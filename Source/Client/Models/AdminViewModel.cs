using Client;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace Client.Models;

public partial class AdminViewModel : ViewModelBase
{
    [ObservableProperty]
    public ObservableCollection<string> items = new();

    public AdminViewModel()
    {

    }
}