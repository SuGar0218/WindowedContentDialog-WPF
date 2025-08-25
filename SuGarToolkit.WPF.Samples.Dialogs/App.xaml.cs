using System.Windows;

namespace SuGarToolkit.WPF.Samples.Dialogs;

public partial class App : Application
{
    public App()
    {
        if (Environment.OSVersion.Version.Major < 10)
        {
            ThemeMode = ThemeMode.Light;
        }
        else
        {
            ThemeMode = ThemeMode.System;
        }
    }
}
