﻿using System.Windows;

namespace SuGarToolkit.WPF.Controls.Dialogs;

public class MessageBox : MessageBoxBase
{
    protected MessageBox(
        bool isModal,
        Window? owner,
        object? content,
        string? title,
        MessageBoxButtons buttons,
        MessageBoxIcon icon,
        MessageBoxDefaultButton? defaultButton,
        MessageBoxOptions options) : base(content, title, buttons, icon, defaultButton, options)
    {
        _isModal = isModal;
        _owner = owner;
        _options = options;
    }

    protected MessageBox(
        bool isModal,
        Window? owner,
        string? message,
        string? title,
        MessageBoxButtons buttons,
        MessageBoxIcon icon,
        MessageBoxDefaultButton? defaultButton,
        MessageBoxOptions options) : base(message, title, buttons, icon, defaultButton, options)
    {
        _isModal = isModal;
        _owner = owner;
        _options = options;
    }

    private readonly bool _isModal;
    private readonly Window? _owner;
    private readonly MessageBoxOptions _options;

    /// <summary>
    /// Show text messages in a MessageBox with only OK button.
    /// <br/>
    /// Overload #11:
    /// Invoke overload #9 with without owner/parent window.
    /// </summary>
    public static MessageBoxResult Show(string? message, string? title = null) => Show(false, null, message, title);

    /// <summary>
    /// Show text messages in a MessageBox with only OK button.
    /// <br/>
    /// Overload #10:
    /// Invoke overload #8 without owner/parent window.
    /// </summary>
    public static MessageBoxResult Show(object? content, string? title = null) => Show(false, null, content, title);

    /// <summary>
    /// Show text messages in a MessageBox with only OK button.
    /// <br/>
    /// Overload #9:
    /// Invoke overload #5 with buttons is MessageBoxButtons.OK.
    /// </summary>
    public static MessageBoxResult Show(
        bool isModal,
        Window? owner,
        string? message,
        string? title = null)
        => Show(isModal, owner, message, title, MessageBoxButtons.OK, MessageBoxDefaultButton.Button1, false);

    /// <summary>
    /// Show text messages in a MessageBox with only OK button.
    /// <br/>
    /// Overload #8:
    /// Invoke overload #4 with buttons is MessageBoxButtons.OK.
    /// </summary>
    public static MessageBoxResult Show(
        bool isModal,
        Window? owner,
        object? content,
        string? title = null)
        => Show(isModal, owner, content, title, MessageBoxButtons.OK, MessageBoxDefaultButton.Button1, false);

    /// <summary>
    /// Show text messages in a MessageBox without icon.
    /// <br/>
    /// Overload #7:
    /// Invoke overload #6 with content is a SelectableTextBox.
    /// </summary>
    public static MessageBoxResult Show(
        string? message,
        string? title,
        MessageBoxButtons buttons,
        MessageBoxDefaultButton? defaultButton = MessageBoxDefaultButton.Button1,
        bool isTitleBarVisible = true)
        => Show(false, null, message, title, buttons, defaultButton, isTitleBarVisible);

    /// <summary>
    /// Show a MessageBox without icon.
    /// <br/>
    /// Overload #6:
    /// Invoke overload #4 without owner/parent window.
    /// </summary>
    public static MessageBoxResult Show(
        object? content,
        string? title,
        MessageBoxButtons buttons,
        MessageBoxDefaultButton? defaultButton = MessageBoxDefaultButton.Button1,
        bool isTitleBarVisible = true)
        => Show(false, null, content, title, buttons, defaultButton, isTitleBarVisible);

    /// <summary>
    /// Show text messages in a MessageBox without icon.
    /// <br/>
    /// Overload #5:
    /// Invoke overload #4 with content is a SelectableTextBox.
    /// </summary>
    public static MessageBoxResult Show(
        bool isModal,
        Window? owner,
        string? message,
        string? title,
        MessageBoxButtons buttons,
        MessageBoxDefaultButton? defaultButton = MessageBoxDefaultButton.Button1,
        bool isTitleBarVisible = true)
    {
        return Show(isModal, owner, message, title, buttons, MessageBoxIcon.None, defaultButton, isTitleBarVisible);
    }

    /// <summary>
    /// Show a MessageBox without icon.
    /// <br/>
    /// Overload #4:
    /// Invoke overload #2 with icon = MessageBoxIcon.None.
    /// </summary>
    public static MessageBoxResult Show(
        bool isModal,
        Window? owner,
        object? content,
        string? title,
        MessageBoxButtons buttons,
        MessageBoxDefaultButton? defaultButton = MessageBoxDefaultButton.Button1,
        bool isTitleBarVisible = true)
    {
        return Show(isModal, owner, content, title, buttons, MessageBoxIcon.None, defaultButton, isTitleBarVisible);
    }

