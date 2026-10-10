using System;
using Vehicles.Core.Interfaces;
using Vehicles.Core.Resources;

namespace Vehicles.Core.Models
{
    // Car inherits from Vehicle and implements IDriveable
    public class Car : Vehicle, IDriveable
    {
        // Constructor accepting make, model, and optional initial odometer (defaults to 0)
        public Car(string make, string model, double odometer = 0) : base(make, model, odometer)
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
            return string.Format(AppMessages.Log_CarDrove, Make, Model, km, Odometer);
        }

        // Implementation of the abstract Move method from Vehicle (delegates to Drive)
        public override string Move(double km)
        {
            return Drive(km);
        }
    }
}