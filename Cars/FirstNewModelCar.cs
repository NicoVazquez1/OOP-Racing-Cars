using Relaciones_entre_clases.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relaciones_entre_clases.Cars
{
    //INHERITANCE
    public class FirstNewModelCar : ClassicModelCar, ITelemetrySystem
    {
        protected override float Mass { get; set; } = 300;
        readonly string fileModel_path = "C:\\Users\\Nico\\source\\repos\\Relaciones entre clases\\FirstNewModel.txt";
        public FirstNewModelCar(int enginesNum, Battery battery) : base(enginesNum, battery)
        {
            Chassis = File.ReadAllText(fileModel_path);
        }

        //IMPLEMENTATION
        public string GetTelemetryData(float batteryPercent, int time, int distance)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            string batteryVisualization = "";

            if (batteryPercent <= 35)
            {
                batteryVisualization += "\u001b[31m"; 
            }
            else if (batteryPercent <= 70)
            {
                batteryVisualization += "\u001b[33m"; 
            }
            else
            {
                batteryVisualization += "\u001b[32m"; 
            }


            for (int j = 0; j < batteryPercent; j += 5)
                batteryVisualization += "■";

            string telemetryData = $"     -------------------------------------------------------------- \n" +
                                   $"    Time           |{time}               \n" +
                                   $"    Distance       |{distance}           \n" +
                                   $"    Battery        |{batteryVisualization} {batteryPercent}%\n\u001b[36m" +
                                   $"    Speed          |{Speed.ToString("0")}\n" +
                                   $"    Boosters       |{Boosters.Length}    \n" +
                                   $"    Battery model  |     {Battery.Name}  \n" +
                                   $"     -------------------------------------------------------------- \n";
                                  

            return telemetryData;
        }
    }
}
