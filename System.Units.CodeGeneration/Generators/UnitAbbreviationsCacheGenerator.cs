using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Units.CodeGeneration.Models;

namespace System.Units.CodeGeneration.Generators
{
    internal class UnitAbbreviationsCacheGenerator : GeneratorBase
    {
        private readonly Quantity[] _quantities;

        public UnitAbbreviationsCacheGenerator(Quantity[] quantities)
        {
            _quantities = quantities;
        }

        public override string Generate()
        {
            Writer.WL(GeneratedFileHeader);
            Writer.WL(@"
using System;

namespace System.Units
{
    public partial class UnitAbbreviationsCache
    {
        private static readonly (string CultureName, Type UnitType, int UnitValue, string[] UnitAbbreviations)[] GeneratedLocalizations
            = new []
            {");
            foreach (var quantity in _quantities)
            {
                var unitEnumName = $"{quantity.Name}Unit";

                foreach (var unit in quantity.Units)
                {
                    foreach (var localization in unit.Localization)
                    {
                        var cultureName = localization.Culture;

                        // All units must have a unit abbreviation, so fallback to "" for units with no abbreviations defined in JSON
                        var abbreviationParams = localization.Abbreviations.Any()
                            ? string.Join(", ", localization.Abbreviations.Select(abbr => $"\"{abbr}\""))
                            : "\"\"";
                        Writer.WL($@"
                (""{cultureName}"", typeof({unitEnumName}), (int){unitEnumName}.{unit.SingularName}, new string[]{{{abbreviationParams}}}),");
                    }
                }
            }

            Writer.WL(@"
            };
    }
}");
            return Writer.ToString();
        }
    }
}
