using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLibrary.ConceptSamples
{
    public class BaseChildConversion
    {
        private abstract class Bike
        {
            public string Brand { get; set; }
            public float Price { get; set; }
            public bool Geared { get; set; }
            public virtual float MaxSpeed
            {
                get
                {
                    return GetMaxSpeed();
                }
            }
            private float GetMaxSpeed()
            {
                //Calculate
                return 1;
            }
        }

        private class MountainBike : Bike
        {
            public int GearCount { get; set; }
            public override float MaxSpeed
            {
                get
                {
                    //Calculate
                    return base.MaxSpeed * GearCount;
                }
            }
        }

        private class KidsBike : Bike
        {
            public int SuitableTillAge { get; set; }
            public override float MaxSpeed
            {
                get
                {
                    return 0;
                }
            }

            public static implicit operator KidsBike(MountainBike bike)
            {
                return new KidsBike() { Brand = bike.Brand, Geared = false, Price = bike.Price, SuitableTillAge = 10 };
            }
        }

        public void BaseChildVerify()
        {
            var mountainBike = new MountainBike() { Brand = "BTwin", GearCount = 5, Geared = true, Price = 15000 };
            var bike = mountainBike as Bike;
            KidsBike kidsBike = mountainBike;
        }
    }
}
