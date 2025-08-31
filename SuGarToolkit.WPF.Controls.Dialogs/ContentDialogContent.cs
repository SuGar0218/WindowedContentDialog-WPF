using SuGarToolkit.WPF.SourceGenerators;

using System.Windows;
using System.Windows.Controls;

namespace SuGarToolkit.WPF.Controls.Dialogs;

public partial class ContentDialogContent : ContentControl
{
    static ContentDialogContent() => DefaultStyleKeyProperty.OverrideMetadata(typeof(ContentDialogContent), new FrameworkPropertyMetadata(typeof(ContentDialogContent)));

    public ContentDialogContent() : base()
    {
        Loaded += OnLoaded;
        Unloaded += (o, e) => countCustomMeasureAfterLoaded = 0;
    }

    private Button PrimaryButton;
    private Button SecondaryButton;
    private Button CloseButton;

    public event RoutedEventHandler? PrimaryButtonClick;
    public event RoutedEventHandler? SecondaryButtonClick;
    public event RoutedEventHandler? CloseButtonClick;

    public UIElement TitleArea { get; private set; }
    public Grid DialogSpace { get; private set; }
    public Border CommandSpace { get; private set; }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        TitleArea = (UIElement) GetTemplateChild(nameof(TitleArea));
        DialogSpace = (Grid) GetTemplateChild(nameof(DialogSpace));
        CommandSpace = (Border) GetTemplateChild(nameof(CommandSpace));

        PrimaryButton = (Button) GetTemplateChild(nameof(PrimaryButton));
        SecondaryButton = (Button) GetTemplateChild(nameof(SecondaryButton));
        CloseButton = (Button) GetTemplateChild(nameof(CloseButton));

