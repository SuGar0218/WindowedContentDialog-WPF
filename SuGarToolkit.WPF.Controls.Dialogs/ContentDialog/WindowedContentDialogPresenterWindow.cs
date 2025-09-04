using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shell;

namespace SuGarToolkit.WPF.Controls.Dialogs;

public partial class WindowedContentDialogPresenterWindow : Window
{
    public WindowedContentDialogPresenterWindow()
    {
        InitializeWindow();
        Background = null;
        FontSize = 14;
    }

    private void InitializeWindow()
    {
        SizeToContent = SizeToContent.WidthAndHeight;
        Activated += (sender, args) => ContentDialogContent!.AfterGotFocus();
        Deactivated += (sender, args) => ContentDialogContent!.AfterLostFocus();
        if (SystemParameters.IsGlassEnabled)
        {
            WindowChrome.SetWindowChrome(this, new WindowChrome()
            {
                GlassFrameThickness = new Thickness(-1),
                NonClientFrameEdges = NonClientFrameEdges.Bottom
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
