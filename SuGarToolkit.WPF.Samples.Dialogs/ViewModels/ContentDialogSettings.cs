using CommunityToolkit.Mvvm.ComponentModel;

using SuGarToolkit.WPF.Controls.Dialogs;

using System.Windows;

namespace SuGarToolkit.WPF.Samples.Dialogs.ViewModels;

public partial class ContentDialogSettings : ObservableObject
{
    public string Title { get; set; } = "Lorem ipsum dolor sit amet";

    public string Message { get; set; } = "";

    public string PrimaryButtonText { get; set; } = "Primary Button";

    public string SecondaryButtonText { get; set; } = "Secondary Button";

    public string CloseButtonText { get; set; } = "Close";

    public ContentDialogButton DefaultButton { get; set; } = ContentDialogButton.Primary;

    [ObservableProperty]
    public partial bool IsChild { get; set; } = true;

    public bool IsModal { get; set; } = true;

    public bool IsTitleBarVisible { get; set; } = true;

    public bool PrimaryButtonNotClose { get; set; }

    public bool SecondaryButtonNotClose { get; set; }

    public bool DisableBehind { get; set; }

    public bool CenterInParent { get; set; } = true;

    public ThemeMode ThemeMode { get; set; }
}
