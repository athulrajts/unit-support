using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Units.CodeGeneration.Models;

namespace System.Units.CodeGeneration.Generators
{
    internal static class UnitsNetGenerator
    {
        private const int AlignPad = 35;

        /// <summary>
        ///     Generate source code for UnitsNet project for the given parsed quantities.
        ///     Outputs files relative to the given root dir to these locations:
        ///     <list type="bullet">
        ///         <item>
        ///             <description>UnitsNet/GeneratedCode (quantity and unit types, Quantity, UnitAbbreviationCache)</description>
        ///         </item>
        ///         <item>
        ///             <description>UnitsNet.Tests/GeneratedCode (tests)</description>
        ///         </item>
        ///         <item>
        ///             <description>UnitsNet.Tests/CustomCode (test stubs, one for each quantity if not already created)</description>
        ///         </item>
        ///     </list>
        /// </summary>
        /// <param name="rootDir">Path to repository root directory.</param>
        /// <param name="quantities">The parsed quantities.</param>
        public static void Generate(string rootDir, Quantity[] quantities)
        {
            var quantitiesDir = $"{rootDir}/System.Units/Quantities/Generated";
            var unitTypeDir = $"{rootDir}/System.Units/Units/Generated";
            var abbrCache = $"{rootDir}/System.Units/Abbreviations/Generated";
            var rootGeneratedDir = $"{rootDir}/System.Units/Generated";
            var testProjectDir = $"{rootDir}/System.Units.Tests/";

            // Ensure output directories exist
            Directory.CreateDirectory(quantitiesDir);
            Directory.CreateDirectory(unitTypeDir);
            Directory.CreateDirectory(abbrCache);
            Directory.CreateDirectory(rootGeneratedDir);
            Directory.CreateDirectory($"{testProjectDir}/Quantities/Generated");
            //Directory.CreateDirectory($"{extensionsOutputDir}");
            //Directory.CreateDirectory($"{extensionsTestOutputDir}");
            //Directory.CreateDirectory($"{testProjectDir}/Generated");
            //Directory.CreateDirectory($"{testProjectDir}/Generated/TestsBase");
            //Directory.CreateDirectory($"{testProjectDir}/Generated/QuantityTests");

            foreach (var quantity in quantities)
            {
                var sb = new StringBuilder($"{quantity.Name}:".PadRight(AlignPad));
                
                GenerateQuantity(sb, quantity, $"{quantitiesDir}/{quantity.Name}.g.cs");
                GenerateUnitType(sb, quantity, $"{unitTypeDir}/{quantity.Name}Unit.g.cs");
               
                //GenerateNumberToExtensions(sb, quantity, $"{extensionsOutputDir}/NumberTo{quantity.Name}Extensions.g.cs");
                //GenerateNumberToExtensionsTestClass(sb, quantity, $"{extensionsTestOutputDir}/NumberTo{quantity.Name}ExtensionsTest.g.cs");

                // Example: CustomCode/Quantities/LengthTests inherits GeneratedCode/TestsBase/LengthTestsBase
                // This way when new units are added to the quantity JSON definition, we auto-generate the new
                // conversion function tests that needs to be manually implemented by the developer to fix the compile error
                // so it cannot be forgotten.
                GenerateQuantityTestBaseClass(sb, quantity, $"{testProjectDir}/Quantities/Generated/{quantity.Name}TestsBase.g.cs");
                //GenerateQuantityTestClassIfNotExists(sb, quantity, $"{testProjectDir}/CustomCode/{quantity.Name}Tests.cs");

                Log.Information(sb.ToString());
            }

            //GenerateIQuantityTests(quantities, $"{testProjectDir}/GeneratedCode/IQuantityTests.g.cs");

            //Log.Information("");
            GenerateUnitAbbreviationsCache(quantities, $"{abbrCache}/UnitAbbreviationsCache.g.cs");
            GenerateQuantityType(quantities, $"{rootGeneratedDir}/QuantityType.g.cs");
            GenerateStaticQuantity(quantities, $"{rootGeneratedDir}/Quantity.g.cs");
            GenerateUnitConverter(quantities, $"{rootGeneratedDir}/UnitConverter.g.cs");

            var unitCount = quantities.SelectMany(q => q.Units).Count();
            Log.Information("");
            Log.Information($"Total of {unitCount} units and {quantities.Length} quantities.");
            Log.Information("");
        }

