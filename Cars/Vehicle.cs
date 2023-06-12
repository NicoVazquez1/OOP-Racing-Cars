﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Relaciones_entre_clases.Objects;

namespace Relaciones_entre_clases.Cars
{
    public abstract class Vehicle
    {
        public int X { get; set; } = 1;
        public int Y { get; set; }
        public int Identifier { get; set; }
        float speed;
        public float Speed
        {
            get => speed;
            set => speed = Math.Max(Math.Min(value, 100), 0.0f);
        }
        public virtual void Accelerate()
        {
            
        }
    }
}
