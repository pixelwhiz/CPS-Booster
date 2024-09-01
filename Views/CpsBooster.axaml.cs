using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Controls;
using Avalonia;

namespace CPS_Booster.Views
{
    public partial class CpsBooster : Window
    {
        private bool _isDragging;
        private Point _lastPosition;

        public CpsBooster()
        {
            InitializeComponent();
        }

        private void OnPointerPressed(object? sender, PointerPressedEventArgs e)
        {
            // Enable dragging
            _isDragging = true;
            _lastPosition = e.GetPosition(this);
            this.Cursor = new Cursor(StandardCursorType.Hand);
        }

        private void OnPointerMoved(object? sender, PointerEventArgs e)
        {
            if (_isDragging)
            {
                // Calculate new position
                var currentPosition = e.GetPosition(this);
                var delta = currentPosition - _lastPosition;

                // Move the window
                Position = new PixelPoint(Position.X + (int)delta.X, Position.Y + (int)delta.Y);

                // Update the last position
                _lastPosition = currentPosition;
            }
        }

        private void OnPointerReleased(object? sender, PointerReleasedEventArgs e)
        {
            // Disable dragging
            _isDragging = false;
            this.Cursor = new Cursor(StandardCursorType.Arrow);
        }
    }
}