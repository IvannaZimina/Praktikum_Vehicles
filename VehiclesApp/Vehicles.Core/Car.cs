using System;
using Vehicles.Core.Interfaces;

namespace Vehicles.Core
{
    // Car inherits from Vehicle (gets Make, Model, Odometer) and implements IDriveable (represents the driving role)
    public class Car : Vehicle, IDriveable
    {
        // Constructor that passes make and model up to the base Vehicle class
        // [: base(make, model)] — calls the constructor of the Vehicle base class to immediately check the make and model for errors and record them.
        public Car(string make, string model) : base(make, model)
        {
        }

        // Implementation of the abstract Move method from Vehicle
        // [override] - indicates that we take the abstract empty Move method from Vehicle and write specific car-specific logic for it.
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