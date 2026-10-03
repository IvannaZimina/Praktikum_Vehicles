using System.Collections.ObjectModel;
using System.Windows;
using Vehicles.Core;
using Vehicles.Core.Interfaces;

namespace Vehicles.WpfApp
{
    public partial class MainWindow : Window
    {
        // Observable collection to store all vehicles, automatically updates the UI on changes
        public ObservableCollection<Vehicle> Vehicles { get; set; } = new();

        public MainWindow()
        {
            InitializeComponent();

            // Bind the vehicle collection to the ListBox
            VehicleListBox.ItemsSource = Vehicles;

            // Add some initial sample data for demonstration
            Vehicles.Add(new Car("Volvo", "V60", 120));
            Vehicles.Add(new Boat("Bella", "600", 50));
            Vehicles.Add(new AmphibiousCar("Amphi", "X", 10));
        }

        // Handles selection change to dynamically enable/disable buttons based on supported interfaces
        private void VehicleListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (VehicleListBox.SelectedItem is Vehicle selectedVehicle)
            {
                SelectedVehicleText.Text = $"{selectedVehicle.Make} {selectedVehicle.Model}";
                OdometerText.Text = $"Läbisõit: {selectedVehicle.Odometer} km";

                // Enable/disable buttons polymorphically using interfaces (no massive if/switch by type!)
                DriveButton.IsEnabled = selectedVehicle is IDriveable;
                SwimButton.IsEnabled = selectedVehicle is ISwimmable;
            }
            else
            {
                SelectedVehicleText.Text = "Vali sõiduk nimekirjast";
                OdometerText.Text = "Läbisõit: 0 km";
                DriveButton.IsEnabled = false;
                SwimButton.IsEnabled = false;
            }
        }

        // Handles the Drive action for IDriveable vehicles
        private void DriveButton_Click(object sender, RoutedEventArgs e)
        {
            if (VehicleListBox.SelectedItem is IDriveable driveable && VehicleListBox.SelectedItem is Vehicle vehicle)
            {
                try
                {
                    if (double.TryParse(DistanceTextBox.Text, out double km))
                    {
                        // Call the polymorphic drive method
                        string message = driveable.Drive(km);
                        LogTextBox.AppendText(message + "\n");

                        // Update odometer display
                        OdometerText.Text = $"Läbisõit: {vehicle.Odometer} km";
                        VehicleListBox.Items.Refresh();
                    }
                    else
                    {
                        MessageBox.Show("Vigane sisend! Palun sisesta number.", "Viga", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                catch (System.ArgumentException ex)
                {
                    // Catch validation errors from Core without breaking the object state
                    MessageBox.Show(ex.Message, "Valideerimisviga", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        // Handles the Swim action for ISwimmable vehicles
        private void SwimButton_Click(object sender, RoutedEventArgs e)
        {
            if (VehicleListBox.SelectedItem is ISwimmable swimmable && VehicleListBox.SelectedItem is Vehicle vehicle)
            {
                try
                {
                    if (double.TryParse(DistanceTextBox.Text, out double km))
                    {
                        // Call the polymorphic swim method
                        string message = swimmable.Swim(km);
                        LogTextBox.AppendText(message + "\n");

                        // Update odometer display
                        OdometerText.Text = $"Läbisõit: {vehicle.Odometer} km";
                        VehicleListBox.Items.Refresh();
                    }
                    else
                    {
                        MessageBox.Show("Vigane sisend! Palun sisesta number.", "Viga", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                catch (System.ArgumentException ex)
                {
                    MessageBox.Show(ex.Message, "Valideerimisviga", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        // Adds a new Car to the collection
        private void AddCarButton_Click(object sender, RoutedEventArgs e)
        {
            Vehicles.Add(new Car("Toyota", "RAV4", 0));
        }

        // Adds a new Boat to the collection
        private void AddBoatButton_Click(object sender, RoutedEventArgs e)
        {
            Vehicles.Add(new Boat("Yamaha", "Breeze", 0));
        }

        // Adds a new Amphibious Car to the collection
        private void AddAmphibiousButton_Click(object sender, RoutedEventArgs e)
        {
            Vehicles.Add(new AmphibiousCar("Gibbs", "Aquada", 0));
        }

        // Removes the selected vehicle from the collection
        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (VehicleListBox.SelectedItem is Vehicle selectedVehicle)
            {
                Vehicles.Remove(selectedVehicle);
                LogTextBox.AppendText($"Eemaldatud: {selectedVehicle.Make} {selectedVehicle.Model}\n");
            }
        }
    }
}