        //private static void GenerateQuantityTestClassIfNotExists(StringBuilder sb, Quantity quantity, string filePath)
        //{
        //    if (File.Exists(filePath))
        //    {
        //        sb.Append("test stub(skip) ");
        //        return;
        //    }

        //    var content = new UnitTestStubGenerator(quantity).Generate();
        //    File.WriteAllText(filePath, content, Encoding.UTF8);
        //    sb.Append("test stub(OK) ");
        //}

        private static void GenerateQuantity(StringBuilder sb, Quantity quantity, string filePath)
        {
            var content = new QuantityGenerator(quantity).Generate();
            File.WriteAllText(filePath, content, Encoding.UTF8);
            sb.Append("quantity(OK) ");
        }

        //private static void GenerateNumberToExtensions(StringBuilder sb, Quantity quantity, string filePath)
        //{
        //    var content = new NumberExtensionsGenerator(quantity).Generate();
        //    File.WriteAllText(filePath, content, Encoding.UTF8);
        //    sb.Append("number extensions(OK) ");
        //}

        //private static void GenerateNumberToExtensionsTestClass(StringBuilder sb, Quantity quantity, string filePath)
        //{
        //    var content = new NumberExtensionsTestClassGenerator(quantity).Generate();
        //    File.WriteAllText(filePath, content, Encoding.UTF8);
        //    sb.Append("number extensions tests(OK) ");
        //}

        private static void GenerateUnitType(StringBuilder sb, Quantity quantity, string filePath)
        {
            var content = new UnitTypeGenerator(quantity).Generate();
            File.WriteAllText(filePath, content, Encoding.UTF8);
            sb.Append("unit(OK) ");
        }

        private static void GenerateQuantityTestBaseClass(StringBuilder sb, Quantity quantity, string filePath)
        {
            var content = new UnitTestBaseClassGenerator(quantity).Generate();
            File.WriteAllText(filePath, content, Encoding.UTF8);
            sb.Append("test base(OK) ");
        }

        //private static void GenerateIQuantityTests(Quantity[] quantities, string filePath)
        //{
        //    var content = new IQuantityTestClassGenerator(quantities).Generate();
        //    File.WriteAllText(filePath, content, Encoding.UTF8);
        //    Log.Information("IQuantityTests.g.cs: ".PadRight(AlignPad) + "(OK)");
        //}

        private static void GenerateUnitAbbreviationsCache(Quantity[] quantities, string filePath)
        {
            var content = new UnitAbbreviationsCacheGenerator(quantities).Generate();
            File.WriteAllText(filePath, content, Encoding.UTF8);
            Log.Information("UnitAbbreviationsCache.g.cs: ".PadRight(AlignPad) + "(OK)");
        }

        private static void GenerateQuantityType(Quantity[] quantities, string filePath)
        {
            var content = new QuantityTypeGenerator(quantities).Generate();
            File.WriteAllText(filePath, content, Encoding.UTF8);
            Log.Information("QuantityType.g.cs: ".PadRight(AlignPad) + "(OK)");
        }

        private static void GenerateStaticQuantity(Quantity[] quantities, string filePath)
        {
            var content = new StaticQuantityGenerator(quantities).Generate();
            File.WriteAllText(filePath, content, Encoding.UTF8);
            Log.Information("Quantity.g.cs: ".PadRight(AlignPad) + "(OK)");
        }

        private static void GenerateUnitConverter(Quantity[] quantities, string filePath)
        {
            var content = new UnitConverterGenerator(quantities).Generate();
            File.WriteAllText(filePath, content, Encoding.UTF8);
            Log.Information("UnitConverter.g.cs: ".PadRight(AlignPad) + "(OK)");
        }
    }
}
