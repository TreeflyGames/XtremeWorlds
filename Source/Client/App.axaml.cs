using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

namespace AvaloniaAppTemplate;

public partial class App : Avalonia.Application
{
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

        base.OnFrameworkInitializationCompleted();
    }

    public static void ShowWindowByName(string windowName)
    {
        if (Avalonia.Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            foreach (var window in desktop.Windows)
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

    public static void HideWindowByName(string windowName)
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
    }

    public static void CloseWindowByName(string windowName)
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
    }

    public static Window? GetWindowByName(string windowName)
    {
        if (Avalonia.Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            foreach (var window in desktop.Windows)
            {
                if (window.Title == windowName)
                {
                    return window;
                }
            }
        }
        return null;
    }

    public static Avalonia.Controls.Control? GetControlByName(Avalonia.Controls.Window window, string controlName)
    {
        if (Avalonia.Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            if (window != null)
            {
                return window.FindControl<Avalonia.Controls.Control>(controlName);
            }
        }
        return null;
    }
}
