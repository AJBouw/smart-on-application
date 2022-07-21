using System;
using System.ComponentModel;
using MvvmHelpers.Commands;

namespace SmartOnApp.ViewModels
{
    public class LandingPageViewModel : INotifyPropertyChanged
    {
        public LandingPageViewModel()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        string lightStatus = "off";
        public string LightStatus
        {
            get => lightStatus;
            set
            {
                lightStatus = value;
                var args = new PropertyChangedEventArgs(nameof(LandingPageViewModel));

                PropertyChanged?.Invoke(this, args);
            }
        }

        int brightness;
        public int Brightness
        {
            get => brightness;
            set
            {
                brightness = value;
                var args = new PropertyChangedEventArgs(nameof(LandingPageViewModel));

                PropertyChanged?.Invoke(this, args);
            }
        }

        string displayedLocation = "Backyard Fence";
        public string DisplayedLocation
        {
            get => displayedLocation;
            set
            {
                displayedLocation = value;
                var args = new PropertyChangedEventArgs(nameof(LandingPageViewModel));

                PropertyChanged?.Invoke(this, args);
            }
        }

        string lastUpdate = "01 - 01 - 2022 00:00 AM";
        public string LastUpdate
        {
            get => lastUpdate;
            set
            {
                lastUpdate = value;
                var args = new PropertyChangedEventArgs(nameof(LandingPageViewModel));

                PropertyChanged?.Invoke(this, args);
            }
        }

        string currentDate = "Wednesday 1 January 2022";
        public string CurrentDate
        {
            get => currentDate;
            set
            {
                currentDate = value;
                var args = new PropertyChangedEventArgs(nameof(LandingPageViewModel));

                PropertyChanged?.Invoke(this, args);
            }
        }

        string currentTime = "00:00 AM";
        public string CurrentTime
        {
            get => currentTime;
            set
            {
                currentTime = value;
                var args = new PropertyChangedEventArgs(nameof(LandingPageViewModel));

                PropertyChanged?.Invoke(this, args);
            }
        }

        string sunrise = "06:00 AM";
        public string Sunrise
        {
            get => sunrise;
            set
            {
                sunrise = value;
                var args = new PropertyChangedEventArgs(nameof(LandingPageViewModel));

                PropertyChanged?.Invoke(this, args);
            }
        }

        string sunset = "10:00 PM";
        public string Sunset
        {
            get => sunset;
            set
            {
                sunset = value;
                var args = new PropertyChangedEventArgs(nameof(LandingPageViewModel));

                PropertyChanged?.Invoke(this, args);
            }
        }
    }
}
