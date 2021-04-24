using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Units.CodeGeneration.Models;

namespace System.Units.CodeGeneration.Generators
{
    internal class UnitTypeGenerator : GeneratorBase
    {
        private readonly Quantity _quantity;
        private readonly string _unitEnumName;

        public UnitTypeGenerator(Quantity quantity)
        {
            _quantity = quantity;
            _unitEnumName = $"{quantity.Name}Unit";
        }

        public override string Generate()
        {
            Writer.WL(GeneratedFileHeader);
            Writer.WL($@"
namespace System.Units
{{
    public enum {_unitEnumName}
    {{
        Undefined = 0,");

            foreach (var unit in _quantity.Units)
            {
                if (unit.XmlDocSummary.HasText())
                {
                    Writer.WL();
                    Writer.WL($@"
        /// <summary>
        ///     {unit.XmlDocSummary}
        /// </summary>");
                }

                if (unit.XmlDocRemarks.HasText())
                {
                    Writer.WL($@"
        /// <remarks>{unit.XmlDocRemarks}</remarks>");
                }

                Writer.WLIfText(2, QuantityGenerator.GetObsoleteAttributeOrNull(unit));
                Writer.WL($@"
        {unit.SingularName},");
            }

            Writer.WL($@"
    }}
}}");
            return Writer.ToString();
        }
    }
}
