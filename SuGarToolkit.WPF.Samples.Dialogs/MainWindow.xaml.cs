using SuGarToolkit.WPF.Controls.Dialogs;

using System.Windows;

using MessageBoxResult = SuGarToolkit.WPF.Controls.Dialogs.MessageBoxResult;

namespace SuGarToolkit.WPF.Samples.Dialogs;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void ContentDialogContent_PrimaryButtonClick(object sender, RoutedEventArgs e)
    {
        WindowedContentDialog dialog = new()
        {
            WindowTitle = "WindowedContentDialog",
            Title = "Windowed Content Dialog",
            Content = new LoremIpsumPage().Content,
            PrimaryButtonText = "Primary Button",
            SecondaryButtonText = "Secondary Button",
            CloseButtonText = "Close",
            DefaultButton = ContentDialogButton.Close,
            OwnerWindow = this,
            CenterInParent = true,
            IsModal = true,
        };
        ContentDialogResult result = dialog.Show();
        WindowedContentDialogResultTextBlock.Text = result.ToString();
    }

    private void ContentDialogContent_SecondaryButtonClick(object sender, RoutedEventArgs e)
    {
        MessageBoxResult result = FluentMessageBox.Show(
            isModal: true,
            this,
            "This is a MessageBox in Fluent Design style.",
            "Fluent Message Box",
            MessageBoxButtons.YesNoCancel,
            MessageBoxIcon.Information,
            MessageBoxDefaultButton.Button1);
        MessageBoxResultTextBlock.Text = result.ToString();
    }

    private void ContentDialogContent_CloseButtonClick(object sender, RoutedEventArgs e)
    {
        Close();
    }
}