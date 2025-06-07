using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Editor.ViewModels;

namespace Editor.Controls;

public class MapPaintControl : Control
{
    public MapPaintControl()
    {
        Tiles.CollectionChanged += OnCollectionChanged;
        PointerPressed += OnPointerPressed;
        
    }
    public ObservableCollection<TileViewModel> Tiles { get; set; } = new ObservableCollection<TileViewModel>();
        
    public int SelectedTileX { get; set; }
    public int SelectedTileY { get; set; }
    public int TileSize { get; set; } = 32;

    private void OnPointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e)
    {
        var point = e.GetPosition(this);
        int canvasX = (int)(point.X / TileSize) * TileSize;
        int canvasY = (int)(point.Y / TileSize) * TileSize;

        Tiles.Add(new TileViewModel
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
            
        if (DataContext is not PaintControlViewModel)
        {
            return; // No data context, nothing to draw
        }
            
        // Draw all the tiles in the Tiles collection
        foreach (var tile in Tiles)
        {
            var sourceRect = new Rect(tile.TileX * TileSize, tile.TileY * TileSize, TileSize, TileSize);
            var destRect = new Rect(tile.CanvasX, tile.CanvasY, TileSize, TileSize);
            context.DrawImage(((PaintControlViewModel?)DataContext).Image, sourceRect, destRect);
        }
    }
    
    private void OnCollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        if (e.NewItems != null)
        {
            foreach (TileViewModel item in e.NewItems)
                item.PropertyChanged += OnPropertyChanged;
        }
        if (e.OldItems != null)
        {
            foreach (TileViewModel item in e.OldItems)
                item.PropertyChanged -= OnPropertyChanged;
        }
        InvalidateVisual();
    }
    
    private void OnPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        InvalidateVisual();
    }
}