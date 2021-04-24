using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Units.CodeGeneration.Models
{
    internal class Unit
    {
        public BaseUnits BaseUnits;
        public string FromBaseToUnitFunc;
        public string FromUnitToBaseFunc;
        public Localization[] Localization = Array.Empty<Localization>();
        public string PluralName;
        public Prefix[] Prefixes = Array.Empty<Prefix>();
        public string SingularName;
        public string XmlDocRemarks;
        public string XmlDocSummary;
        public string ObsoleteText;
    }
}
