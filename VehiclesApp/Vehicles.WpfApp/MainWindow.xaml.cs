using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Vehicles.Core;
using Vehicles.Core.Interfaces;
using Vehicles.Core.Models;
using Vehicles.Core.Resources;
using Vehicles.WpfApp.UIHelpers;
using Vehicles.WpfApp.ViewModels; // Connect ViewModel namespace

namespace Vehicles.WpfApp
{
    // Main window code class definition
    public partial class MainWindow : Window
    {
        // Reference to main view model managing application state
        public MainViewModel ViewModel { get; } = new();

        // Garage property for cars and amphibious cars
        public VehicleGarage CarGarage => ViewModel.CarGarage;

        // Garage property for boats
        public VehicleGarage BoatGarage => ViewModel.BoatGarage;

        // Store currently selected garage reference to avoid null warning
        private VehicleGarage? activeGarage;

        // Constructor running on application startup
        public MainWindow()
        {
            InitializeComponent();

            // Set data context for data binding implementation
            DataContext = ViewModel;

            // Connect garages to list box items sources
            CarGarageListBox.ItemsSource = CarGarage.Vehicles;
            BoatGarageListBox.ItemsSource = BoatGarage.Vehicles;

            // Subscribe to log messages from both garages using external helper
            CarGarage.OnLogMessage += message => LogHelper.AppendLog(LogTextBox, message);
            BoatGarage.OnLogMessage += message => LogHelper.AppendLog(LogTextBox, message);
        }

        // Triggered when user selects item in car garage list box
        private void CarGarageListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Check if selected item is valid vehicle instance
            if (CarGarageListBox.SelectedItem is Vehicle selectedVehicle)
            {
                // Unselect item in boat list box to keep single active selection
                BoatGarageListBox.SelectedItem = null;
                activeGarage = CarGarage;

                // Pass selected vehicle to view model state property
                ViewModel.SelectedVehicle = selectedVehicle;

                // Update selected vehicle name and odometer text blocks
                SelectedVehicleText.Text = $"{selectedVehicle.Make} {selectedVehicle.Model}";
                OdometerText.Text = string.Format(AppMessages.UI_OdometerFormat, selectedVehicle.Odometer);

                // Enable action buttons based on vehicle interface capabilities
                DriveButton.IsEnabled = selectedVehicle is IDriveable;
                SwimButton.IsEnabled = selectedVehicle is ISwimmable;
            }
            // Reset selection if both list boxes lack selection
            else if (BoatGarageListBox.SelectedItem == null)
            {
                ResetSelection();
            }
        }

