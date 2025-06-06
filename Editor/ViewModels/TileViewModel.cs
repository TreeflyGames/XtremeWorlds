using Avalonia;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using System.ComponentModel;

namespace Editor.ViewModels;

public class TileViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private int _tilesetIndex;
    public int TilesetIndex
    {
        get => _tilesetIndex;
        set
        {
            if (_tilesetIndex != value)
            {
                _tilesetIndex = value;
                OnPropertyChanged(nameof(TilesetIndex));
            }
        }
    }

    private int _tileX;
    public int TileX
    {
        get => _tileX;
        set
        {
            if (_tileX != value)
            {
                _tileX = value;
                OnPropertyChanged(nameof(TileX));
            }
        }
    }

    private int _tileY;
    public int TileY
    {
        get => _tileY;
        set
        {
            if (_tileY != value)
            {
                _tileY = value;
                OnPropertyChanged(nameof(TileY));
            }
        }
    }

    private double _canvasX;
    public double CanvasX
    {
        get => _canvasX;
        set
        {
            if (_canvasX != value)
            {
                _canvasX = value;
                OnPropertyChanged(nameof(CanvasX));
            }
        }
    }

    private double _canvasY;
    public double CanvasY
    {
        get => _canvasY;
        set
        {
            if (_canvasY != value)
            {
                _canvasY = value;
                OnPropertyChanged(nameof(CanvasY));
            }
        }
    }

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}