    /// <summary>
    /// Determine whether to show title bar (mainly the close button) and show text messages in a MessageBox.
    /// <br/>
    /// Overload #3:
    /// Invoke overload #2 with content is a SelectableTextBox.
    /// </summary>
    public static MessageBoxResult Show(
        bool isModal,
        Window? owner,
        string? message,
        string? title,
        MessageBoxButtons buttons,
        MessageBoxIcon icon,
        MessageBoxDefaultButton? defaultButton = MessageBoxDefaultButton.Button1,
        bool isTitleBarVisible = true)
    {
        MessageBoxOptions options = MessageBoxOptions.Default;
        options.IsTitleBarVisible = isTitleBarVisible;
        return Show(isModal, owner, message, title, buttons, icon, defaultButton, options);
    }

    /// <summary>
    /// Determine whether to show title bar (mainly the close button) and show a MessageBox.
    /// <br/>
    /// Overload #2:
    /// Invoke overload #0 and only set IsTitleBarVisible in options.
    /// </summary>
    public static MessageBoxResult Show(
        bool isModal,
        Window? owner,
        object? content,
        string? title,
        MessageBoxButtons buttons,
        MessageBoxIcon icon,
        MessageBoxDefaultButton? defaultButton = MessageBoxDefaultButton.Button1,
        bool isTitleBarVisible = true)
    {
        MessageBoxOptions options = MessageBoxOptions.Default;
        options.IsTitleBarVisible = isTitleBarVisible;
        return Show(isModal, owner, content, title, buttons, icon, defaultButton, options);
    }

    /// <summary>
    /// Show text messages in a MessageBox with a similar appearance to WinUI 3 ContentDialog.
    /// Overload #1:
    /// Invoke overload #0 with content is a SelectableTextBox.
    /// </summary>
    /// <param name="isModal">Whether the MessageBox is a modal window.</param>
    /// <param name="owner">Owner/Parent window of the MessageBox</param>
    /// <param name="message">Text message displayed in body area</param>
    /// <param name="title">Text text displayed in header area</param>
    /// <param name="buttons">The button combination displayed at the bottom of MessageBox</param>
    /// <param name="icon">MessageBox icon</param>
    /// <param name="defaultButton">Which button should be focused initially</param>
    /// <param name="options">Other style settings like SystemBackdrop.</param>
    public static MessageBoxResult Show(
        bool isModal,
        Window? owner,
        string? message,
        string? title,
        MessageBoxButtons buttons,
        MessageBoxIcon icon,
        MessageBoxDefaultButton? defaultButton,
        MessageBoxOptions options)
    {
        return new MessageBox(isModal, owner, message, title, buttons, icon, defaultButton, options).Show();
    }

    /// <summary>
    /// Show a MessageBox with a similar appearance to WinUI 3 ContentDialog.
    /// <br/>
    /// Overload #0:
    /// The main overload of Show with full parameters.
    /// </summary>
    /// <param name="isModal">Whether the MessageBox is a modal window.</param>
    /// <param name="owner">Owner/Parent window of the MessageBox</param>
    /// <param name="content">DialogContent displayed in body area, which can be string or UIElement</param>
    /// <param name="title">Text text displayed in header area</param>
    /// <param name="buttons">The button combination displayed at the bottom of MessageBox</param>
    /// <param name="icon">MessageBox icon</param>
    /// <param name="defaultButton">Which button should be focused initially</param>
    /// <param name="options">Other style settings like SystemBackdrop.</param>
    /// <returns>The button selected by user.</returns>
    public static MessageBoxResult Show(
        bool isModal,
        Window? owner,
        object? content,
        string? title,
        MessageBoxButtons buttons,
        MessageBoxIcon icon,
        MessageBoxDefaultButton? defaultButton,
        MessageBoxOptions options)
    {
        return new MessageBox(isModal, owner, content, title, buttons, icon, defaultButton, options).Show();
    }

    protected override IStandaloneContentDialog CreateDialog() => new WindowedContentDialog
    {
        WindowTitle = _title,
        IsModal = _isModal,
        OwnerWindow = _owner,
        CenterInParent = _options.CenterInParent,
        IsTitleBarVisible = _options.IsTitleBarVisible
    };
}

