using System;
using Avalonia.Threading;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using NUnit.Framework.Internal.Commands;

namespace UnitTest.Base.Utils;

[AttributeUsage(AttributeTargets.Method)]
public sealed class RunOnUIAttribute : Attribute, IWrapTestMethod
{
    public TestCommand Wrap(TestCommand command)
    {
        return new RunOnUICommand(command);
    }

    private class RunOnUICommand : DelegatingTestCommand
    {
        public RunOnUICommand(TestCommand innerCommand)
            : base(innerCommand)
        {
        }

        public override TestResult Execute(TestExecutionContext context)
        {
            var dispatcher = Dispatcher.UIThread;

            if (dispatcher.CheckAccess())
            {
                var result = RunTest(context);

                if (result is Exception ex)
                    throw ex;

                return (TestResult)result;
            }
            else
            {
                var resultTask = Dispatcher.UIThread.InvokeAsync(() => RunTest(context));

                if (resultTask.Status != DispatcherOperationStatus.Aborted
                    && resultTask.Status != DispatcherOperationStatus.Completed)
                    resultTask.Wait();

                if (resultTask.Result is Exception ex)
                    throw ex;

                return (TestResult)resultTask.Result;
            }
        }

        private object RunTest(TestExecutionContext context)
        {
            try
            {
                return innerCommand.Execute(context);
            }
            catch (Exception e)
            {
                return e;
            }
        }
    }
}