using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Units.CodeGeneration.Models
{
    internal class Quantity
    {
        public BaseDimensions BaseDimensions = new BaseDimensions(); // Default to empty
        public string BaseType = "double"; // TODO Rename to ValueType
        public string BaseUnit; // TODO Rename to DefaultUnit or IntermediateConversionUnit to avoid confusion with Unit.BaseUnits
        public bool GenerateArithmetic = true;
        public bool Logarithmic = false;
        public int LogarithmicScalingFactor = 1;
        public string Name;
        public Unit[] Units = Array.Empty<Unit>();
        public string XmlDocRemarks;
        public string XmlDoc;
        public string ObsoleteText;
    }
}
