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
            KeyDownEvent.AddClassHandler<TopLevel>(OnKeyDown, handledEventsToo: true);
            KeyUpEvent.AddClassHandler<TopLevel>(OnKeyUp, handledEventsToo: true);
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

        private void OnKeyUp(object? sender, KeyEventArgs e)
        {
            if (Vm != null)
            {
                // Change rectangle color
                // Request the updated image be rendered, in case there is a marquee
                switch (e.Key)
                {
                    case Key.LeftShift:
                        Vm.Red = 0;
                        InvalidateVisual();
                        break;

                    case Key.LeftCtrl:
                        Vm.Green = 0;
                        InvalidateVisual();
                        break;

                    case Key.LeftAlt:
                        Vm.Blue = 0;
                        InvalidateVisual();
                        break;
                }
            }
        }
        
        private void OnKeyDown(object? sender, KeyEventArgs e)
        {
            if (Vm != null)
            {
                // Change rectangle color or cancel dragging
                // Request the updated image be rendered, in case there is a marquee
                switch (e.Key)
                {
                    case Key.LeftShift:
                        Vm.Red = 255;
                        InvalidateVisual();
                        break;

                    case Key.LeftCtrl:
                        Vm.Green = 255;
                        InvalidateVisual();
                        break;

                    case Key.LeftAlt:
                        Vm.Blue = 255;
                        InvalidateVisual();
                        break;

                    case Key.Escape:
                        Vm.Dragging = false;
                        InvalidateVisual();
                        break;
                }
            }
        }
    }
}
