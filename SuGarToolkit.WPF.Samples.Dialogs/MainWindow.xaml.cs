using System.Windows;

namespace SuGarToolkit.WPF.Samples.Dialogs;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    public static MainWindow Current => (MainWindow) Application.Current.MainWindow;
}