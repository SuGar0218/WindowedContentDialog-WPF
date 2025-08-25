using SuGarToolkit.WPF.Controls.Dialogs;

using System;
using System.Collections.Generic;

namespace SuGarToolkit.WPF.Sample.Dialogs.ViewModels;

public class EnumValues
{
    public static readonly MessageBoxButtons[] MessageBoxButtons = Enum.GetValues<MessageBoxButtons>();

    public static readonly MessageBoxDefaultButton[] MessageBoxDefaultButtons = Enum.GetValues<MessageBoxDefaultButton>();

    public static readonly MessageBoxIcon[] MessageBoxImages = Enum.GetValues<MessageBoxIcon>();

    public static readonly List<ContentDialogButton> ContentDialogButtons = [.. Enum.GetValues<ContentDialogButton>()];
}
