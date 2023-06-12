using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relaciones_entre_clases.Objects
{
    public class Charger
    {
        protected float ChargeSpeed { get; set; }
        public float Uses { get; set; }
        public Charger(float speed, int uses)
        {
            ChargeSpeed = speed;
            Uses = uses;
        }
        public virtual float RechargeBattery() => ChargeSpeed;
    }
}
