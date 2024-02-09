using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml.Linq;

namespace BingoGenerator.UI.UserControls
{
    public class CheckBoxStateChangedEventArgs : EventArgs
    {
        public bool NewValue { get; }

        public CheckBoxStateChangedEventArgs(bool newValue)
        {
            NewValue = newValue;
        }
    }
    public partial class CheckBox : UserControl
    {
        public CheckBox()
        {
            InitializeComponent();
            Checked.Checked += StateChanged;
            Checked.Unchecked += StateChanged;
        }

        public event EventHandler<CheckBoxStateChangedEventArgs> StateChangedEvent;

        public bool IsChecked
        {
            get { return Checked.IsChecked == true; }
            set { Checked.IsChecked = value; }
        }

        public string NameContent
        {
            get { return BoxName.Text; }
            set { BoxName.Text = value; }
        }

        public Brush ForeGround
        {
            get { return Checked.Foreground; }
            set { Checked.Foreground = value; }
        }

        public double Fontsize
        {
            get { return Checked.FontSize; }
            set { Checked.FontSize = value; }
        }

        private void StateChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            StateChangedEvent?.Invoke(this, new CheckBoxStateChangedEventArgs(IsChecked));
        }
    }
}
