using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Vehicles.Core;
using Vehicles.Core.Models;
using Vehicles.Core.Interfaces;
using Vehicles.Core.Resources;

namespace Vehicles.WpfApp
{
    // Main window code
    public partial class MainWindow : Window
    {
        // Garage for cars and amphibious cars
        public VehicleGarage CarGarage { get; } = new();

        // Garage for boats
        public VehicleGarage BoatGarage { get; } = new();

        // Saves which garage is currently selected (using VehicleGarage? to avoid null warning)
        private VehicleGarage? activeGarage;

        // Constructor - runs when the app opens
        public MainWindow()
        {
            InitializeComponent();

            // Connect garages to the UI lists
            CarGarageListBox.ItemsSource = CarGarage.Vehicles;
            BoatGarageListBox.ItemsSource = BoatGarage.Vehicles;

            // Listen to log messages from both garages
            CarGarage.OnLogMessage += AppendLog;
            BoatGarage.OnLogMessage += AppendLog;

            // Add some default items on startup
            CarGarage.AddVehicle(new Car("Volvo", "V60", 120));
            BoatGarage.AddVehicle(new Boat("Bella", "600", 50));
            CarGarage.AddVehicle(new AmphibiousCar("Amphi", "X", 10));
        }

        // Helper method to add text to the log box and scroll down
        private void AppendLog(string message)
        {
            LogTextBox.AppendText(message + "\n");
            LogTextBox.ScrollToEnd();
        }

