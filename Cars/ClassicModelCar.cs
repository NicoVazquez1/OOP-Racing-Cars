using Relaciones_entre_clases.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relaciones_entre_clases.Cars
{
    //INHERITANCE - ABSTRACT 
    public class ClassicModelCar : Vehicle
    {
        public Booster[] Boosters { get; set; }
        public Battery Battery { get; set; }
        #region
        protected virtual float Mass { get; set; } = 640;
        public float Weight { get; set; }
        readonly string fileModel_path = "C:\\Users\\Nico\\source\\repos\\Relaciones entre clases\\ClassicModel.txt";
        public string Chassis { get; set; }
        #endregion

        public ClassicModelCar(int boostersNum, Battery battery)
        {
            //COMPOSITION
            if (boostersNum > 1 && boostersNum <= 4)
            {
                Boosters = new Booster[boostersNum];
                for (int i = 0; i < boostersNum; i++)
                    Boosters[i] = new Booster();
            }
            else
                Boosters = new Booster[] { new Booster() };

            //AGGREGATION
            Battery = battery;

            //some phisics
            Mass += Boosters.Sum(booster => booster.mass);
            Mass += battery.Mass;
            Weight = Mass;

            Chassis = File.ReadAllText(fileModel_path);
        }

        //DEPENDENCY
        public void StartEngines()
        {
            foreach (var engine in Boosters)
                engine.Star();
        }

        //ALSO DEPENDENCY
        public override void Accelerate()
        {
            if (Battery.Level <= 0)
            {
                Console.WriteLine("No battery, please recharge your battery");
                return;
            }

            if (Battery.Level < Battery.Capacity * 0.25)
                Console.WriteLine("Battery level low, please recharge soon");

            float acceleration = 0;
            float distance;

            foreach (var engine in Boosters)
            {
                if (engine.On)
                {
                    acceleration += engine.Accelerate(Weight);
                    Battery.Level -= engine.UpdateBatteryConsume();
                    Weight += Speed;
                }
                else
                {
                    Weight = Mass;
                    Speed = 0;
                }
            }
            Speed += acceleration;
            distance = Speed / 10;
            X += (int)distance;
        }

        //ASSOCIATION
        public void RechargeBattery(Charger charger)
        {
            Battery.Level += charger.RechargeBattery();
        }

        public void StopEngines()
        {
            foreach (var engine in Boosters)
                engine.Stop();
        }
    }
}
