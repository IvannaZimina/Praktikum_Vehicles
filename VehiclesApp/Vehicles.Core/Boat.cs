using System;
using Vehicles.Core.Interfaces;

namespace Vehicles.Core
{
    // Boat inherits from Vehicle and implements ISwimmable (represents the swimming/water role)
    public class Boat : Vehicle, ISwimmable
    {
        // Constructor accepting make, model, and optional initial odometer (defaults to 0)
        public Boat(string make, string model, double odometer = 0) : base(make, model, odometer)
        {
        }

        // Implementation of the abstract Move method specifically for a boat
        public override string Move(double km)
        {
            // Validation: distance must be greater than zero
            if (km <= 0)
            {
                throw new ArgumentException("Distance must be greater than zero.");
            }

            // Increase the odometer
            Odometer += km;

            // Return a status message about the boat's trip
            return $"Boat {Make} {Model} sailed {km} km. Total odometer: {Odometer} km.";
        }
    }
}