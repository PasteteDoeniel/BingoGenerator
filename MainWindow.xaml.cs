﻿using System.ComponentModel;
using System.Numerics;
using System.Windows;
using System.Xml;

namespace BingoGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, ISaveInterface
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
            preview = new BingoPreview();
            SaveManager.Get().Register((ISaveInterface)this);
            SaveManager.Get().Load();
        }

        private void Button_Click_Generate(object sender, RoutedEventArgs e)
        {
            string[] content = BingoContent.Text.Split(",");

            if (content.Length != CurrentRowCount)
            {
                MessageBox.Show("Bingo Nummern und Reihen Anzal stimmen nicht überein", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
            if (CurrentRowCount % 2 == 0)
            {
                MessageBox.Show("Reihen Anzal muss ungerade sein", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            preview.Generate(content, CurrentCellSize, CurrentRowCount, CurrentFontSize, bIsBold.IsChecked == true, CurrentMargin, CurrentCustomFields);
            preview.Show();
        }

        private void Button_Click_Save(object sender, RoutedEventArgs e)
        {
            preview.PrintBingoBoard();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            SaveManager.Get().Save();
            System.Windows.Application.Current.Shutdown();
        }

        public void Save(ref SaveDocument saveArchive)
        {
            XmlElement Settings = saveArchive.CreateElement("Settings");

            XmlElement SRowNumbers = saveArchive.CreateElement("RowNumbers");
            SRowNumbers.InnerText = RowCount.Text;
            Settings.AppendChild(SRowNumbers);

            XmlElement SCellSize = saveArchive.CreateElement("CellSize");
            SCellSize.InnerText = CellSize.Text;
            Settings.AppendChild(SCellSize);

            XmlElement SFontSize = saveArchive.CreateElement("FontSize");
            SFontSize.InnerText = FontSize.Text;
            Settings.AppendChild(SFontSize);

            XmlElement SMargin = saveArchive.CreateElement("Margin");
            SMargin.InnerText = MarginLeft.Text + "," + MarginTop.Text;
            Settings.AppendChild(SMargin);

            XmlElement SIsBold = saveArchive.CreateElement("IsBold");
            SIsBold.InnerText = bIsBold.IsChecked == true ? "1" : "0";
            Settings.AppendChild(SIsBold);

            XmlElement SCustomNumbers = saveArchive.CreateElement("CustomNumbers");
            SCustomNumbers.InnerText = CustomNumbers.Text;
            Settings.AppendChild(SCustomNumbers);

            XmlElement SBingoNumbers = saveArchive.CreateElement("BingoNumbers");
            SBingoNumbers.InnerText = BingoContent.Text;
            Settings.AppendChild(SBingoNumbers);

            saveArchive.AppendChild(Settings);
        }

        public void Load(SaveDocument loadArchive)
        {
            XmlNode SettingsEntry = new XmlDocument();
            if (loadArchive.TryGetEntry("Settings", ref SettingsEntry))
            {
                foreach (XmlNode Setting in SettingsEntry.ChildNodes)
                {
                    if (Setting.Name == "RowNumbers")
                    {
                        CurrentRowCount = int.Parse(Setting.InnerText);
                    }
                    else if (Setting.Name == "CellSize")
                    {
                        CurrentCellSize = float.Parse(Setting.InnerText);
                    }
                    else if (Setting.Name == "FontSize")
                    {
                        CurrentFontSize = int.Parse(Setting.InnerText);
                    }
                    else if (Setting.Name == "Margin")
                    {
                        string[] MarginSplit = Setting.InnerText.Split(',');
                        CurrentMargin = new Vector2(float.Parse(MarginSplit[0]), float.Parse(MarginSplit[1]));
                    }
                    else if (Setting.Name == "IsBold")
                    {
                        CurrentIsBold = Setting.InnerText == "1";
                    }
                    else if (Setting.Name == "CustomNumbers")
                    {
                        CurrentCustomFields = Setting.InnerText;
                    }
                    else if (Setting.Name == "BingoNumbers")
                    {
                        CurrentNumbers = Setting.InnerText;
                    }
                }
            }
            else
            {
                CurrentRowCount = Constants.DefaultRowCount;
                CurrentCellSize = Constants.DefaultCellSize;
                CurrentFontSize = Constants.DefaultFontSize;
                CurrentMargin = Constants.DefaultMargin;
                CurrentIsBold = Constants.DefaultIsBold;
                CurrentCustomFields = Constants.DefaultCustomFields;
                CurrentNumbers = Constants.DefaultNumbers;
            }

            RowCount.Text = CurrentRowCount.ToString();
            CellSize.Text = CurrentCellSize.ToString();
            FontSize.Text = CurrentFontSize.ToString();
            MarginLeft.Text = CurrentMargin.X.ToString();
            MarginTop.Text = CurrentMargin.Y.ToString();
            bIsBold.IsChecked = CurrentIsBold;
            CustomNumbers.Text = CurrentCustomFields.ToString();
            BingoContent.Text = CurrentNumbers.ToString();
        }
    }
}