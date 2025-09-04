using SuGarToolkit.WPF.Controls.Dialogs;

using System.Windows;
using System.Windows.Controls;

using MessageBox = SuGarToolkit.WPF.Controls.Dialogs.MessageBox;
using MessageBoxOptions = SuGarToolkit.WPF.Controls.Dialogs.MessageBoxOptions;
using MessageBoxResult = SuGarToolkit.WPF.Controls.Dialogs.MessageBoxResult;

namespace SuGarToolkit.WPF.Samples.Dialogs.Views;

public partial class SamplePage : Page
{
    public SamplePage()
    {
        InitializeComponent();
    }

    private void ShowMessageBoxButton_Click(object sender, RoutedEventArgs e)
    {
        MessageBoxResult result = MessageBox.Show(
            ViewModel.MessageBoxSettings.IsModal,
            ViewModel.MessageBoxSettings.IsChild ? Application.Current.MainWindow : null,
            ViewModel.MessageBoxSettings.Content,
            ViewModel.MessageBoxSettings.Title,
            ViewModel.MessageBoxSettings.Buttons,
            ViewModel.MessageBoxSettings.Image,
            ViewModel.MessageBoxSettings.DefaultButton,
            new MessageBoxOptions
            {
                CenterInParent = ViewModel.MessageBoxSettings.CenterInParent
            });
        MessageBoxResultBox.Text = result.ToString();
    }

    private void ShowContentDialogButton_Click(object sender, RoutedEventArgs e)
    {
        WindowedContentDialog dialog = new()
        {
            WindowTitle = ViewModel.ContentDialogSettings.Title,
            Title = ViewModel.ContentDialogSettings.Title,
            Content = !string.IsNullOrEmpty(ViewModel.ContentDialogSettings.Message) ? ViewModel.ContentDialogSettings.Message : new LoremIpsumPage().Content,

            PrimaryButtonText = ViewModel.ContentDialogSettings.PrimaryButtonText,
            SecondaryButtonText = ViewModel.ContentDialogSettings.SecondaryButtonText,
            CloseButtonText = ViewModel.ContentDialogSettings.CloseButtonText,
            DefaultButton = ViewModel.ContentDialogSettings.DefaultButton,

            OwnerWindow = ViewModel.ContentDialogSettings.IsChild ? Application.Current.MainWindow : null,
            CenterInParent = ViewModel.ContentDialogSettings.CenterInParent,
        };
        if (ViewModel.ContentDialogSettings.PrimaryButtonNotClose)
        {
            dialog.PrimaryButtonClick += (o, e) => e.Cancel = true;
        }
        if (ViewModel.ContentDialogSettings.SecondaryButtonNotClose)
        {
            dialog.SecondaryButtonClick += (o, e) => e.Cancel = true;
        }
        ContentDialogResult result = dialog.Show(ViewModel.ContentDialogSettings.IsModal);
        ContentDialogResultBox.Text = result.ToString();
    }

    private void ShowXamlContentDialogButton_Click(object sender, RoutedEventArgs e)
    {
        WindowedContentDialog dialog = (WindowedContentDialog) Resources["XamlWindowedContentDialog"];
        ContentDialogResult result = dialog.Show();
        ContentDialogResultBox.Text = result.ToString();
    }

    private void ShowContentDialogWindowButton_Click(object sender, RoutedEventArgs args)
    {
        SampleContentDialogWindow window = new();
        window.Closed += (sender, args) =>
        {
            SampleContentDialogWindow window = (SampleContentDialogWindow) sender!;
            ContentDialogWindowResultBox.Text = window.Result.ToString();
        };
        window.Show();
    }
}
