using SuGarToolkit.Sample.Dialogs.ViewModels;
using SuGarToolkit.WPF.Controls.Dialogs;
using SuGarToolkit.WPF.Samples.Dialogs.ViewModels;

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
using System.Windows.Navigation;
using System.Windows.Shapes;

using MessageBox = SuGarToolkit.WPF.Controls.Dialogs.MessageBox;
using MessageBoxOptions = SuGarToolkit.WPF.Controls.Dialogs.MessageBoxOptions;
using MessageBoxResult = SuGarToolkit.WPF.Controls.Dialogs.MessageBoxResult;

namespace SuGarToolkit.WPF.Samples.Dialogs.Views;

public partial class SamplePage : Page
{
    public SamplePage()
    {
        InitializeComponent();
        DataContext = viewModel;
    }

    private readonly SampleViewModel viewModel = new();

    private void ShowMessageBoxButton_Click(object sender, RoutedEventArgs e)
    {
        MessageBoxResult result = MessageBox.Show(
            viewModel.MessageBoxSettings.IsModal,
            viewModel.MessageBoxSettings.IsChild ? Application.Current.MainWindow : null,
            viewModel.MessageBoxSettings.Content,
            viewModel.MessageBoxSettings.Title,
            viewModel.MessageBoxSettings.Buttons,
            viewModel.MessageBoxSettings.Image,
            viewModel.MessageBoxSettings.DefaultButton,
            new MessageBoxOptions
            {
                IsTitleBarVisible = viewModel.MessageBoxSettings.IsTitleBarVisible,
                CenterInParent = viewModel.MessageBoxSettings.CenterInParent,

                //DisableBehind = viewModel.MessageBoxSettings.DisableBehind,
            });
        MessageBoxResultBox.Text = result.ToString();
    }

    private void ShowContentDialogButton_Click(object sender, RoutedEventArgs e)
    {
        WindowedContentDialog dialog = new()
        {
            WindowTitle = viewModel.ContentDialogSettings.Title,
            Title = viewModel.ContentDialogSettings.Title,
            Content = !string.IsNullOrEmpty(viewModel.ContentDialogSettings.Message) ? viewModel.ContentDialogSettings.Message : new LoremIpsumPage().Content,

            PrimaryButtonText = viewModel.ContentDialogSettings.PrimaryButtonText,
            SecondaryButtonText = viewModel.ContentDialogSettings.SecondaryButtonText,
            CloseButtonText = viewModel.ContentDialogSettings.CloseButtonText,
            DefaultButton = viewModel.ContentDialogSettings.DefaultButton,

            OwnerWindow = viewModel.ContentDialogSettings.IsChild ? Application.Current.MainWindow : null,
            IsTitleBarVisible = viewModel.ContentDialogSettings.IsTitleBarVisible,
            CenterInParent = viewModel.ContentDialogSettings.CenterInParent,

            //DisableBehind = viewModel.ContentDialogSettings.DisableBehind,
        };
        if (viewModel.ContentDialogSettings.PrimaryButtonNotClose)
        {
            dialog.PrimaryButtonClick += (o, e) => e.Cancel = true;
        }
        if (viewModel.ContentDialogSettings.SecondaryButtonNotClose)
        {
            dialog.SecondaryButtonClick += (o, e) => e.Cancel = true;
        }
        ContentDialogResult result = dialog.Show(viewModel.ContentDialogSettings.IsModal);
        ContentDialogResultBox.Text = result.ToString();
    }
}
