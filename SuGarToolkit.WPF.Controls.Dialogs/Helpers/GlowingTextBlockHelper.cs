using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace SuGarToolkit.WPF.Controls.Dialogs.Helpers;

public class GlowingTextBlockHelper
{
    public static bool GetUseGlowingTextBlock(FrameworkElement d) => (bool) d.GetValue(UseGlowingTextBlockProperty);

    public static void SetUseGlowingTextBlock(FrameworkElement d, bool value) => d.SetValue(UseGlowingTextBlockProperty, value);

    public static readonly DependencyProperty UseGlowingTextBlockProperty = DependencyProperty.RegisterAttached(
        "UseGlowingTextBlock",
        typeof(bool),
        typeof(DependencyObject),
        new PropertyMetadata(false, (d, e) =>
        {
            if ((bool) e.NewValue)
            {
                ((FrameworkElement) d).Loaded += ApplyDropShadowEffectOnLoaded;
            }
            else
            {
                ((FrameworkElement) d).Loaded -= ApplyDropShadowEffectOnLoaded;
            }
        }));

    private static void ApplyDropShadowEffect(DependencyObject root)
    {
        for (int i = 0; i < VisualTreeHelper.GetChildrenCount(root); i++)
        {
            DependencyObject? child = VisualTreeHelper.GetChild(root, i);
            if (child is TextBlock textBlock)
            {
                textBlock.Effect = effect;
            }
            else
            {
                ApplyDropShadowEffect(child);
            }
        }
    }

    private static void ApplyDropShadowEffectOnLoaded(object sender, RoutedEventArgs e) => ApplyDropShadowEffect((DependencyObject) sender);

    private static readonly DropShadowEffect effect = new()
    {
        Color = Colors.White,
        BlurRadius = 16,
        Opacity = 1.0,
        ShadowDepth = 0,
        RenderingBias = RenderingBias.Performance
    };
}
