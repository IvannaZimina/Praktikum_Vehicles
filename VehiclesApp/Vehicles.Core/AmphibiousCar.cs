using System;
using Vehicles.Core.Interfaces;

namespace Vehicles.Core
{
    // Multiple interfaces: C# doesn't allow multiple class inheritance (no 'class X : Car, Boat'), 
    // but a class can implement as many interfaces as needed.
    // AmphibiousCar inherits from Vehicle and implements BOTH interfaces: IDriveable and ISwimmable
    public class AmphibiousCar : Vehicle, IDriveable, ISwimmable
    {
        // Constructor accepting make, model, and optional initial odometer (defaults to 0)
        public AmphibiousCar(string make, string model, double odometer = 0) : base(make, model, odometer)
        {
        }

        // Implementation of the abstract Move method from Vehicle
        public override string Move(double km)
        {
            // Validation: distance must be greater than zero
            if (km <= 0)
            {
                throw new ArgumentException("Distance must be greater than zero.");
            }

            // Increase the odometer
            Odometer += km;

            // Return a status message for an amphibious vehicle
            return $"Amphibious Car {Make} {Model} traveled {km} km (by land and water). Total odometer: {Odometer} km.";
        }
    }
}