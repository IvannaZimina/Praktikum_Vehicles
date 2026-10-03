using System;

namespace Vehicles.Core
{
    // abstract class serves as a template for child classes (Car, Boat, etc.)
    // Why abstract? Because a generic "Vehicle" is just a concept, not a real thing.
    // This prevents creating useless "empty" objects using new Vehicle() — only specific things like Cars or Boats are allowed!
    public abstract class Vehicle
    {
        // Properties Make and Model — publicly readable (get), 
        // but can only be set during object creation or inside the class (protected set)
        public string Make { get; }
        public string Model { get; }

        // Odometer (mileage) — readable from the outside, but can only be modified from within the classes
        public double Odometer { get; protected set; }

        // Base class constructor with validation (checking input data) and optional initial odometer
        protected Vehicle(string make, string model, double odometer = 0)
        {
            if (string.IsNullOrWhiteSpace(make) || string.IsNullOrWhiteSpace(model))
            {
                throw new ArgumentException("Make and model cannot be empty.");
            }

            if (odometer < 0)
            {
                throw new ArgumentException("Odometer cannot be negative.");
            }

            Make = make.Trim();
            Model = model.Trim();
            Odometer = odometer; // Initial or pre-existing mileage
        }

        // abstract method Move — a polymorphic method. 
        // It has no body here because each specific vehicle (car, boat) 
        // will implement its own movement logic using 'override'.
        public abstract string Move(double km);

        // Override ToString to nicely display vehicle info in the UI ListBox
        public override string ToString()
        {
            return $"{Make} {Model} ({Odometer} km)";
        }
    }
}