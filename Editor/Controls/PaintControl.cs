using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Editor.ViewModels;

namespace Editor.Controls
{
    /// <summary>
    /// A control that responds to mouse and keyboard, to edit and render an image
    /// </summary>
    public class PaintControl : Control
    {
        public PaintControlViewModel? Vm => DataContext as PaintControlViewModel;

        public PaintControl()
        {

        }

        protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
        {
            base.OnPropertyChanged(change);

            if (change.Property == DataContextProperty && Bounds.Size != default)
            {
                // let the view model know the PaintControl's size, so the image can be the correct size.
                Vm?.SetImageSize(new PixelSize((int)Bounds.Width, (int)Bounds.Height));

                // Request the image be rendered
                InvalidateVisual();
            }
        }
        
        private void OnPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            InvalidateVisual();
        }
    }
}
