using BingoGenerator.Core;
using System.Diagnostics;
using System.Numerics;
using System.Windows;
using System.Xml;

namespace BingoGenerator.ViewCore.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BingoPreview preview;

        private int CurrentRowCount;
        private float CurrentCellSize;
        private int CurrentFontSize;
        private Vector2 CurrentMargin;
        private bool CurrentIsBold;
        private string CurrentCustomFields;
        private string CurrentNumbers;

        public MainWindow()
        {
            InitializeComponent();
            SettingsManager s = SettingsManager.Get();
            preview = new BingoPreview();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            SaveManager.Get().Save();
            System.Windows.Application.Current.Shutdown();
        }

        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            preview.Generate();
            preview.Show();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            preview.PrintBingoBoard();
        }

        private void Window_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}