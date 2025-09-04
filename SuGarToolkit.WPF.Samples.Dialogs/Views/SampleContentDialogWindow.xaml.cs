using SuGarToolkit.WPF.Controls.Dialogs;

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
