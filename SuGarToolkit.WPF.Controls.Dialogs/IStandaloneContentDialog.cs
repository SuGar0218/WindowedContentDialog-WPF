using System.Windows;
using System.Windows.Media;

namespace SuGarToolkit.WPF.Controls.Dialogs;

public interface IStandaloneContentDialog
{
    public ContentDialogResult Show();

    public object? Title { get; set; }
    public object? Content { get; set; }
    public string? PrimaryButtonText { get; set; }
    public string? SecondaryButtonText { get; set; }
    public string? CloseButtonText { get; set; }
    public Brush? Foreground { get; set; }
    public Brush? Background { get; set; }
    public Brush? BorderBrush { get; set; }
    public Thickness BorderThickness { get; set; }
    public DataTemplate? TitleTemplate { get; set; }
    public DataTemplate? ContentTemplate { get; set; }
    public ContentDialogButton DefaultButton { get; set; }
    public bool IsPrimaryButtonEnabled { get; set; }
    public bool IsSecondaryButtonEnabled { get; set; }
    public Style? PrimaryButtonStyle { get; set; }
    public Style? SecondaryButtonStyle { get; set; }
    public Style? CloseButtonStyle { get; set; }
    public FlowDirection FlowDirection { get; set; }

    /// <summary>
    /// ThemeMode.None is treated as following owner window
    /// </summary>
    public ThemeMode ThemeMode { get; set; }

    public ThemeMode DetermineTheme() => ThemeMode;
}
