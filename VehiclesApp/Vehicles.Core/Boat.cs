using System;
using Vehicles.Core.Interfaces;

namespace Vehicles.Core
{
    // Boat inherits from Vehicle and implements ISwimmable
    public class Boat : Vehicle, ISwimmable
    {
        // Constructor accepting make, model, and optional initial odometer (defaults to 0)
        public Boat(string make, string model, double odometer = 0) : base(make, model, odometer)
        {
        }

        // Implementation of ISwimmable interface method
        public string Swim(double km)
        {
            if (km <= 0)
            {
                throw new ArgumentException("Distance must be greater than zero.");
            }

            Odometer += km;
            return $"Boat {Make} {Model} sailed {km} km. Total odometer: {Odometer} km.";
        }

        // Implementation of the abstract Move method from Vehicle (delegates to Swim)
        public override string Move(double km)
        {
            return Swim(km);
        }
    }
}