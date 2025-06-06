using System.Collections.ObjectModel;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia;
using Avalonia.Controls;
using Editor.ViewModels;
using Avalonia.Input;

namespace Editor.Controls;

public class MapCanvasControl : Control
{
    public MapCanvasControl()
    {
        PlacedTiles.CollectionChanged += PlacedTiles_CollectionChanged;
        PointerPressed += OnPointerPressed;
    }

    
    private void PlacedTiles_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        if (e.NewItems != null)
        {
            foreach (TileViewModel item in e.NewItems)
                item.PropertyChanged += PlacedTile_PropertyChanged;
        }
        if (e.OldItems != null)
        {
            foreach (TileViewModel item in e.OldItems)
                item.PropertyChanged -= PlacedTile_PropertyChanged;
        }
        InvalidateVisual();
    }

    private void PlacedTile_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        InvalidateVisual();
    }
    
    public ObservableCollection<TileViewModel> PlacedTiles { get; set; } = new ObservableCollection<TileViewModel>();
    
    public int SelectedTileX { get; set; }
    public int SelectedTileY { get; set; }
    public int TileSize { get; set; } = 32;
    
    private void OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        var point = e.GetPosition(this);
        int canvasX = (int)(point.X / TileSize) * TileSize;
        int canvasY = (int)(point.Y / TileSize) * TileSize;

        PlacedTiles.Add(new TileViewModel
        {
            TileX = SelectedTileX,
            TileY = SelectedTileY,
            CanvasX = canvasX,
            CanvasY = canvasY
        });

        InvalidateVisual();
    }

    public override void Render(DrawingContext context)
    {
        base.Render(context);
        
        if (PlacedTiles == null || DataContext is not MainWindowViewModel viewModel || viewModel.TilesetBitmap == null)
            return;
        
        foreach (var tile in PlacedTiles)
        {
            var sourceRect = new Rect(tile.TileX * TileSize, tile.TileY * TileSize, TileSize, TileSize);
            var destRect = new Rect(tile.CanvasX, tile.CanvasY, TileSize, TileSize);
            context.DrawImage(((MainWindowViewModel?)DataContext).TilesetBitmap, sourceRect, destRect);
        }
    }
}