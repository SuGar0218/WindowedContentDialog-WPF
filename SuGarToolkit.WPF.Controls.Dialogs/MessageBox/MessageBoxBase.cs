using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

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
                _dialog.CloseButtonText = NativeMessageBoxButtonTextLoader.OK;
                _dialog.DefaultButton = ContentDialogButton.Close;
                break;

            case MessageBoxButtons.OKCancel:
                _dialog.PrimaryButtonText = NativeMessageBoxButtonTextLoader.OK;
                _dialog.SecondaryButtonText = NativeMessageBoxButtonTextLoader.Cancel;
                break;

            case MessageBoxButtons.YesNo:
                _dialog.PrimaryButtonText = NativeMessageBoxButtonTextLoader.Yes;
                _dialog.SecondaryButtonText = NativeMessageBoxButtonTextLoader.No;
                break;

            case MessageBoxButtons.YesNoCancel:
                _dialog.PrimaryButtonText = NativeMessageBoxButtonTextLoader.Yes;
                _dialog.SecondaryButtonText = NativeMessageBoxButtonTextLoader.No;
                _dialog.CloseButtonText = NativeMessageBoxButtonTextLoader.Cancel;
                break;

            case MessageBoxButtons.AbortRetryIgnore:
                _dialog.PrimaryButtonText = NativeMessageBoxButtonTextLoader.Abort;
                _dialog.SecondaryButtonText = NativeMessageBoxButtonTextLoader.Retry;
                _dialog.CloseButtonText = NativeMessageBoxButtonTextLoader.Ignore;
                break;

            case MessageBoxButtons.RetryCancel:
                _dialog.PrimaryButtonText = NativeMessageBoxButtonTextLoader.Retry;
                _dialog.SecondaryButtonText = NativeMessageBoxButtonTextLoader.Cancel;
                break;

            case MessageBoxButtons.CancelTryContinue:
                _dialog.PrimaryButtonText = NativeMessageBoxButtonTextLoader.Continue;
                _dialog.SecondaryButtonText = NativeMessageBoxButtonTextLoader.TryAgain;
                _dialog.CloseButtonText = NativeMessageBoxButtonTextLoader.Cancel;
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
    private static SelectableTextBlock? CreateSelectableTextBlock(string? text) => string.IsNullOrEmpty(text) ? null : new()
    {
        Text = text,
        TextWrapping = TextWrapping.Wrap,
        HorizontalAlignment = HorizontalAlignment.Stretch,
        Effect = new DropShadowEffect
        {
            Color = Colors.White,
            Opacity = 1.0,
            ShadowDepth = 0,
            BlurRadius = 14
        }
    };
}
