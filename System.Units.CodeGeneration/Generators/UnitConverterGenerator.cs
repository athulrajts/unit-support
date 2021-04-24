using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Units.CodeGeneration.Models;

namespace System.Units.CodeGeneration.Generators
{
    internal class UnitConverterGenerator : GeneratorBase
    {
        private readonly Quantity[] _quantities;

        public UnitConverterGenerator(Quantity[] quantities)
        {
            _quantities = quantities;
        }

        public override string Generate()
        {
            Writer.WL(GeneratedFileHeader);
            Writer.WL($@"

namespace System.Units
{{
    public sealed partial class UnitConverter
    {{
        /// <summary>
        /// Registers the default conversion functions in the given <see cref=""UnitConverter""/> instance.
        /// </summary>
        /// <param name=""unitConverter"">The <see cref=""UnitConverter""/> to register the default conversion functions in.</param>
        public static void RegisterDefaultConversions(UnitConverter unitConverter)
        {{");
            foreach (Quantity quantity in _quantities)
                foreach (Unit unit in quantity.Units)
                {
                    Writer.WL(quantity.BaseUnit == unit.SingularName
                        ? $@"
            unitConverter.SetConversionFunction<{quantity.Name}>({quantity.Name}.BaseUnit, {quantity.Name}.BaseUnit, q => q);"
                        : $@"
            unitConverter.SetConversionFunction<{quantity.Name}>({quantity.Name}.BaseUnit, {quantity.Name}Unit.{unit.SingularName}, q => q.ToUnit({quantity.Name}Unit.{unit.SingularName}));
            unitConverter.SetConversionFunction<{quantity.Name}>({quantity.Name}Unit.{unit.SingularName}, {quantity.Name}.BaseUnit, q => q.ToBaseUnit());");
                }

            Writer.WL($@"
        }}
    }}
}}");

            return Writer.ToString();
        }
    }
}
