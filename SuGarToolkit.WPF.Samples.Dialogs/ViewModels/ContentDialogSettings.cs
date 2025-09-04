using CommunityToolkit.Mvvm.ComponentModel;

using SuGarToolkit.WPF.Controls.Dialogs;

namespace SuGarToolkit.WPF.Samples.Dialogs.ViewModels;

public partial class ContentDialogSettings : ObservableObject
{
    [ObservableProperty]
    public partial string Title { get; set; } = "Lorem ipsum dolor sit amet";

    [ObservableProperty]
    public partial string Message { get; set; } = "";

    [ObservableProperty]
    public partial string PrimaryButtonText { get; set; } = "Primary Button";

    [ObservableProperty]
    public partial string SecondaryButtonText { get; set; } = "Secondary Button";

    [ObservableProperty]
    public partial string CloseButtonText { get; set; } = "Close";

    [ObservableProperty]
    public partial bool IsPrimaryButtonEnabled { get; set; } = true;

    [ObservableProperty]
    public partial bool IsSecondaryButtonEnabled { get; set; } = true;

    [ObservableProperty]
    public partial ContentDialogButton DefaultButton { get; set; } = ContentDialogButton.Primary;

    [ObservableProperty]
    public partial bool IsChild { get; set; } = true;

    [ObservableProperty]
    public partial bool IsModal { get; set; } = true;

    public bool PrimaryButtonNotClose { get; set; }

    public bool SecondaryButtonNotClose { get; set; }

    public bool CenterInParent { get; set; } = true;
}
