using SuGarToolkit.WPF.SourceGenerators;

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace SuGarToolkit.WPF.Controls.Dialogs;

[ContentProperty(nameof(Content))]
public partial class WindowedContentDialog : Control, IStandaloneContentDialog
{
    static WindowedContentDialog() => DefaultStyleKeyProperty.OverrideMetadata(typeof(WindowedContentDialog), new FrameworkPropertyMetadata(typeof(WindowedContentDialog)));

    public WindowedContentDialog()
    {
        ContentDialogContent = new ContentDialogContent();
        InitializeRelayDependencyProperties();
    }

    private void InitializePresenterWindow()
    {
        PresenterWindow = new WindowedContentDialogPresenterWindow();
        PresenterWindow.InitializeComponent(ContentDialogContent);
        PresenterWindow.Title = WindowTitle ?? string.Empty;
        PresenterWindow.PrimaryButtonClick += (sender, args) => PrimaryButtonClick?.Invoke(this, args);
        PresenterWindow.SecondaryButtonClick += (sender, args) => SecondaryButtonClick?.Invoke(this, args);
        PresenterWindow.CloseButtonClick += (sender, args) => CloseButtonClick?.Invoke(this, args);
        PresenterWindow.Closed += (sender, args) =>
        {
            Result = PresenterWindow.Result;
            PresenterWindow = null;
        };
    }

    public event CancelEventHandler? PrimaryButtonClick;
    public event CancelEventHandler? SecondaryButtonClick;
    public event CancelEventHandler? CloseButtonClick;

    #region properties

    [RelayDependencyProperty("ContentDialogContent.Title")]
    public partial object? Title { get; set; }

    [RelayDependencyProperty("ContentDialogContent.Content")]
    public partial object? Content { get; set; }

    [RelayDependencyProperty("ContentDialogContent.PrimaryButtonText")]
    public partial string? PrimaryButtonText { get; set; }

    [RelayDependencyProperty("ContentDialogContent.SecondaryButtonText")]
    public partial string? SecondaryButtonText { get; set; }

    [RelayDependencyProperty("ContentDialogContent.CloseButtonText")]
    public partial string? CloseButtonText { get; set; }

    [RelayDependencyProperty("ContentDialogContent.TitleTemplate")]
    public partial DataTemplate? TitleTemplate { get; set; }

    [RelayDependencyProperty("ContentDialogContent.ContentTemplate")]
    public partial DataTemplate? ContentTemplate { get; set; }

    [RelayDependencyProperty("ContentDialogContent.ContentTemplateSelector")]
    public partial DataTemplateSelector? ContentTemplateSelector { get; set; }

    [RelayDependencyProperty("ContentDialogContent.DefaultButton")]
    public partial ContentDialogButton DefaultButton { get; set; }

    [RelayDependencyProperty("ContentDialogContent.IsPrimaryButtonEnabled")]
    public partial bool IsPrimaryButtonEnabled { get; set; }

    [RelayDependencyProperty("ContentDialogContent.IsSecondaryButtonEnabled")]
    public partial bool IsSecondaryButtonEnabled { get; set; }

    [RelayDependencyProperty("ContentDialogContent.PrimaryButtonStyle")]
    public partial Style? PrimaryButtonStyle { get; set; }

    [RelayDependencyProperty("ContentDialogContent.SecondaryButtonStyle")]
    public partial Style? SecondaryButtonStyle { get; set; }

    [RelayDependencyProperty("ContentDialogContent.CloseButtonStyle")]
    public partial Style? CloseButtonStyle { get; set; }

    [DependencyProperty]
    public partial string? WindowTitle { get; set; }

    [DependencyProperty(DefaultValue = true)]
    public partial bool IsTitleBarVisible { get; set; }

    [DependencyProperty(DefaultValue = true)]
    public partial bool CenterInParent { get; set; }

    [DependencyProperty(DefaultValue = true)]
    public partial bool IsModal { get; set; }

    public Window? OwnerWindow { get; set; }

    #endregion

    public ContentDialogResult Result { get; private set; }

    public ContentDialogResult Show(bool isModal)
    {
        IsModal = isModal;
        return Show();
    }

    /// <summary>
    /// 显示对话框窗口，关闭时返回用户选择结果。
    /// <br/>
    /// 不用注意了，窗口关闭时已经脱离了父级控件。
    /// 注意：FrameworkElement 不能多处共享。
    /// 如果 DialogContent 是 FrameworkElement，那么此 FrameworkElement 不能已被其他父级持有，例如 new MainWindow().DialogContent；
    /// 下次更改 DialogContent 前，此弹窗也只能弹出一次，因为每次弹窗都创建一个新的窗口示例，使得同一个 FrameworkElement 被多处共享。
    /// </summary>
    /// <returns>用户选择结果</returns>
    public ContentDialogResult Show()
    {
        if (PresenterWindow is not null)
        {
            PresenterWindow.Activate();
            return ContentDialogResult.None;
        }
        InitializePresenterWindow();
        PresenterWindow!.Owner = OwnerWindow;
        PresenterWindow.WindowStartupLocation = CenterInParent ? (OwnerWindow is null ? WindowStartupLocation.CenterScreen : WindowStartupLocation.CenterOwner) : WindowStartupLocation.Manual;
        if (!IsModal || OwnerWindow is null)
        {
            PresenterWindow.Show();
            return ContentDialogResult.None;
        }
        if (IsModal)
        {
            PresenterWindow.ShowDialog();
        }
        else
        {
            PresenterWindow.Show();
        }
        return Result;
    }

    [DisallowNull]
    private ContentDialogContent ContentDialogContent { get; init; }

    private WindowedContentDialogPresenterWindow? PresenterWindow { get; set; }
}
