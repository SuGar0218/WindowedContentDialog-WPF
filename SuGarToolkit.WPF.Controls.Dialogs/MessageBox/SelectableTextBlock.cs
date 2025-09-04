using SuGarToolkit.WPF.SourceGenerators;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace SuGarToolkit.WPF.Controls.Dialogs;

[ContentProperty(nameof(Text))]
public partial class SelectableTextBlock : Control
{
    static SelectableTextBlock() => DefaultStyleKeyProperty.OverrideMetadata(typeof(SelectableTextBlock), new FrameworkPropertyMetadata(typeof(SelectableTextBlock)));

    [DependencyProperty]
    public partial string Text { get; set; }

    [DependencyProperty]
    public partial TextWrapping TextWrapping { get; set; }
}
