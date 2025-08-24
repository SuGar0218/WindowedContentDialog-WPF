using SuGarToolkit.WPF.SourceGenerators;

using System.Windows;
using System.Windows.Controls;

namespace SuGarToolkit.WPF.Controls.Dialogs;

public partial class MessageBoxHeader : Control
{
    [DependencyProperty]
    public partial string? Text { get; set; }

    [DependencyProperty<MessageBoxIcon>(DefaultValue = MessageBoxIcon.None)]
    public partial MessageBoxIcon Icon { get; set; }

    static MessageBoxHeader()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(MessageBoxHeader), new FrameworkPropertyMetadata(typeof(MessageBoxHeader)));
    }
}