using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using ApprovalTests;
using NUnit.Framework;
using UnitTest.Base.Apps;
using UnitTest.Base.Utils;

namespace UnitTest.Base;

public class UnitTestBase
{
    protected string AssetPath;

    private IDisposable disposable;

    static UnitTestBase()
    {
        var fwNm = Util.GetRuntimeName();
        Approvals.RegisterDefaultNamerCreation(() => new ChangeOutputPathNamer("Out"));
    }

    public UnitTestBase()
    {
        var asm = Assembly.GetExecutingAssembly();
        AssetPath = Path.GetDirectoryName(asm.Location);
        disposable = App.Start();
    }

    [SetUp]
    public void WaitApplicationStart()
    {
        Debug.Print("Begin WaitApplicationStart");
        while (!App.ApplicationStarted)
            Thread.Sleep(10);
        Debug.Print("End WaitApplicationStart");
    }
}