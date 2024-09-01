using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.VisualTree;

namespace CPS_Booster.Views.UserControls
{
    public partial class Heading : UserControl
    {
        private bool _isDragging;
        private Point _dragStartPoint;

        public Heading()
        {
            InitializeComponent();
        }

        private void OnPointerPressed(object sender, PointerPressedEventArgs e)
        {
            if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
            {
                _isDragging = true;
                _dragStartPoint = e.GetPosition(this);
            }
        }

        private void OnPointerMoved(object sender, PointerEventArgs e)
        {
            if (_isDragging)
            {
                var currentPosition = e.GetPosition(this);
                var offset = currentPosition - _dragStartPoint;

                var window = this.GetVisualRoot() as Window;
                if (window != null)
                {
                    var newPosition = new PixelPoint((int)(window.Position.X + offset.X),
                        (int)(window.Position.Y + offset.Y));
                    window.Position = newPosition;
                }
            }
        }

        private void OnPointerReleased(object sender, PointerReleasedEventArgs e)
        {
            if (e.Pointer.IsPrimary)
            {
                _isDragging = false;
            }
        }

        private void MinimizeWindow(object sender, RoutedEventArgs e)
        {
            var window = this.GetVisualRoot() as Window;
            if (window != null)
            {
                window.WindowState = WindowState.Minimized;
            }
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            var window = this.GetVisualRoot() as Window;
            if (window != null)
            {
                window.Close();
            }
        }

        private void MoveWindow(object sender, PointerPressedEventArgs e)
        {
            var window = this.GetVisualRoot() as Window;
            if (window != null)
            {
                window.BeginMoveDrag(e);
            }
        }
        
        private void OnLogoButtonClick(object sender, RoutedEventArgs e)
        {
            // Toggle popup visibility
            var popup = this.FindControl<Popup>("LogoPopup");
            if (popup != null)
            {
                popup.IsOpen = !popup.IsOpen;
            }
        }

        private void OnWindowPointerPressed(object sender, PointerPressedEventArgs e)
        {
            var popup = this.FindControl<Popup>("LogoPopup");
            if (popup != null && popup.IsOpen)
            {
                // Check if the click is outside the popup
                var pointerPosition = e.GetPosition(this);
                var popupBounds = popup.Bounds;

                if (!popupBounds.Contains(pointerPosition))
                {
                    popup.IsOpen = false;
                }
            }
        }
    }
}