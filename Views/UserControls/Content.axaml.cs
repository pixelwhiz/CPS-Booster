using Avalonia.Controls;
using Avalonia.Interactivity;
using CPS_Booster.ViewModels;

namespace CPS_Booster.Views.UserControls
{
    public partial class Content : UserControl
    {
        public Content()
        {
            InitializeComponent();
            DataContext = new CpsBoosterViewModel();
        }


        private void ClickTesterButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is CpsBoosterViewModel viewModel)
            {
                viewModel.RegisterClick();
            }
        }
    }
}