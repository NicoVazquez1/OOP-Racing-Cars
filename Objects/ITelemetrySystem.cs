using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relaciones_entre_clases.Objects
{
    interface ITelemetrySystem
    {
        string GetTelemetryData(float battery, float time, int distance);
    }
}
