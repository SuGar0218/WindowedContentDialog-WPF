using System.Windows;

namespace SuGarToolkit.WPF.Controls.Dialogs;

public class MessageBoxBaseOptions
{
    /// <summary>
    /// Disable the content of window behind when _dialog window shows.
    /// </summary>
    public bool DisableBehind { get; set; }

    //public ContentDialogSmokeLayerKind SmokeLayerKind { get; set; }

    public UIElement? CustomSmokeLayer { get; set; }

    public FlowDirection FlowDirection { get; set; }
}
