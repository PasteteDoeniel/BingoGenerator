using BingoGenerator.Core;
using BingoGenerator.UI.Elements;
using BingoGenerator.UI.UserControls;
using System;
using System.Numerics;
using System.Windows.Controls;


namespace BingoGenerator.ViewCore.View
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : UserControl
    {
        SettingsManager Settings;
        public SettingsView()
        {
            InitializeComponent();

            Settings = SettingsManager.Get();

            Dimensions.TextContent.Text = Settings.GetRowCount().ToString();
            FieldSize.TextContent.Text = Settings.GetCellSize().ToString();
            FontSize.TextContent.Text = Settings.GetFontSize().ToString();
            Margin.TextContent.Text = Settings.GetMargin().X.ToString() + "," + Settings.GetMargin().Y.ToString();
            CheckBoxUIElement.SetIsChecked(IsBoldCheckbox, Settings.GetIsBold());
            CustomFields.TextContent.Text = Settings.GetCustomFields().ToString();
            BingoNumbers.TextContent.Text = Settings.GetNumbers().ToString();

            Dimensions.TextChangedEvent += Dimensions_TextChanged;
            FieldSize.TextChangedEvent += FieldSize_TextChanged;
            FontSize.TextChangedEvent += FontSize_TextChanged;
            Margin.TextChangedEvent += Margin_TextChanged;
            CustomFields.TextChangedEvent += CustomFields_TextChanged;
            BingoNumbers.TextChangedEvent += BingoNumbers_TextChanged;
        }

        private void IsBoldCheckbox_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            Settings.SetIsBold(CheckBoxUIElement.GetIsChecked(IsBoldCheckbox));
        }

        private void Dimensions_TextChanged(object sender, TextFieldChangedEventArgs e)
        {
            Settings.SetRowCount(int.Parse(e.NewText));
        }
        private void FieldSize_TextChanged(object sender, TextFieldChangedEventArgs e)
        {
            Settings.SetCellSize(float.Parse(e.NewText));
        }

        private void FontSize_TextChanged(object sender, TextFieldChangedEventArgs e)
        {
            Settings.SetFontSize(int.Parse(e.NewText));
        }

        private void Margin_TextChanged(object sender, TextFieldChangedEventArgs e)
        {
            string[] NewMargin = e.NewText.Split(",");
            Settings.SetMargin(new Vector2(int.Parse(NewMargin[0]), int.Parse(NewMargin[1])));
        }
        private void CustomFields_TextChanged(object sender, TextFieldChangedEventArgs e)
        {
            Settings.SetCustomFields(e.NewText);
        }
        private void BingoNumbers_TextChanged(object sender, TextFieldChangedEventArgs e)
        {
            Settings.SetNumbers(e.NewText);
        }
    }
}
