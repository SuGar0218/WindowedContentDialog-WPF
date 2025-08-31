using SuGarToolkit.WPF.Controls.Dialogs;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SuGarToolkit.WPF.Samples.Dialogs.Views;

public partial class SampleContentDialogWindow : ContentDialogWindow
{
    public SampleContentDialogWindow()
    {
        InitializeComponent();
    }

    // SuGarToolkit.WPF.Samples.Dialogs.Views.SampleContentDialogWindow.XamlCode
    public static readonly string XamlCode = """
<dialogs:ContentDialogWindow
    xmlns:dialogs="clr-namespace:SuGarToolkit.WPF.Controls.Dialogs;assembly=SuGarToolkit.WPF.Controls.Dialogs"
    Title="Sample ContentDialogWindow"
    CloseButtonText="Close"
    DefaultButton="Primary"
    DialogTitle="Sample ContentDialogWindow"
    PrimaryButtonText="Primary Button"
    SecondaryButtonText="Secondary Button"
    ...>

    <StackPanel>
        <CheckBox Content="Using" IsThreeState="True" />
        <CheckBox Content="ContentDialogWindow" IsChecked="True" />
        <CheckBox Content="in XAML" IsThreeState="True" />
    </StackPanel>

</dialogs:ContentDialogWindow>
""";
}
