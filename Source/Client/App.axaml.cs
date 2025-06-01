using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Client;
using Core;
using System.Collections.ObjectModel;

namespace AvaloniaAppTemplate;

public partial class App : Avalonia.Application
{
    public ObservableCollection<string> Items { get; set; } = new ObservableCollection<string>();

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new AdminWindow();
            desktop.MainWindow.Closing += (s, e) =>
            {
                e.Cancel = true;
                desktop.MainWindow.Hide();
            };
        }

        Client.Program.StartGameThread();
        base.OnFrameworkInitializationCompleted();
    }

    public static void ShowWindowByName(string windowName)
    {
        Avalonia.Threading.Dispatcher.UIThread.Post(() =>
        {
            if (Avalonia.Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                foreach (var window in desktop.Windows)
                {
                    if (window != null)
                    {
                        if (window.Title == windowName)
                        {
                            window.Show();
                            window.Activate();
                            return;
                        }
                    }
                }
            }
        });
    }

    public static void HideWindowByName(string windowName)
    {
        Avalonia.Threading.Dispatcher.UIThread.Post(() =>
        {
            if (Avalonia.Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                foreach (var window in desktop.Windows)
                {
                    if (window.Title == windowName)
                    {
                        window.Hide();
                        return;
                    }
                }
            }
        });
    }

    public static void CloseWindowByName(string windowName)
    {
        Avalonia.Threading.Dispatcher.UIThread.Post(() =>
        {
            if (Avalonia.Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                foreach (var window in desktop.Windows)
                {
                    if (window.Title == windowName)
                    {
                        window.Close();
                        return;
                    }
                }
            }
        });
    }

    public static Window? GetWindowByName(string windowName)
    {
        Window? result = null;

        if (Avalonia.Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            foreach (var window in desktop.Windows)
            {
                if (window != null)
                {
                    if (window.Title == windowName)
                    {
                        result = window;
                        break;
                    }
                }
            }
        }
        return result;
    }

    public static Avalonia.Controls.Control? GetControlByName(Avalonia.Controls.Window window, string controlName)
    {
        Avalonia.Controls.Control? result = null;

        if (Avalonia.Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            result = window.FindControl<Avalonia.Controls.Control>(controlName);
        }
        return result;
    }
}
