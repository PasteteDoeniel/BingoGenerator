using System;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Media;


namespace BingoGenerator.UI.UserControls
{
    public class TextFieldChangedEventArgs : EventArgs
    {
        public string NewText { get; }

        public TextFieldChangedEventArgs(string newText)
        {
            NewText = newText;
        }
    }

    public partial class TextField : UserControl
    {
        public event EventHandler<TextFieldChangedEventArgs> TextChangedEvent;

        public string UserTextContent
        {
            get { return TextContent.Text; }
            set { TextContent.Text = value; }
        }

        public string FieldContent
        {
            get { return FieldName.Text; }
            set { 
                    FieldName.Text = value;
                }
        }

        public Brush ForeGround
        {
            get { return FieldName.Foreground; }
            set { FieldName.Foreground = value; }
        }

        public double FontSize
        {
            get { return FieldName.FontSize; }
            set { FieldName.FontSize = value; }
        }

        public TextField()
        {
            InitializeComponent();
            TextContent.TextChanged += TextChanged;
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            TextChangedEvent?.Invoke(this, new TextFieldChangedEventArgs(TextContent.Text));
        }
    }
}
