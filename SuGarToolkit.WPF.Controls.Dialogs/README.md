# WindowedContentDialog for WPF

Show ContentDialog similar to WinUI3 and MessageBox in WinUI3 style in a separate window.

There has been WindowedContentDialog for WinUI3: https://github.com/SuGar0218/WindowedContentDialog

This library depends on the new Fluent style provided for WPF in .NET 9.

``` xaml
<ResourceDictionary.MergedDictionaries>
    <ResourceDictionary Source="pack://application:,,,/PresentationFramework.Fluent;component/Themes/Fluent.xaml" />
</ResourceDictionary.MergedDictionaries>
```

## Attention namespace

Some class names are the same as them in namespace ```System.Windows```, such as ```MessageBox``` and ```MessageBoxResult```.

If you want to use Fluent style MessageBox instead of traditional MessageBox:

``` C#
using MessageBox = SuGarToolkit.WPF.Controls.Dialogs.MessageBox;
using MessageBoxOptions = SuGarToolkit.WPF.Controls.Dialogs.MessageBoxOptions;
using MessageBoxResult = SuGarToolkit.WPF.Controls.Dialogs.MessageBoxResult;
```

## Use similarly to ContentDialog in WinUI 3

``` C#
using SuGarToolkit.WPF.Controls.Dialogs;

WindowedContentDialog dialog = new()
{
    WindowTitle = "Title of the dialog window",
    Title = "Title displayed in the dialog header",
    Content = "The content can be text or UIElement.",

    PrimaryButtonText = "PrimaryButtonText",
    SecondaryButtonText = "SecondaryButtonText",
    CloseButtonText = "CloseButtonText",
    DefaultButton = ContentDialogButton.Primary,

    IsModal = true,
    OwnerWindow = Application.Current.MainWindow,
    CenterInParent = viewModel.ContentDialogSettings.CenterInParent
};
ContentDialogResult result = dialog.Show();
```

If you want to prevent dialog from closing after buttons clicked, please handle click event and set ```e.Cancel = true``` where ```e``` is ```CancelEventArgs```.

``` C#
dialog.PrimaryButtonClick += (o, e) => e.Cancel = true;
```

## Use similarly to MessageBox in WPF or WinForm

``` C#
using SuGarToolkit.WPF.Controls.Dialogs;
using MessageBox = SuGarToolkit.WPF.Controls.Dialogs.MessageBox;
using MessageBoxOptions = SuGarToolkit.WPF.Controls.Dialogs.MessageBoxOptions;
using MessageBoxResult = SuGarToolkit.WPF.Controls.Dialogs.MessageBoxResult;

MessageBoxResult result = MessageBox.Show(
    true,  // whether the MessageBox is modal
    Application.Current.MainWindow,  // owner/parent window of MessageBox
    "The content can be text or UIElement",  // content displayed in MessageBox body
    "The title of MessageBox",  // title displayed in MessageBox header
    MessageBoxButtons.YesNo,  // the button group of MessageBox similar to WinForm
    MessageBoxIcon.Information,  // the icon displayed in MessageBox header
    MessageBoxDefaultButton.Button1,  // which button is default
    new MessageBoxOptions
    {
        CenterInParent = true  // whether to appear at the center of owner/parent window
    }
);
```
