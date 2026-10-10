using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using Vehicles.Core;
using Vehicles.Core.Interfaces;
using Vehicles.Core.Models;
using Vehicles.Core.Resources;
using Vehicles.WpfApp.UIHelpers;

namespace Vehicles.WpfApp.ViewModels
{
    // Implement INotifyPropertyChanged interface for data binding support
    public class MainViewModel : INotifyPropertyChanged
    {
        // Initialize car garage instance
        public VehicleGarage CarGarage { get; } = new();

        // Initialize boat garage instance
        public VehicleGarage BoatGarage { get; } = new();

        // Property for distance input in UI
        private string distanceText = "10";
        public string DistanceText
        {
            get => distanceText;
            set
            {
                if (distanceText == value) return;
                distanceText = value;
                OnPropertyChanged();
            }
        }

        // Constructor initializing default items on startup
        public MainViewModel()
        {
            // Add default car to car garage
            CarGarage.AddVehicle(new Car("Volvo", "V60", 120));

            // Add default boat to boat garage
            BoatGarage.AddVehicle(new Boat("Bella", "600", 50));

            // Add default amphibious car to car garage
            CarGarage.AddVehicle(new AmphibiousCar("Amphi", "X", 10));
        }

        // Add a new Toyota RAV4 car to the car garage
        public void AddCar()
        {
            CarGarage.AddVehicle(new Car("Toyota", "RAV4", 0));
        }

        // Add a new Yamaha boat to the boat garage
        public void AddBoat()
        {
            BoatGarage.AddVehicle(new Boat("Yamaha", "Breeze", 0));
        }

        // Add a new Amphibious car to the car garage
        public void AddAmphibiousCar()
        {
            CarGarage.AddVehicle(new AmphibiousCar("Gibbs", "Aquada", 0));
        }

        // Backing field for selected vehicle state
        private Vehicle? selectedVehicle;

        // Property representing currently selected vehicle
        public Vehicle? SelectedVehicle
        {
            get => selectedVehicle;
            set
            {
                // Check if new value equals current value
                if (selectedVehicle == value) return;

                // Update backing field with new value
                selectedVehicle = value;

                // Notify UI about selected vehicle change
                OnPropertyChanged();

                // Notify UI about dependent text property change
                OnPropertyChanged(nameof(SelectedVehicleText));
                OnPropertyChanged(nameof(OdometerText));
                OnPropertyChanged(nameof(CanDrive));
                OnPropertyChanged(nameof(CanSwim));
            }
        }

        // Computed property returning formatted text for selected vehicle UI display
        public string SelectedVehicleText => SelectedVehicle != null
            ? $"{SelectedVehicle.Make} {SelectedVehicle.Model}"
            : AppMessages.UI_SelectVehiclePrompt;

        // Computed property returning formatted odometer text
        public string OdometerText => string.Format(AppMessages.UI_OdometerFormat, SelectedVehicle?.Odometer ?? 0);

        // Properties controlling button enabled states based on vehicle interfaces
        public bool CanDrive => SelectedVehicle is IDriveable;
        public bool CanSwim => SelectedVehicle is ISwimmable;

        // Drive action logic for selected vehicle
        public void DriveSelectedVehicle(System.Windows.Controls.TextBox logTextBox)
        {
            if (SelectedVehicle is IDriveable driveable)
            {
                try
                {
                    if (double.TryParse(DistanceText, out double km))
                    {
                        string message = driveable.Drive(km);
                        LogHelper.AppendLog(logTextBox, message);
                        OnPropertyChanged(nameof(OdometerText));
                    }
                    else
                    {
                        MessageBox.Show(AppMessages.UI_InvalidInputMessage, AppMessages.UI_ErrorTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                catch (System.ArgumentException ex)
                {
                    MessageBox.Show(ex.Message, AppMessages.UI_Error_ValidationTitle, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        // Swim action logic for selected vehicle
        public void SwimSelectedVehicle(System.Windows.Controls.TextBox logTextBox)
        {
            if (SelectedVehicle is ISwimmable swimmable)
            {
                try
                {
                    if (double.TryParse(DistanceText, out double km))
                    {
                        string message = swimmable.Swim(km);
                        LogHelper.AppendLog(logTextBox, message);
                        OnPropertyChanged(nameof(OdometerText));
                    }
                    else
                    {
                        MessageBox.Show(AppMessages.UI_InvalidInputMessage, AppMessages.UI_ErrorTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                catch (System.ArgumentException ex)
                {
                    MessageBox.Show(ex.Message, AppMessages.UI_Error_ValidationTitle, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        // Event for property change notifications
        public event PropertyChangedEventHandler? PropertyChanged;

        // Helper method invoking property changed event
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}