using System;
using Vehicles.Core.Interfaces;
using Vehicles.Core.Resources;

namespace Vehicles.Core.Models
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

        // Implementation of IDriveable interface method
        public string Drive(double km)
        {
            if (km <= 0)
            {
                throw new ArgumentException(AppMessages.Error_Validation_DistanceMustBePositive);
            }

            Odometer += km;
            return string.Format(AppMessages.Log_AmphibiousDrove, Make, Model, km, Odometer);
        }

        // Implementation of ISwimmable interface method
        public string Swim(double km)
        {
            if (km <= 0)
            {
                throw new ArgumentException(AppMessages.Error_Validation_DistanceMustBePositive);
            }

            Odometer += km;
            return string.Format(AppMessages.Log_AmphibiousSwam, Make, Model, km, Odometer);
        }

        // Implementation of the abstract Move method from Vehicle (defaults to driving on land)
        public override string Move(double km)
        {
            return Drive(km);
        }
    }
}