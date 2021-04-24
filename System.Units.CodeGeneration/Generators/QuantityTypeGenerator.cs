using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Units.CodeGeneration.Models;

namespace System.Units.CodeGeneration.Generators
{
    internal class QuantityTypeGenerator : GeneratorBase
    {
        private readonly Quantity[] _quantities;

        public QuantityTypeGenerator(Quantity[] quantities)
        {
            _quantities = quantities;
        }

        public override string Generate()
        {
            Writer.WL(GeneratedFileHeader);
            Writer.WL(@"
namespace System.Units
{
    /// <summary>
    ///     Lists all generated quantities with the same name as the quantity struct type,
    ///     such as Length, Mass, Force etc.
    ///     This is useful for populating options in the UI, such as creating a generic conversion
    ///     tool with inputValue, quantityName, fromUnit and toUnit selectors.
    /// </summary>
    public enum QuantityType
    {
        Undefined = 0,");
            foreach (var quantity in _quantities)
                Writer.WL($@"
        {quantity.Name},");
            Writer.WL(@"
    }
}");
            return Writer.ToString();
        }
    }
}
