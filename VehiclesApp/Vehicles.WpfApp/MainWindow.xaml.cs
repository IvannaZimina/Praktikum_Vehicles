using System.Collections.ObjectModel;
using System.Windows;
using Vehicles.Core;
using Vehicles.Core.Interfaces;

namespace Vehicles.WpfApp
{
    // Interaction logic class for MainWindow.xaml, inheriting from WPF Window base class
    public partial class MainWindow : Window
    {
        // Observable collection to store all vehicles, automatically updates the UI on changes
        public ObservableCollection<Vehicle> Vehicles { get; set; } = new();

        // Constructor of the main window executed when the application starts up
        public MainWindow()
        {
            // Initializes and loads all visual components defined in the XAML file
            InitializeComponent();

            // Bind the vehicle collection data source to the ListBox UI element
            VehicleListBox.ItemsSource = Vehicles;

            // Add some initial sample data items to the collection for demonstration
            Vehicles.Add(new Car("Volvo", "V60", 120));
            Vehicles.Add(new Boat("Bella", "600", 50));
            Vehicles.Add(new AmphibiousCar("Amphi", "X", 10));
        }

        // Event handler triggered whenever the user changes the selected vehicle item in the ListBox
        // Parameters: sender (the object that raised the event), e (event arguments containing selection details)
        private void VehicleListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // Check if the currently selected item in the ListBox is successfully castable to a Vehicle object
            if (VehicleListBox.SelectedItem is Vehicle selectedVehicle)
            {
                // Update the UI header text block with the make and model of the selected vehicle
                SelectedVehicleText.Text = $"{selectedVehicle.Make} {selectedVehicle.Model}";

                // Update the UI text block to display the current odometer reading in kilometers
                OdometerText.Text = $"Läbisõit: {selectedVehicle.Odometer} km";

                // Enable or disable buttons polymorphically using interfaces (true if supported, false otherwise)
                DriveButton.IsEnabled = selectedVehicle is IDriveable;
                SwimButton.IsEnabled = selectedVehicle is ISwimmable;
            }
            else // Executed when no vehicle is selected or the selection is cleared
            {
                // Reset the title text block back to the default instruction prompt
                SelectedVehicleText.Text = "Vali sõiduk nimekirjast";

                // Reset the odometer display text back to zero kilometers
                OdometerText.Text = "Läbisõit: 0 km";

                // Force disable the drive button since no valid vehicle is active
                DriveButton.IsEnabled = false;

                // Force disable the swim button since no valid vehicle is active
                SwimButton.IsEnabled = false;
            }
        }

        // Event handler triggered when the Drive button is clicked by the user
        // Parameters: sender (the button clicked), e (routed event arguments)
        private void DriveButton_Click(object sender, RoutedEventArgs e)
        {
            // Verify that the selected item implements IDriveable and can also be referenced as a Vehicle
            if (VehicleListBox.SelectedItem is IDriveable driveable && VehicleListBox.SelectedItem is Vehicle vehicle)
            {
                try // Start a try block to safely catch validation exceptions from the Core layer
                {
                    // Attempt to parse the text box input string into a valid double numeric value (distance in km)
                    if (double.TryParse(DistanceTextBox.Text, out double km))
                    {
                        // Invoke the polymorphic Drive method, returning a status message string
                        string message = driveable.Drive(km);

                        // Append the operation status message to the bottom activity log text box
                        LogTextBox.AppendText(message + "\n");

                        // Refresh the odometer text display on the UI with the updated vehicle mileage
                        OdometerText.Text = $"Läbisõit: {vehicle.Odometer} km";

                        // Force the ListBox control to refresh its item presentation bindings
                        VehicleListBox.Items.Refresh();
                    }
                    else // Executed if the user typed text that is not a valid number
                    {
                        // Show a warning message box alerting the user about invalid numerical input
                        MessageBox.Show("Vigane sisend! Palun sisesta number.", "Viga", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                catch (System.ArgumentException ex) // Catches business logic errors (e.g., negative or zero distance)
                {
                    // Display an error message box containing the validation exception description text
                    MessageBox.Show(ex.Message, "Valideerimisviga", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        // Event handler triggered when the Swim button is clicked by the user
        // Parameters: sender (the button clicked), e (routed event arguments)
        private void SwimButton_Click(object sender, RoutedEventArgs e)
        {
            // Verify that the selected item implements ISwimmable and can also be referenced as a Vehicle
            if (VehicleListBox.SelectedItem is ISwimmable swimmable && VehicleListBox.SelectedItem is Vehicle vehicle)
            {
                try // Start a try block to handle potential business logic exceptions safely
                {
                    // Attempt to parse the distance text input box value into a double type
                    if (double.TryParse(DistanceTextBox.Text, out double km))
                    {
                        // Invoke the polymorphic Swim method, returning a descriptive result message string
                        string message = swimmable.Swim(km);

                        // Append the successful swim operation log message into the activity log box
                        LogTextBox.AppendText(message + "\n");

                        // Update the odometer label text to reflect the new increased mileage value
                        OdometerText.Text = $"Läbisõit: {vehicle.Odometer} km";

                        // Refresh the visual container items inside the vehicle list box
                        VehicleListBox.Items.Refresh();
                    }
                    else // Executed if string parsing into double fails
                    {
                        // Show a warning popup window prompting the user to enter a correct number format
                        MessageBox.Show("Vigane sisend! Palun sisesta number.", "Viga", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                catch (System.ArgumentException ex) // Catch invalid argument exceptions coming from the core model
                {
                    // Show a critical error message box showing why the validation failed
                    MessageBox.Show(ex.Message, "Valideerimisviga", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        // Event handler triggered when the Add Car button is clicked
        // Parameters: sender (the button clicked), e (routed event arguments)
        private void AddCarButton_Click(object sender, RoutedEventArgs e)
        {
            // Create a new Car instance with preset parameters and add it to the observable collection
            Vehicles.Add(new Car("Toyota", "RAV4", 0));
        }

        // Event handler triggered when the Add Boat button is clicked
        // Parameters: sender (the button clicked), e (routed event arguments)
        private void AddBoatButton_Click(object sender, RoutedEventArgs e)
        {
            // Create a new Boat instance with preset parameters and add it to the observable collection
            Vehicles.Add(new Boat("Yamaha", "Breeze", 0));
        }

        // Event handler triggered when the Add Amphibious Car button is clicked
        // Parameters: sender (the button clicked), e (routed event arguments)
        private void AddAmphibiousButton_Click(object sender, RoutedEventArgs e)
        {
            // Create a new AmphibiousCar instance with preset parameters and add it to the collection
            Vehicles.Add(new AmphibiousCar("Gibbs", "Aquada", 0));
        }

        // Event handler triggered when the Remove button is clicked to delete a vehicle
        // Parameters: sender (the button clicked), e (routed event arguments)
        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            // Check if an active vehicle object is currently highlighted/selected in the list box
            if (VehicleListBox.SelectedItem is Vehicle selectedVehicle)
            {
                // Remove the target vehicle object from the main observable collection
                Vehicles.Remove(selectedVehicle);

                // Write a removal confirmation entry note into the activity log text box
                LogTextBox.AppendText($"Eemaldatud: {selectedVehicle.Make} {selectedVehicle.Model}\n");
            }
        }
    }
}