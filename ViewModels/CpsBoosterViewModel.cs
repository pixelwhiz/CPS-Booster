using System;
using System.ComponentModel;
using System.Timers;
using CommunityToolkit.Mvvm.ComponentModel; // Assuming you are using CommunityToolkit.Mvvm

namespace CPS_Booster.ViewModels
{
    public partial class CpsBoosterViewModel : ObservableObject
    {
        private int _clickCount;
        private double _cps;
        private Timer _timer;

        public double CPS
        {
            get => _cps;
            private set => SetProperty(ref _cps, value);
        }

        public string CPSDisplay => $"{CPS:F1} CPS";

        public CpsBoosterViewModel()
        {
            _timer = new Timer(1000);
            _timer.Elapsed += OnTimerElapsed;
            _timer.Start();
        }
        
        public void RegisterClick()
        {
            _clickCount++;
            OnPropertyChanged(nameof(CPSDisplay));
        }
        
        private void OnTimerElapsed(object? sender, ElapsedEventArgs e)
        {
            CPS = _clickCount;
            _clickCount = 0;
            OnPropertyChanged(nameof(CPSDisplay));
        }

    }
}