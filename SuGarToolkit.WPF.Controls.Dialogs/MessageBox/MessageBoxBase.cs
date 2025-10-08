using SuGarToolkit.WPF.Controls.Dialogs.Strings;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SuGarToolkit.WPF.Controls.Dialogs;

public abstract class MessageBoxBase
{
    protected MessageBoxBase(
        object? content,
        string? title,
        MessageBoxButtons buttons,
        MessageBoxIcon icon,
        MessageBoxDefaultButton? defaultButton,
        MessageBoxBaseOptions options)
    {
        _content = content;
        _title = title;
        _buttons = buttons;
        _icon = icon;
        _defaultButton = defaultButton;
        _options = options;
        _dialog = null!;
    }

    protected MessageBoxBase(
        string? message,
        string? title,
        MessageBoxButtons buttons,
        MessageBoxIcon icon,
        MessageBoxDefaultButton? defaultButton,
        MessageBoxBaseOptions options) : this(CreateSelectableTextBlock(message), title, buttons, icon, defaultButton, options) { }

    protected readonly object? _content;
    protected readonly string? _title;
    protected readonly MessageBoxButtons _buttons;
    protected readonly MessageBoxIcon _icon;
    protected readonly MessageBoxDefaultButton? _defaultButton;
    private readonly MessageBoxBaseOptions _options;
    private IStandaloneContentDialog _dialog;

    protected MessageBoxResult Show()
    {
        _dialog = CreateDialog();
        _dialog.Content = _content;
        _dialog.Title = new MessageBoxHeader
        {
            Icon = _icon,
            Text = _title
        };
        _dialog.FlowDirection = _options.FlowDirection;
        _dialog.ThemeMode = _options.ThemeMode;
        DetermineDefaultButton();
        DetermineButtonText();
        return ShowAndWaitForResult();
    }

    /// <summary>
    /// Create instance and set properties not contained in StandaloneContentDialogBase. 
    /// </summary>
    /// <returns></returns>
    protected abstract IStandaloneContentDialog CreateDialog();

    //protected abstract ElementTheme DetermineTheme();

    protected void DetermineDefaultButton()
    {
        _dialog.DefaultButton = _defaultButton switch
        {
            MessageBoxDefaultButton.Button1 => ContentDialogButton.Primary,
            MessageBoxDefaultButton.Button2 => ContentDialogButton.Secondary,
            MessageBoxDefaultButton.Button3 => ContentDialogButton.Close,
            null => ContentDialogButton.None,
            _ => ContentDialogButton.None
        };
    }

    protected void DetermineButtonText()
    {
        switch (_buttons)
        {
            case MessageBoxButtons.OK:
                _dialog.CloseButtonText = NativeMessageBoxButtonStringLoader.OK.Replace('&', '_');
                _dialog.DefaultButton = ContentDialogButton.Close;
                break;

            case MessageBoxButtons.OKCancel:
                _dialog.PrimaryButtonText = NativeMessageBoxButtonStringLoader.OK.Replace('&', '_');
                _dialog.CloseButtonText = NativeMessageBoxButtonStringLoader.Cancel.Replace('&', '_');
                _dialog.DefaultButton = ContentDialogButton.Close;
                break;

            case MessageBoxButtons.YesNo:
                _dialog.PrimaryButtonText = NativeMessageBoxButtonStringLoader.Yes.Replace('&', '_');
                _dialog.SecondaryButtonText = NativeMessageBoxButtonStringLoader.No.Replace('&', '_');
                break;

            case MessageBoxButtons.YesNoCancel:
                _dialog.PrimaryButtonText = NativeMessageBoxButtonStringLoader.Yes.Replace('&', '_');
                _dialog.SecondaryButtonText = NativeMessageBoxButtonStringLoader.No.Replace('&', '_');
                _dialog.CloseButtonText = NativeMessageBoxButtonStringLoader.Cancel.Replace('&', '_');
                break;

            case MessageBoxButtons.AbortRetryIgnore:
                _dialog.PrimaryButtonText = NativeMessageBoxButtonStringLoader.Abort.Replace('&', '_');
                _dialog.SecondaryButtonText = NativeMessageBoxButtonStringLoader.Retry.Replace('&', '_');
                _dialog.CloseButtonText = NativeMessageBoxButtonStringLoader.Ignore.Replace('&', '_');
                break;

            case MessageBoxButtons.RetryCancel:
                _dialog.PrimaryButtonText = NativeMessageBoxButtonStringLoader.Retry.Replace('&', '_');
                _dialog.SecondaryButtonText = NativeMessageBoxButtonStringLoader.Cancel.Replace('&', '_');
                break;

            case MessageBoxButtons.CancelTryContinue:
                _dialog.PrimaryButtonText = NativeMessageBoxButtonStringLoader.Continue.Replace('&', '_');
                _dialog.SecondaryButtonText = NativeMessageBoxButtonStringLoader.TryAgain.Replace('&', '_');
                _dialog.CloseButtonText = NativeMessageBoxButtonStringLoader.Cancel.Replace('&', '_');
                _dialog.DefaultButton = ContentDialogButton.Close;
                break;
        }
    }

    protected MessageBoxResult ShowAndWaitForResult()
    {
        ContentDialogResult result = _dialog.Show();
        MessageBoxResult[] results = resultGroups[(int) _buttons];
        return result switch
        {
            ContentDialogResult.None => results[^1],
            ContentDialogResult.Primary => results[0],
            ContentDialogResult.Secondary => results[1],
            _ => MessageBoxResult.None,
        };
    }

    private static readonly MessageBoxResult[][] resultGroups = [
        [MessageBoxResult.OK],
        [MessageBoxResult.OK, MessageBoxResult.Cancel],
        [MessageBoxResult.Abort, MessageBoxResult.Retry, MessageBoxResult.Ignore],
        [MessageBoxResult.Yes, MessageBoxResult.No, MessageBoxResult.Cancel],
        [MessageBoxResult.Yes, MessageBoxResult.No],
        [MessageBoxResult.Retry, MessageBoxResult.Cancel],
        [MessageBoxResult.Continue, MessageBoxResult.TryAgain, MessageBoxResult.Cancel]
    ];

    /// <summary>
    /// Create a readonly TextBox that allows user to select the message text.
    /// <br/>
    /// Why not use TextBlock with IsTextSelectionEnabled=true ?
    /// Currently (WindowsAppSDK 1.7), once user selected text,
    /// TextBlock with IsTextSelectionEnabled=true and TextWrapping=TextWrapping.Wrap
    /// cannot update text wrapping automatically until the next time user selects text.
    /// </summary>
    private static TextBlock? CreateSelectableTextBlock(string? text) => string.IsNullOrEmpty(text) ? null : new()
    {
        Text = text,
        TextWrapping = TextWrapping.Wrap,
        HorizontalAlignment = HorizontalAlignment.Stretch
    };
}
