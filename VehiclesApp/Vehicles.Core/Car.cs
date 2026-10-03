using System;
using Vehicles.Core.Interfaces;

namespace Vehicles.Core
{
    // Car inherits from Vehicle (gets Make, Model, Odometer) and implements IDriveable (represents the driving role)
    public class Car : Vehicle, IDriveable
    {
        // Constructor accepting make, model, and optional initial odometer (defaults to 0)
        public Car(string make, string model, double odometer = 0) : base(make, model, odometer)
        {
        }

        // Implementation of the abstract Move method from Vehicle
        public override string Move(double km)
        {
            // Validation: mileage cannot be changed with invalid input (km must be greater than 0)
            if (km <= 0)
            {
                throw new ArgumentException("Distance must be greater than zero.");
            }

            // Increase the odometer
            Odometer += km;

            // Return a status message about the car's trip
            return $"Car {Make} {Model} drove {km} km. Total odometer: {Odometer} km.";
        }
    }
}