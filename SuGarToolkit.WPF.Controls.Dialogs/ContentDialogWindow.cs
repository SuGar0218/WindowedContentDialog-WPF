using SuGarToolkit.WPF.SourceGenerators;

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Shell;

namespace SuGarToolkit.WPF.Controls.Dialogs;

/// <summary>
/// Derived classes must 
/// </summary>
public partial class ContentDialogWindow : Window
{
    static ContentDialogWindow() => DefaultStyleKeyProperty.OverrideMetadata(typeof(ContentDialogWindow), new FrameworkPropertyMetadata(typeof(ContentDialogWindow)));

    public ContentDialogWindow()
    {
        if (Environment.OSVersion.Version.Major >= 10)
        {
            WindowChrome.SetWindowChrome(this, new WindowChrome()
            {
                GlassFrameThickness = new Thickness(-1),
                NonClientFrameEdges = NonClientFrameEdges.Left | NonClientFrameEdges.Right | NonClientFrameEdges.Bottom
            });
        }
        Activated += (sender, args) => ContentDialogContent!.AfterGotFocus();
        Deactivated += (sender, args) => ContentDialogContent!.AfterLostFocus();
        Style = (Style) Application.Current.Resources["ContentDialogWindowDefaultStyle"];
        DataContext = this;
    }

    public event CancelEventHandler? PrimaryButtonClick;
    public event CancelEventHandler? SecondaryButtonClick;
    public event CancelEventHandler? CloseButtonClick;

    #region properties

    /// <summary>
    /// 此 DialogTitle 表示对话框标题部分的内容，可以是文本也可以是 UI。
    /// </summary>
    [DependencyProperty]
    public partial object? DialogTitle { get; set; }

    /// <summary>
    /// 此 DialogTitle 表示对话框内容部分的内容，与 Content 作用相同。
    /// </summary>
    [RelayDependencyProperty(nameof(Content))]
    public partial object? DialogContent { get; set; }

    [DependencyProperty]
    public partial DataTemplate? DialogTitleTemplate { get; set; }

    [DependencyProperty]
    public partial string? PrimaryButtonText { get; set; }

    [DependencyProperty]
    public partial string? SecondaryButtonText { get; set; }

    [DependencyProperty]
    public partial string? CloseButtonText { get; set; }

    [DependencyProperty(DefaultValue = true)]
    public partial bool IsPrimaryButtonEnabled { get; set; }

    [DependencyProperty(DefaultValue = true)]
    public partial bool IsSecondaryButtonEnabled { get; set; }

    [DependencyProperty(DefaultValue = ContentDialogButton.Close)]
    public partial ContentDialogButton DefaultButton { get; set; }

    [DependencyProperty(DefaultValuePath = nameof(DefaultButtonStyle))]
    public partial Style? PrimaryButtonStyle { get; set; }

    [DependencyProperty(DefaultValuePath = nameof(DefaultButtonStyle))]
    public partial Style? SecondaryButtonStyle { get; set; }

    [DependencyProperty(DefaultValuePath = nameof(DefaultButtonStyle))]
    public partial Style? CloseButtonStyle { get; set; }

    #endregion

    public ContentDialogResult Result { get; private set; }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        ContentDialogContent = (ContentDialogContent) GetTemplateChild(nameof(ContentDialogContent));
        ContentDialogContent.PrimaryButtonClick += OnPrimaryButtonClick;
        ContentDialogContent.SecondaryButtonClick += OnSecondaryButtonClick;
        ContentDialogContent.CloseButtonClick += OnCloseButtonClick;
        ContentDialogContent.Loaded += (sender, args) => ContentDialogContent.TitleArea.PreviewMouseLeftButtonDown += (sender, args) => DragMove();
    }

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

    [DisallowNull]
    private ContentDialogContent ContentDialogContent { get; set; }

    private static Style DefaultButtonStyle => (Style) Application.Current.Resources["DefaultButtonStyle"];
}
