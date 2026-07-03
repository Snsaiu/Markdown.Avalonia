using System;
using System.Diagnostics;
using System.Threading;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

namespace UnitTest.Base.Apps;

public class App : Application
{
    internal static bool ApplicationStarted;

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        Debug.Print("OnFrameworkInitializationCompleted Called");

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            Debug.Print("Lifetime is ClassicDesktop");

            var win = new Window();
            win.Loaded += (s, e) => Loaded();

            desktop.MainWindow = win;
        }
        else
        {
            Loaded();
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void Loaded()
    {
        Debug.Print("MainWindowLoaded");
        ApplicationStarted = true;
    }

    public static IDisposable Start()
    {
        var starter = new AppStarter();

        var th = new Thread(starter.Start);
        th.Start();

        return starter;
    }
}

internal class AppStarter : IDisposable
{
    private ClassicDesktopStyleApplicationLifetime lifetime;

    public void Dispose()
    {
        try
        {
            lifetime.Shutdown();
        }
        finally
        {
            lifetime.Dispose();
        }
    }

    public void Start()
    {
        var builder = AppBuilder.Configure<App>();
        builder.UsePlatformDetect();

        var ags = new string[0];

        lifetime = new ClassicDesktopStyleApplicationLifetime
        {
            Args = ags,
            ShutdownMode = ShutdownMode.OnMainWindowClose
        };
        builder.SetupWithLifetime(lifetime);

        lifetime.Start(ags);
    }
}