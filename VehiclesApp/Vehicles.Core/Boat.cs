using System;
using Vehicles.Core.Interfaces;
using Vehicles.Core.Resources;

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
                throw new ArgumentException(AppMessages.Error_Validation_DistanceMustBePositive);
            }

            Odometer += km;
            return string.Format(AppMessages.Log_BoatSailed, Make, Model, km, Odometer);
        }

        // Implementation of the abstract Move method from Vehicle (delegates to Swim)
        public override string Move(double km)
        {
            return Swim(km);
        }
    }
}