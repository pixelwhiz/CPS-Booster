using System;
using System.Runtime.InteropServices;
using Avalonia.Controls;
using Avalonia.Interactivity;
using CPS_Booster.ViewModels;

namespace CPS_Booster.Views.UserControls
{
    public partial class Content : UserControl
    {

        public bool isSimulateStarting = false;
        
        public Content()
        {
            InitializeComponent();
            DataContext = new CpsBoosterViewModel();
        }

        [DllImport("core.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void start(int cps, bool leftClick, bool middleClick, bool rightClick);

        [DllImport("core.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void stop();
        
        private void starts(object sender, RoutedEventArgs e)
        {
            int cps = (int)(numericUpDown.Value ?? 0);
            bool leftClick = leftClickCheckBox.IsChecked ?? false;
            bool middleClick = rightClickCheckBox.IsChecked ?? false;
            bool rightClick = rightClickCheckBox.IsChecked ?? false;
            
            start(cps, leftClick, middleClick, rightClick);
            stopButton.IsVisible = true;
            startButton.IsVisible = false;
        }

        private void stops(object sender, RoutedEventArgs e)
        {
            stop();
            stopButton.IsVisible = false;
            startButton.IsVisible = true;
        }
    }
}