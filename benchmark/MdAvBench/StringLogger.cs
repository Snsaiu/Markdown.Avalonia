using System.Text;
using BenchmarkDotNet.Loggers;

namespace MdAvBench;

internal class StringLogger : ILogger
{
    private readonly StringBuilder _builder = new();

    public string Id => "StringLogger";
    public int Priority => 0;

    public void Flush()
    {
    }

    public void Write(LogKind logKind, string text)
    {
        _builder.Append(text);
    }

    public void WriteLine()
    {
        _builder.AppendLine();
    }

    public void WriteLine(LogKind logKind, string text)
    {
        _builder.AppendLine(text);
    }

    public override string ToString()
    {
        return _builder.ToString();
    }
}