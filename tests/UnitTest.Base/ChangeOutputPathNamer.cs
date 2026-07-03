using System.IO;
using ApprovalTests.Namers;

namespace UnitTest.Base;

public class ChangeOutputPathNamer : UnitTestFrameworkNamer
{
    private readonly string dir;

    public ChangeOutputPathNamer(string dir)
    {
        this.dir = dir;
    }

    public override string SourcePath
    {
        get
        {
            var basePath = base.SourcePath;
            return Path.Combine(basePath, dir);
        }
    }
}