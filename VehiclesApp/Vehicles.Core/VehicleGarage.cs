using System;
using System.Collections.ObjectModel;
using Vehicles.Core.Models;
using Vehicles.Core.Resources;

namespace Vehicles.Core
{
    // Garage class to store and manage vehicles
    public class VehicleGarage
    {
        // List of vehicles in this garage, updates UI automatically
        public ObservableCollection<Vehicle> Vehicles { get; } = new();

        // Event to send log messages to the main window
        public event Action<string> OnLogMessage;

        // Add a new vehicle to the garage
        public void AddVehicle(Vehicle vehicle)
        {
            // Check all cars already in the garage
            foreach (var existingVehicle in Vehicles)
            {
                // Create a message that the old car honks at the new one
                string honkMessage = string.Format(AppMessages.Log_VehicleHonks, existingVehicle.Make, existingVehicle.Model, vehicle.Make, vehicle.Model);

                // Send the message to the log
                OnLogMessage?.Invoke(honkMessage);
            }

            // Finally, add the new vehicle to the list
            Vehicles.Add(vehicle);
        }

        // Remove a vehicle from the garage
        public void RemoveVehicle(Vehicle vehicle)
        {
            // Make sure the vehicle is actually in the garage
            if (Vehicles.Contains(vehicle))
            {
                // Remove it from the list
                Vehicles.Remove(vehicle);

                // Check all cars left in the garage
                foreach (var remainingVehicle in Vehicles)
                {
                    // Create a message that the remaining car flashes headlights
                    string flashMessage = string.Format(AppMessages.Log_VehicleFlashes, remainingVehicle.Make, remainingVehicle.Model, vehicle.Make, vehicle.Model);

                    // Send the message to the log
                    OnLogMessage?.Invoke(flashMessage);
                }
            }
        }
    }
}