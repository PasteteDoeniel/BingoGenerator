using System;
using System.Windows;

namespace BingoGenerator.UI.Elements
{
    public class ButtonUIElement : UIElement
    {
        public static readonly DependencyProperty ButtonCornerRadiusProperty =
        DependencyProperty.RegisterAttached(
        "ButtonCornerRadius",
        typeof(CornerRadius),
        typeof(ButtonUIElement),
        new FrameworkPropertyMetadata(default(CornerRadius),
        flags: FrameworkPropertyMetadataOptions.AffectsRender));

        // Declare a get accessor method.
        public static CornerRadius GetButtonCornerRadius(UIElement target) =>
            (CornerRadius)target.GetValue(ButtonCornerRadiusProperty);

        // Declare a set accessor method.
        public static void SetButtonCornerRadius(UIElement target, CornerRadius value) =>
            target.SetValue(ButtonCornerRadiusProperty, value);
    }
}
