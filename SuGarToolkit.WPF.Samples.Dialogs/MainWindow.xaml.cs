using System.Windows;

namespace SuGarToolkit.WPF.Samples.Dialogs;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        ThemeMode = Application.Current.ThemeMode;
        InitializeComponent();
    }

    // The content in Frame cannot follow ThemeMode of Window.
    //private void Frame_Loaded(object sender, RoutedEventArgs e)
    //{
    //    Frame frame = (Frame) sender;
    //    frame.Navigate(new SamplePage());
    //}
}