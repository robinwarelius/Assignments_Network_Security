using IoT_Unit.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT_Unit.Utilities
{
    public static class CreateUnit
    {
        public static Unit Create(string name, string description, string temperature)
        {
            return new Unit()
            {
                Name = name,
                Description = description,
                Temperature = temperature
            };
        }
    }
}
