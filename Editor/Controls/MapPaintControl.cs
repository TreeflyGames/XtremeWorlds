using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Editor.ViewModels;

namespace Editor.Controls;

public class MapPaintControl : Control
{
    public ObservableCollection<TileViewModel> Tiles { get; set; } = new ObservableCollection<TileViewModel>();
        
    public int SelectedTileX { get; set; }
    public int SelectedTileY { get; set; }
    public int TileSize { get; set; } = 32;
    
    public override void Render(DrawingContext context)
    {
        base.Render(context);
            
        if (DataContext is not TileViewModel)
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
}