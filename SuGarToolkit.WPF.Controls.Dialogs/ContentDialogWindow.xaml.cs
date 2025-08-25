using System.ComponentModel;
using System.Windows;

namespace SuGarToolkit.WPF.Controls.Dialogs;

public partial class ContentDialogWindow : Window
{
    public ContentDialogWindow()
    {
        InitializeComponent();
        Activated += (sender, args) => content.AfterGotFocus();
        Deactivated += (sender, args) => content.AfterLostFocus();
        DataContext = this;
    }

    public event CancelEventHandler? PrimaryButtonClick;
    public event CancelEventHandler? SecondaryButtonClick;
    public event CancelEventHandler? CloseButtonClick;

    //public event RoutedEventHandler? Opened;

    public ContentDialogResult Result { get; private set; }

    /// <summary>
    /// 此 DialogTitle 表示对话框标题部分的内容，可以是文本也可以是 UI。
    /// </summary>
    public object? DialogTitle
    {
        get => content.Title;
        set => content.Title = value;
    }

    /// <summary>
    /// 此 DialogContent 表示对话框正文部分的内容，而不是整个窗口的内容。
    /// </summary>
    public object? DialogContent
    {
        get => content.Content;
        set => content.Content = value;
    }

    public DataTemplate? DialogTitleTemplate
    {
        get => content.TitleTemplate;
        set => content.TitleTemplate = value;
    }

    public DataTemplate? DialogContentTemplate
    {
        get => content.ContentTemplate;
        set => content.ContentTemplate = value;
    }

    #region ContentDialogContent properties

    public string? PrimaryButtonText
    {
        get => content.PrimaryButtonText;
        set => content.PrimaryButtonText = value;
    }

    public string? SecondaryButtonText
    {
        get => content.SecondaryButtonText;
        set => content.SecondaryButtonText = value;
    }

    public string? CloseButtonText
    {
        get => content.CloseButtonText;
        set => content.CloseButtonText = value;
    }

    public bool IsPrimaryButtonEnabled
    {
        get => content.IsPrimaryButtonEnabled;
        set => content.IsPrimaryButtonEnabled = value;
    }

    public bool IsSecondaryButtonEnabled
    {
        get => content.IsSecondaryButtonEnabled;
        set => content.IsSecondaryButtonEnabled = value;
    }

    public ContentDialogButton DefaultButton
    {
        get => content.DefaultButton;
        set => content.DefaultButton = value;
    }

    public Style? PrimaryButtonStyle
    {
        get => content.PrimaryButtonStyle;
        set => content.PrimaryButtonStyle = value;
    }

    public Style? SecondaryButtonStyle
    {
        get => content.SecondaryButtonStyle;
        set => content.SecondaryButtonStyle = value;
    }

    public Style? CloseButtonStyle
    {
        get => content.CloseButtonStyle;
        set => content.CloseButtonStyle = value;
    }

    #endregion

    private void Content_Loaded(object sender, RoutedEventArgs e)
    {
        //content.TitleArea.PreviewMouseLeftButtonDown += (sender, args) => DragMove();
    }

    private void OnPrimaryButtonClick(object sender, RoutedEventArgs e)
    {
        Result = ContentDialogResult.Primary;
        CancelEventArgs args = new();
        PrimaryButtonClick?.Invoke(this, args);
        AfterCommandBarButtonClick(args);
    }

    private void OnSecondaryButtonClick(object sender, RoutedEventArgs e)
    {
        Result = ContentDialogResult.Secondary;
        CancelEventArgs args = new();
        SecondaryButtonClick?.Invoke(this, args);
        AfterCommandBarButtonClick(args);
    }

    private void OnCloseButtonClick(object sender, RoutedEventArgs e)
    {
        Result = ContentDialogResult.None;
        CancelEventArgs args = new();
        CloseButtonClick?.Invoke(this, args);
        AfterCommandBarButtonClick(args);
    }

    private void AfterCommandBarButtonClick(CancelEventArgs args)
    {
        if (args.Cancel)
            return;

        Close();
    }
}