        // Triggered when a car is clicked in the Car Garage list
        private void CarGarageListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CarGarageListBox.SelectedItem is Vehicle selectedVehicle)
            {
                // Unselect item in boat list so only one car is active
                BoatGarageListBox.SelectedItem = null;
                activeGarage = CarGarage;

                // Show selected vehicle name and odometer in UI
                SelectedVehicleText.Text = $"{selectedVehicle.Make} {selectedVehicle.Model}";
                OdometerText.Text = string.Format(AppMessages.UI_OdometerFormat, selectedVehicle.Odometer);

                // Enable buttons depending on what vehicle can do
                DriveButton.IsEnabled = selectedVehicle is IDriveable;
                SwimButton.IsEnabled = selectedVehicle is ISwimmable;
            }
            else if (BoatGarageListBox.SelectedItem == null)
            {
                ResetSelection();
            }
        }

        // Triggered when a boat is clicked in the Boat Garage list
        private void BoatGarageListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BoatGarageListBox.SelectedItem is Vehicle selectedVehicle)
            {
                // Unselect item in car list
                CarGarageListBox.SelectedItem = null;
                activeGarage = BoatGarage;

                // Show selected vehicle name and odometer in UI
                SelectedVehicleText.Text = $"{selectedVehicle.Make} {selectedVehicle.Model}";
                OdometerText.Text = string.Format(AppMessages.UI_OdometerFormat, selectedVehicle.Odometer);

                // Enable buttons depending on what vehicle can do
                DriveButton.IsEnabled = selectedVehicle is IDriveable;
                SwimButton.IsEnabled = selectedVehicle is ISwimmable;
            }
            else if (CarGarageListBox.SelectedItem == null)
            {
                ResetSelection();
            }
        }

        // Reset UI labels and buttons when nothing is selected
        private void ResetSelection()
        {
            activeGarage = null;
            SelectedVehicleText.Text = AppMessages.UI_SelectVehiclePrompt;
            OdometerText.Text = string.Format(AppMessages.UI_OdometerFormat, 0);
            DriveButton.IsEnabled = false;
            SwimButton.IsEnabled = false;
        }

        // Triggered when Drive button is clicked
        private void DriveButton_Click(object sender, RoutedEventArgs e)
        {
            // Find selected vehicle from either car list or boat list (using Vehicle? to avoid nullable warning)
            Vehicle? selectedVehicle = (CarGarageListBox.SelectedItem as Vehicle) ?? (BoatGarageListBox.SelectedItem as Vehicle);

            if (selectedVehicle is IDriveable driveable)
            {
                try
                {
                    // Check if input is a valid number
                    if (double.TryParse(DistanceTextBox.Text, out double km))
                    {
                        // Drive the vehicle and print message to log
                        string message = driveable.Drive(km);
                        AppendLog(message);

                        // Update odometer and refresh UI lists
                        OdometerText.Text = string.Format(AppMessages.UI_OdometerFormat, selectedVehicle.Odometer);
                        CarGarageListBox.Items.Refresh();
                        BoatGarageListBox.Items.Refresh();
                    }
                    else
                    {
                        // Show warning if input is not a number
                        MessageBox.Show(AppMessages.UI_InvalidInputMessage, AppMessages.UI_ErrorTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                catch (System.ArgumentException ex)
                {
                    // Show error popup if distance is negative/zero
                    MessageBox.Show(ex.Message, AppMessages.UI_Error_ValidationTitle, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        // Triggered when Swim button is clicked
        private void SwimButton_Click(object sender, RoutedEventArgs e)
        {
            // Find selected vehicle from either list (using Vehicle? to avoid nullable warning)
            Vehicle? selectedVehicle = (CarGarageListBox.SelectedItem as Vehicle) ?? (BoatGarageListBox.SelectedItem as Vehicle);

            if (selectedVehicle is ISwimmable swimmable)
            {
                try
                {
                    // Check if input is a valid number
                    if (double.TryParse(DistanceTextBox.Text, out double km))
                    {
                        // Swim the vehicle and print message to log
                        string message = swimmable.Swim(km);
                        AppendLog(message);

                        // Update odometer and refresh UI lists
                        OdometerText.Text = string.Format(AppMessages.UI_OdometerFormat, selectedVehicle.Odometer);
                        CarGarageListBox.Items.Refresh();
                        BoatGarageListBox.Items.Refresh();
                    }
                    else
                    {
                        // Show warning for wrong input
                        MessageBox.Show(AppMessages.UI_InvalidInputMessage, AppMessages.UI_ErrorTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                catch (System.ArgumentException ex)
                {
                    // Show error popup for invalid numbers
                    MessageBox.Show(ex.Message, AppMessages.UI_Error_ValidationTitle, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        // Add a new Toyota RAV4 car to the car garage
        private void AddCarButton_Click(object sender, RoutedEventArgs e)
        {
            CarGarage.AddVehicle(new Car("Toyota", "RAV4", 0));
        }

        // Add a new Yamaha boat to the boat garage
        private void AddBoatButton_Click(object sender, RoutedEventArgs e)
        {
            BoatGarage.AddVehicle(new Boat("Yamaha", "Breeze", 0));
        }

        // Add a new Amphibious car to the car garage
        private void AddAmphibiousButton_Click(object sender, RoutedEventArgs e)
        {
            CarGarage.AddVehicle(new AmphibiousCar("Gibbs", "Aquada", 0));
        }

        // Remove selected vehicle from its garage
        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (activeGarage != null)
            {
                Vehicle? vehicleToRemove = (CarGarageListBox.SelectedItem as Vehicle) ?? (BoatGarageListBox.SelectedItem as Vehicle);

                if (vehicleToRemove != null)
                {
                    // Запоминаем текущий активный гараж перед удалением
                    ListBox targetListBox = (activeGarage == CarGarage) ? CarGarageListBox : BoatGarageListBox;

                    // Remove item from active garage
                    activeGarage.RemoveVehicle(vehicleToRemove);

                    // Log removal action
                    string logMessage = string.Format(AppMessages.UI_Log_VehicleRemoved, vehicleToRemove.Make, vehicleToRemove.Model);
                    AppendLog(logMessage);

                    // Trigger visual headlight flashing for remaining vehicles
                    FlashHeadlightsForListBox(targetListBox);

                    // Clear active selection state
                    ResetSelection();
                }
            }
        }

        // Включаем фары желтыми на 2 секунды для всех элементов в ListBox
        private async void FlashHeadlightsForListBox(ListBox listBox)
        {
            SetHeadlightsOpacity(listBox, 1.0);
            await System.Threading.Tasks.Task.Delay(2000);
            SetHeadlightsOpacity(listBox, 0.1);
        }

        // Set opacity for headlight ellipses inside the ListBox items
        private void SetHeadlightsOpacity(ListBox listBox, double opacity)
        {
            listBox.UpdateLayout();

            foreach (var item in listBox.Items)
            {
                var container = listBox.ItemContainerGenerator.ContainerFromItem(item) as ListBoxItem;
                if (container != null)
                {
                    var headlight1 = FindVisualChild<System.Windows.Shapes.Ellipse>(container, "Headlight1");
                    var headlight2 = FindVisualChild<System.Windows.Shapes.Ellipse>(container, "Headlight2");

                    if (headlight1 != null) headlight1.Opacity = opacity;
                    if (headlight2 != null) headlight2.Opacity = opacity;
                }
            }
        }

        // Find visual child element by name
        private T? FindVisualChild<T>(DependencyObject parent, string childName) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is T typedChild && (child as FrameworkElement)?.Name == childName)
                {
                    return typedChild;
                }

                var descendant = FindVisualChild<T>(child, childName);
                if (descendant != null) return descendant;
            }
            return null;
        }
    }
}