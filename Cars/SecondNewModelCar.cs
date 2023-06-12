using Relaciones_entre_clases.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relaciones_entre_clases.Cars
{
    //INHERITANCE
    public class SecondNewModelCar : ClassicModelCar, ITelemetrySystem
    {
        protected override float Mass { get; set; } = 580;
        public bool TurboModeEnabled { get; set; }
        private readonly string fileModel_path = "C:\\Users\\Nico\\source\\repos\\Relaciones entre clases\\SecondNewModel.txt";
        private readonly string fileModelTurboMode_path = "C:\\Users\\Nico\\source\\repos\\Relaciones entre clases\\SecondNewModel_Turbo.txt";
        public SecondNewModelCar(int enginesNum, Battery battery) : base(enginesNum, battery)
        {
            Chassis = File.ReadAllText(fileModel_path);
        }
        public void ActivateTurboMode()
        {
            TurboModeEnabled = true;
            Console.WriteLine("Turbo mode activated!");
            
        }

        public void DeactivateTurboMode()
        {
            TurboModeEnabled = false;
            Console.WriteLine("Turbo mode deactivated.");
        }

        //override
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
                    if (TurboModeEnabled)
                    {
                        acceleration *= 1.7f;
                        Battery.Level -= engine.UpdateBatteryConsume() * 1.1f;
                        Chassis = File.ReadAllText(fileModelTurboMode_path);
                    }
                    acceleration += engine.Accelerate(Weight);
                    Battery.Level -= engine.UpdateBatteryConsume();
                    Weight += Speed / 5;
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

        //IMPLEMENTATION
        public string GetTelemetryData(float batteryPercent, float time, int distance)
        {
            if (TurboModeEnabled)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
              
            string batteryVisualization = "Battery: ";

            for (int j = 0; j < batteryPercent; j += 4)
                batteryVisualization += "O";

            string telemetryData = $"    Time: {time}\n" +
                                   $"    Distance: {distance}\n" +
                                   $"    {batteryVisualization} {batteryPercent}%\n" +
                                   $"    Speed: {Speed.ToString("0")}\n\n" +
                                   $"    Boosters: {Boosters.Length}\n" +
                                   $"    Battery: {Battery.Name}\n" +
                                   $"    Turbo mode active: {TurboModeEnabled}";

            return telemetryData;
        }
    }
}
