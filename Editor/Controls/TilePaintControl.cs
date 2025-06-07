using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Editor.ViewModels;

namespace Editor.Controls;

public class TilePaintControl : PaintControl
{
    public ObservableCollection<TileViewModel> Tiles { get; set; } = new ObservableCollection<TileViewModel>();
        
    public int SelectedTileX { get; set; }
    public int SelectedTileY { get; set; }
    public int TileSize { get; set; } = 32;
    
    public TilePaintControl()
    {
        // Setup event handlers
        PointerMoved += OnPointerMoved;
        PointerPressed += OnPointerPressed;
        PointerReleased += OnPointerReleased;
        PointerCaptureLost += OnPointerCaptureLost;
        SizeChanged += OnSizeChanged;
        
        Tiles.CollectionChanged += OnCollectionChanged;
    }
    
     private void OnPointerMoved(object? sender, Avalonia.Input.PointerEventArgs e)
        {
            if (Vm != null)
            {
                // Update the mouse position, and the currently selected marquee
                var pos = e.GetPosition(this);
                Vm.Pos = pos;
                if (Vm.Dragging)
                {
                    Vm.Marquee = new Rect(
                        System.Math.Min(Vm.Origin.X, Vm.Pos.X),
                        System.Math.Min(Vm.Origin.Y, Vm.Pos.Y),
                        System.Math.Abs(Vm.Origin.X - Vm.Pos.X),
                        System.Math.Abs(Vm.Origin.Y - Vm.Pos.Y));
                    InvalidateVisual();
                }
                else
                {
                    Vm.Origin = pos;
                }
                e.Handled = true;
            }
        }

        private void OnPointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e)
        {
            if (Vm != null)
            {
                // Start the drag
                Vm.Dragging = true;
            }
            
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

        private void OnPointerReleased(object? sender, Avalonia.Input.PointerReleasedEventArgs e)
        {
            if (Vm != null)
            {
                if (Vm.Dragging == true)
                {
                    // Finish dragging
                    Vm.Dragging = false;

                    // Paint a new rectangle
                    Vm.AddRectangle();

                    // Request the updated image be rendered
                    InvalidateVisual();
                }
            }
        }

        private void OnPointerCaptureLost(object? sender, PointerCaptureLostEventArgs e)
        {
            if (Vm != null)
            {
                // finish Dragging
                Vm.Dragging = false;

                // Request the image be rendered (to clear any marquee)
                InvalidateVisual();
            }
        }

        private void OnSizeChanged(object? sender, SizeChangedEventArgs e)
        {
            if (Vm != null)
            {
                // Make sure the image matches the size of the control
                Vm.SetImageSize(new PixelSize((int)e.NewSize.Width, (int)e.NewSize.Height));

                // Request the updated image be rendered
                InvalidateVisual();
            }
        }

        public override void Render(DrawingContext context)
        {
            base.Render(context);

            // If there is an image in the view model, copy it to the PaintControl's drawing surface
            if (Vm?.Image != null)
            {
                context.DrawImage(Vm.Image, Bounds);
            }

            // If we are in a dragging operation, draw a dashed rectangle
            // The base color for the rectangle is the color for rectangle that the drag operation will draw
            // The alternative color is either black or white to contrast with the base color
            if (Vm?.Dragging == true)
            {
                var pen = new Pen(new SolidColorBrush(Color.FromRgb(Vm.Red, Vm.Green, Vm.Blue)));
                context.DrawRectangle(pen, Vm.Marquee.Translate(new Vector(0.5, 0.5)));
                byte altColor = (byte)(255 - Vm.Green);
                pen = new Pen(new SolidColorBrush(Color.FromRgb(altColor, altColor, altColor)),
                    dashStyle: DashStyle.Dash);
                context.DrawRectangle(pen, Vm.Marquee.Translate(new Vector(0.5, 0.5)));
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