        // Triggered when user selects item in boat garage list box
        private void BoatGarageListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Check if selected item is valid vehicle instance
            if (BoatGarageListBox.SelectedItem is Vehicle selectedVehicle)
            {
                // Unselect item in car list box to keep single active selection
                CarGarageListBox.SelectedItem = null;
                activeGarage = BoatGarage;

                // Pass selected vehicle to view model state property
                ViewModel.SelectedVehicle = selectedVehicle;

                // Update selected vehicle name and odometer text blocks
                SelectedVehicleText.Text = $"{selectedVehicle.Make} {selectedVehicle.Model}";
                OdometerText.Text = string.Format(AppMessages.UI_OdometerFormat, selectedVehicle.Odometer);

                // Enable action buttons based on vehicle interface capabilities
                DriveButton.IsEnabled = selectedVehicle is IDriveable;
                SwimButton.IsEnabled = selectedVehicle is ISwimmable;
            }
            // Reset selection if both list boxes lack selection
            else if (CarGarageListBox.SelectedItem == null)
            {
                ResetSelection();
            }
        }

        // Reset user interface labels and buttons when selection clears
        private void ResetSelection()
        {
            activeGarage = null;
            ViewModel.SelectedVehicle = null;
            SelectedVehicleText.Text = AppMessages.UI_SelectVehiclePrompt;
            OdometerText.Text = string.Format(AppMessages.UI_OdometerFormat, 0);
            DriveButton.IsEnabled = false;
            SwimButton.IsEnabled = false;
        }

        // Triggered when user clicks drive action button
        private void DriveButton_Click(object sender, RoutedEventArgs e)
        {
            // Execute drive action via view model logic
            ViewModel.DriveSelectedVehicle(LogTextBox);

            // Refresh list box items views after driving
            CarGarageListBox.Items.Refresh();
            BoatGarageListBox.Items.Refresh();
        }

        // Triggered when user clicks swim action button
        private void SwimButton_Click(object sender, RoutedEventArgs e)
        {
            // Execute swim action via view model logic
            ViewModel.SwimSelectedVehicle(LogTextBox);

            // Refresh list box items views after swimming
            CarGarageListBox.Items.Refresh();
            BoatGarageListBox.Items.Refresh();
        }

        // Add new car using view model method call
        private void AddCarButton_Click(object sender, RoutedEventArgs e) => ViewModel.AddCar();

        // Add new boat using view model method call
        private void AddBoatButton_Click(object sender, RoutedEventArgs e) => ViewModel.AddBoat();

        // Add new amphibious car using view model method call
        private void AddAmphibiousButton_Click(object sender, RoutedEventArgs e) => ViewModel.AddAmphibiousCar();

        // Remove selected vehicle from active garage
        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            // Check if active garage reference exists
            if (activeGarage != null)
            {
                // Find vehicle to remove from active list box selection
                Vehicle? vehicleToRemove = (CarGarageListBox.SelectedItem as Vehicle) ?? (BoatGarageListBox.SelectedItem as Vehicle);

                // Check if vehicle reference is valid
                if (vehicleToRemove != null)
                {
                    // Store target list box reference before deletion
                    ListBox targetListBox = (activeGarage == CarGarage) ? CarGarageListBox : BoatGarageListBox;

                    // Remove vehicle item from active garage instance
                    activeGarage.RemoveVehicle(vehicleToRemove);

                    // Format log message and append to log text box via helper
                    string logMessage = string.Format(AppMessages.UI_Log_VehicleRemoved, vehicleToRemove.Make, vehicleToRemove.Model);
                    LogHelper.AppendLog(LogTextBox, logMessage);

                    // Trigger headlight flashing animation for target list box items
                    FlashHeadlightsForListBox(targetListBox);

                    // Clear active selection state after removal
                    ResetSelection();
                }
            }
        }

        // Flash headlights yellow for two seconds for all items in list box asynchronously
        private async void FlashHeadlightsForListBox(ListBox listBox)
        {
            // Turn headlights fully on setting opacity to maximum value
            SetHeadlightsOpacity(listBox, 1.0);

            // Wait asynchronously for two thousand milliseconds without blocking interface thread
            await System.Threading.Tasks.Task.Delay(2000);

            // Dim headlights back down setting opacity to minimum value
            SetHeadlightsOpacity(listBox, 0.1);
        }

        // Set opacity value for headlight ellipse elements inside list box item containers
        private void SetHeadlightsOpacity(ListBox listBox, double opacity)
        {
            // Force interface layout update before searching visual elements
            listBox.UpdateLayout();

            // Loop through every item present in list box collection
            foreach (var item in listBox.Items)
            {
                // Retrieve visual container object for current list item
                var container = listBox.ItemContainerGenerator.ContainerFromItem(item) as ListBoxItem;

                // Check if container reference is valid
                if (container != null)
                {
                    // Find both headlight ellipses inside visual container using helper method
                    var headlight1 = FindVisualChild<System.Windows.Shapes.Ellipse>(container, "Headlight1");
                    var headlight2 = FindVisualChild<System.Windows.Shapes.Ellipse>(container, "Headlight2");

                    // Apply opacity value if headlight elements exist
                    if (headlight1 != null) headlight1.Opacity = opacity;
                    if (headlight2 != null) headlight2.Opacity = opacity;
                }
            }
        }

        // Generic recursive method finding visual child element matching type and name criteria
        private T? FindVisualChild<T>(DependencyObject parent, string childName) where T : DependencyObject
        {
            // Loop through all direct children of parent element in visual tree hierarchy
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                // Get specific child element at current index position
                var child = VisualTreeHelper.GetChild(parent, i);

                // Check if child matches target type and specified name string
                if (child is T typedChild && (child as FrameworkElement)?.Name == childName)
                {
                    // Return found element immediately matching criteria
                    return typedChild;
                }

                // Search recursively inside current child object descendants
                var descendant = FindVisualChild<T>(child, childName);

                // Return descendant if found during recursive search traversal
                if (descendant != null) return descendant;
            }

            // Return null if no matching visual element exists after checking all branches
            return null;
        }
    }
}