# WindowedContentDialog for WPF

Show ContentDialog and MessageBox with similar layout to WinUI 3 in Aero style in a separate window.

![WindowedContentDialogAero](https://github.com/user-attachments/assets/3c9e79bf-e332-4295-a18d-ea3c046a2b3c)

There has been WindowedContentDialog for WinUI3: https://github.com/SuGar0218/WindowedContentDialog

This library depends on .NET 8.

``` xml
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

## Using in { code-behind }

### Using similarly to ContentDialog in WinUI 3

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

### Using similarly to MessageBox in WPF or WinForm

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

## Using in < XAML />

``` xml
xmlns:dialogs="clr-namespace:SuGarToolkit.WPF.Controls.Dialogs;assembly=SuGarToolkit.WPF.Controls.Dialogs"
```

### Using ```WindowedContentDialog``` in ```<Page.Resources>```

``` xml
<Page.Resources>
    <dialogs:WindowedContentDialog
        x:Key="XamlWindowedContentDialog"
        Title="{Binding ContentDialogSettings.Title, Mode=OneWay}"
        CloseButtonText="{Binding ContentDialogSettings.CloseButtonText, Mode=OneWay}"
        DataContext="{x:Reference ViewModel}"
        DefaultButton="{Binding ContentDialogSettings.DefaultButton, Mode=OneWay}"
        IsModal="{Binding ContentDialogSettings.IsModal, Mode=OneWay}"
        IsPrimaryButtonEnabled="{Binding ContentDialogSettings.IsPrimaryButtonEnabled, Mode=OneWay}"
        IsSecondaryButtonEnabled="{Binding ContentDialogSettings.IsSecondaryButtonEnabled, Mode=OneWay}"
        OwnerWindow="{x:Static app:MainWindow.Current}"
        PrimaryButtonText="{Binding ContentDialogSettings.PrimaryButtonText, Mode=OneWay}"
        SecondaryButtonText="{Binding ContentDialogSettings.SecondaryButtonText, Mode=OneWay}">

        <dialogs:WindowedContentDialog.TitleTemplate>
            <DataTemplate DataType="{x:Type system:String}">
                <dialogs:MessageBoxHeader Icon="Information" Text="{Binding}" />
            </DataTemplate>
        </dialogs:WindowedContentDialog.TitleTemplate>

        <StackPanel>
            <CheckBox Content="Lorem" IsThreeState="True" />
            <CheckBox Content="Ipsum" IsThreeState="True" />
            <CheckBox Content="Dolor" IsThreeState="True" />
            <CheckBox Content="Sit" IsThreeState="True" />
            <CheckBox Content="Amet" IsThreeState="True" />
        </StackPanel>
    </dialogs:WindowedContentDialog>
</Page.Resources>
```

### Using ```ContentDialogWindow``` in XAML

``` xml
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

```
