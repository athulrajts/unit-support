using Serilog;
using Serilog.Events;
using System.Units.CodeGeneration.Generators;

namespace System.Units.CodeGeneration
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo
                .Console(LogEventLevel.Information)
                .CreateLogger();

            var quantities = QuantityJsonFilesParser.ParseQuantities(@"..\..\..\UnitDefinitions\");

            UnitsNetGenerator.Generate(@"..\..\..\..\", quantities);
        }
    }
}
