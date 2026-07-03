using System.IO;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Running;

namespace MdAvBench;

internal class Program
{
    public static void Main(string[] args)
    {
        var summaries = BenchmarkRunner.Run(typeof(Program).Assembly);
        var exporter = AsciiDocExporter.Default;

        var logger = new StringLogger();

        foreach (var summary in summaries)
        {
            exporter.ExportToLog(summary, logger);
            logger.WriteLine();
        }

        File.WriteAllText("summary.md", logger.ToString());
    }
}