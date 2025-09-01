using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shell;

namespace SuGarToolkit.WPF.Controls.Dialogs;

public partial class WindowedContentDialogPresenterWindow : Window
{
    public WindowedContentDialogPresenterWindow()
    {
        InitializeWindow();
    }

    private void InitializeWindow()
    {
        SizeToContent = SizeToContent.WidthAndHeight;
        Activated += (sender, args) => ContentDialogContent!.AfterGotFocus();
        Deactivated += (sender, args) => ContentDialogContent!.AfterLostFocus();
        if (Environment.OSVersion.Version.Major >= 10)
        {
            WindowChrome.SetWindowChrome(this, new WindowChrome()
            {
                GlassFrameThickness = new Thickness(-1),
                NonClientFrameEdges = NonClientFrameEdges.Left | NonClientFrameEdges.Right | NonClientFrameEdges.Bottom
            });
        }
    }

    internal void InitializeComponent(ContentDialogContent component)
    {
        component.PrimaryButtonClick += OnPrimaryButtonClick;
        component.SecondaryButtonClick += OnSecondaryButtonClick;
        component.CloseButtonClick += OnCloseButtonClick;
        component.Loaded += OnContentLoaded;

        Content = component;

        Closed += (sender, args) =>
        {
            ContentDialogContent.PrimaryButtonClick -= OnPrimaryButtonClick;
            ContentDialogContent.SecondaryButtonClick -= OnSecondaryButtonClick;
            ContentDialogContent.CloseButtonClick -= OnCloseButtonClick;
            ContentDialogContent.Loaded -= OnContentLoaded;
            ContentDialogContent.TitleArea.PreviewMouseLeftButtonDown -= OnTitleAreaPreviewMouseLeftButtonDown;
        };
    }

    private void OnContentLoaded(object sender, RoutedEventArgs args)
    {
        ContentDialogContent.TitleArea.PreviewMouseLeftButtonDown += OnTitleAreaPreviewMouseLeftButtonDown;
    }

    private void OnTitleAreaPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        DragMove();
    }

    public event CancelEventHandler? PrimaryButtonClick;
    public event CancelEventHandler? SecondaryButtonClick;
    public event CancelEventHandler? CloseButtonClick;

    public ContentDialogResult Result { get; private set; }

    private void OnPrimaryButtonClick(object sender, RoutedEventArgs e)
    {
        Result = ContentDialogResult.Primary;
        CancelEventArgs args = new();
        PrimaryButtonClick?.Invoke(this, args);
        AfterCommandSpaceButtonClick(args);
    }

    private void OnSecondaryButtonClick(object sender, RoutedEventArgs e)
    {
        Result = ContentDialogResult.Secondary;
        CancelEventArgs args = new();
        SecondaryButtonClick?.Invoke(this, args);
        AfterCommandSpaceButtonClick(args);
    }

    private void OnCloseButtonClick(object sender, RoutedEventArgs e)
    {
        Result = ContentDialogResult.None;
        CancelEventArgs args = new();
        CloseButtonClick?.Invoke(this, args);
        AfterCommandSpaceButtonClick(args);
    }

    private void AfterCommandSpaceButtonClick(CancelEventArgs args)
    {
        if (args.Cancel)
            return;

        Close();
    }

    private ContentDialogContent ContentDialogContent => (ContentDialogContent) Content;
}
