using System;
using System.ComponentModel;
using System.Windows;

namespace BingoGenerator.UI.Elements
{
    public class CheckBoxUIElement : UIElement
    {
        public static readonly DependencyProperty IsCheckedProperty =
        DependencyProperty.RegisterAttached(
        "IsChecked",
        typeof(bool),
        typeof(CheckBoxUIElement),
        new FrameworkPropertyMetadata(default(bool),
        flags: FrameworkPropertyMetadataOptions.AffectsRender));

        // Declare a get accessor method.
        public static bool GetIsChecked(UIElement target) =>
            (bool)target.GetValue(IsCheckedProperty);

        // Declare a set accessor method.
        public static void SetIsChecked(UIElement target, bool value) =>
            target.SetValue(IsCheckedProperty, value);
    }
}