        PrimaryButton.Click += (sender, args) => PrimaryButtonClick?.Invoke(sender, args);
        SecondaryButton.Click += (sender, args) => SecondaryButtonClick?.Invoke(sender, args);
        CloseButton.Click += (sender, args) => CloseButtonClick?.Invoke(sender, args);
    }

    private void OnLoaded(object sender, RoutedEventArgs args)
    {
        ButtonsVisibilityState = DetermineButtonsVisibilityState();
        DefaultButtonState = DetermineDefaultButtonState();
    }

    /// <summary>
    /// Whether customized measurement in MeasureOverride is needed.
    /// <br/>
    /// This variable is set to avoid redundant calculations.
    /// <br/>
    /// If the first measurement after Loaded is finished, there will be no need for customized measurement until Unloaded.
    /// </summary>
    private int countCustomMeasureAfterLoaded;

    protected override Size MeasureOverride(Size availableSize)
    {
        if (countCustomMeasureAfterLoaded > 2)
            return base.MeasureOverride(availableSize);

        if (IsLoaded)
            countCustomMeasureAfterLoaded++;

        return CustomMeasure(availableSize);
    }

    private Size CustomMeasure(Size availableSize)
    {
        int countButtons = 0;
        double buttonLongestWidth = 0.0;
        double buttonMaxWidth = (double) Application.Current.Resources["ContentDialogButtonMaxWidth"];
        if (PrimaryButton.Visibility is Visibility.Visible)
        {
            PrimaryButton.Measure(availableSize);
            buttonLongestWidth = Math.Min(Math.Max(buttonLongestWidth, PrimaryButton.DesiredSize.Width), buttonMaxWidth);
            countButtons++;
        }
        if (SecondaryButton.Visibility is Visibility.Visible)
        {
            SecondaryButton.Measure(availableSize);
            buttonLongestWidth = Math.Min(Math.Max(buttonLongestWidth, SecondaryButton.DesiredSize.Width), buttonMaxWidth);
            countButtons++;
        }
        if (CloseButton.Visibility is Visibility.Visible)
        {
            CloseButton.Measure(availableSize);
            buttonLongestWidth = Math.Min(Math.Max(buttonLongestWidth, CloseButton.DesiredSize.Width), buttonMaxWidth);
            countButtons++;
        }

        double commandSpaceExpectedWidth = CommandSpace.Padding.Left + CommandSpace.Padding.Right
            + countButtons * buttonLongestWidth
            + (countButtons - 1) * ((GridLength) Application.Current.Resources["ContentDialogButtonSpacing"]).Value;

        double minWidth = Math.Max((double) Application.Current.Resources["ContentDialogMinWidth"], commandSpaceExpectedWidth);
        double maxWidth = Math.Max((double) Application.Current.Resources["ContentDialogMaxWidth"], commandSpaceExpectedWidth);
        if (availableSize.Width > maxWidth)
        {
            availableSize.Width = maxWidth;
        }
        Size desiredSize = base.MeasureOverride(availableSize);
        if (desiredSize.Width < minWidth)
        {
            desiredSize.Width = minWidth;
        }
        return desiredSize;
    }

    public void AfterGotFocus()
    {
        //VisualStateManager.GoToState(this, DefaultButtonState.ToString(), false);
        DefaultButtonState = DefaultButton;
    }

    public void AfterLostFocus()
    {
        //VisualStateManager.GoToState(this, "NoDefaultButton", false);
        DefaultButtonState = ContentDialogButton.None;
    }

    [DependencyProperty]
    internal partial ContentDialogButtonsVisibilityState ButtonsVisibilityState { get; set; }

    [DependencyProperty]
    internal partial ContentDialogButton DefaultButtonState { get; set; }

    private ContentDialogButtonsVisibilityState DetermineButtonsVisibilityState()
    {
        if (!string.IsNullOrEmpty(PrimaryButtonText) && !string.IsNullOrEmpty(SecondaryButtonText) && !string.IsNullOrEmpty(CloseButtonText))
        {
            //VisualStateManager.GoToState(this, "AllVisible", false);
            //IsPrimaryButtonEnabled = true;
            //IsSecondaryButtonEnabled = true;
            return ContentDialogButtonsVisibilityState.AllVisible;
        }
        else if (!string.IsNullOrEmpty(PrimaryButtonText))
        {
            if (!string.IsNullOrEmpty(SecondaryButtonText))
            {
                //VisualStateManager.GoToState(this, "PrimaryAndSecondaryVisible", false);
                //IsPrimaryButtonEnabled = true;
                IsSecondaryButtonEnabled = true;
                return ContentDialogButtonsVisibilityState.PrimaryAndSecondaryVisible;
            }
            else if (!string.IsNullOrEmpty(CloseButtonText))
            {
                //VisualStateManager.GoToState(this, "PrimaryAndCloseVisible", false);
                //IsPrimaryButtonEnabled = true;
                IsSecondaryButtonEnabled = false;
                return ContentDialogButtonsVisibilityState.PrimaryAndCloseVisible;
            }
            else
            {
                //VisualStateManager.GoToState(this, "PrimaryVisible", false);
                //IsPrimaryButtonEnabled = true;
                IsSecondaryButtonEnabled = false;
                return ContentDialogButtonsVisibilityState.PrimaryVisible;
            }
        }
        else if (!string.IsNullOrEmpty(SecondaryButtonText))
        {
            IsPrimaryButtonEnabled = false;
            if (!string.IsNullOrEmpty(CloseButtonText))
            {
                //VisualStateManager.GoToState(this, "SecondaryAndCloseVisible", false);
                return ContentDialogButtonsVisibilityState.SecondaryAndCloseVisible;
            }
            else
            {
                //VisualStateManager.GoToState(this, "SecondaryVisible", false);
                return ContentDialogButtonsVisibilityState.SecondaryAndCloseVisible;
            }
            //IsSecondaryButtonEnabled = true;
        }
        else if (!string.IsNullOrEmpty(CloseButtonText))
        {
            //VisualStateManager.GoToState(this, "CloseVisible", false);
            return ContentDialogButtonsVisibilityState.CloseVisible;
        }
        else
        {
            //VisualStateManager.GoToState(this, "NoneVisible", false);
            IsPrimaryButtonEnabled = false;
            IsSecondaryButtonEnabled = false;
            return ContentDialogButtonsVisibilityState.NoneVisible;
        }
    }

    private ContentDialogButton DetermineDefaultButtonState()
    {
        return DefaultButton;
    }

    [DependencyProperty]
    public partial object? Title { get; set; }

    [DependencyProperty]
    public partial DataTemplate? TitleTemplate { get; set; }

    [DependencyProperty]
    public partial string? PrimaryButtonText { get; set; }

    [DependencyProperty]
    public partial string? SecondaryButtonText { get; set; }

    [DependencyProperty]
    public partial string? CloseButtonText { get; set; }

    [DependencyProperty(DefaultValue = true)]
    public partial bool IsPrimaryButtonEnabled { get; set; }

    [DependencyProperty(DefaultValue = true)]
    public partial bool IsSecondaryButtonEnabled { get; set; }

    [DependencyProperty(DefaultValue = ContentDialogButton.Close)]
    public partial ContentDialogButton DefaultButton { get; set; }

    [DependencyProperty(DefaultValuePath = nameof(DefaultButtonStyle))]
    public partial Style? PrimaryButtonStyle { get; set; }

    [DependencyProperty(DefaultValuePath = nameof(DefaultButtonStyle))]
    public partial Style? SecondaryButtonStyle { get; set; }

    [DependencyProperty(DefaultValuePath = nameof(DefaultButtonStyle))]
    public partial Style? CloseButtonStyle { get; set; }

    [DependencyProperty]
    public partial CornerRadius CornerRadius { get; set; }

    private static Style DefaultButtonStyle => (Style) Application.Current.Resources["DefaultButtonStyle"];
}

internal enum ContentDialogButtonsVisibilityState
{
    NoneVisible,
    PrimaryVisible,
    SecondaryVisible,
    CloseVisible,
    PrimaryAndSecondaryVisible,
    PrimaryAndCloseVisible,
    SecondaryAndCloseVisible,
    AllVisible,
}
