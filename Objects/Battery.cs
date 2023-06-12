using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relaciones_entre_clases.Objects
{
    public class Battery
    {
        public float Capacity { get; set; } = 200;
        public float Mass { get; set; }
        public string Name { get; set; }
        float level;
        public float Level
        {
            get => level;
            set => level = Math.Max(Math.Min(value, Capacity), 0);
        }
        public Battery(float bonus, string name) {
            Name = name;
            Capacity += bonus;
            level = Capacity;
            Mass += level / 5;
        }
    }
}
