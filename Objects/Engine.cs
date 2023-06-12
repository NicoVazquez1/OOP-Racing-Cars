using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relaciones_entre_clases.Objects
{
    public class Booster
    {
        private float strength = 950;
        public bool On { get; set; }
        public float Consume = 3;
        public float mass = 20;
        public void Star() => On = true;
        public void Stop() => On = false;
        public float Accelerate(float weight)
        {
            if (On) return strength / weight;
            else return 0;
        }

        public float UpdateBatteryConsume() {
            if (On) return Consume;
            else return 0;
        }

    }

}
