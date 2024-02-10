using System;
using System.Numerics;
using System.Xml;

namespace BingoGenerator.Core
{
    public sealed class SettingsManager : ISaveInterface
    {
        private static SettingsManager? SettingsManagerInstance;

        private int CurrentRowCount = Constants.DefaultRowCount;
        private float CurrentCellSize = Constants.DefaultCellSize;
        private int CurrentFontSize = Constants.DefaultFontSize;
        private Vector2 CurrentMargin = Constants.DefaultMargin;
        private bool CurrentIsBold = Constants.DefaultIsBold;
        private string CurrentCustomFields = Constants.DefaultCustomFields;
        private string CurrentNumbers = Constants.DefaultNumbers;

        private SettingsManager() 
        {
            SaveManager.Get().Register((ISaveInterface)this);
            SaveManager.Get().Load();
        }

        public static SettingsManager Get()
        {
            if (SettingsManagerInstance == null)
            {
                SettingsManagerInstance = new SettingsManager();
            }
            return SettingsManagerInstance;
        }

        public int GetRowCount() { return CurrentRowCount; }
        public float GetCellSize() { return CurrentCellSize; }
        public int GetFontSize() { return CurrentFontSize; }
        public Vector2 GetMargin() { return CurrentMargin; }
        public bool GetIsBold() { return CurrentIsBold; }
        public string GetCustomFields() { return CurrentCustomFields; }
        public string GetNumbers() { return CurrentNumbers; }
        public void SetRowCount(int NewRowCount) { CurrentRowCount = NewRowCount; }
        public void SetCellSize(float NewCellSize) { CurrentCellSize = NewCellSize; }
        public void SetFontSize(int NewFontSize) { CurrentFontSize = NewFontSize; } 
        public void SetMargin(Vector2 NewMargin) { CurrentMargin = NewMargin; }
        public void SetIsBold(bool NewIsBold) { CurrentIsBold = NewIsBold; }
        public void SetCustomFields(string NewCustomFields) { CurrentCustomFields = NewCustomFields; }
        public void SetNumbers(string NewNumbers) { CurrentNumbers = NewNumbers; }
        public void Save(ref SaveDocument saveArchive)
        {
            XmlElement SettingsXML = saveArchive.CreateElement("Settings");


            XmlElement SRowNumbers = saveArchive.CreateElement("RowNumbers");
            SRowNumbers.InnerText = GetRowCount().ToString();
            SettingsXML.AppendChild(SRowNumbers);

            XmlElement SCellSize = saveArchive.CreateElement("CellSize");
            SCellSize.InnerText = GetCellSize().ToString();
            SettingsXML.AppendChild(SCellSize);

            XmlElement SFontSize = saveArchive.CreateElement("FontSize");
            SFontSize.InnerText = GetFontSize().ToString();
            SettingsXML.AppendChild(SFontSize);

            XmlElement SMargin = saveArchive.CreateElement("Margin");
            SMargin.InnerText = GetMargin().X.ToString() + "," + GetMargin().Y.ToString();
            SettingsXML.AppendChild(SMargin);

            XmlElement SIsBold = saveArchive.CreateElement("IsBold");
            SIsBold.InnerText = GetIsBold() ? "1" : "0";
            SettingsXML.AppendChild(SIsBold);

            XmlElement SCustomNumbers = saveArchive.CreateElement("CustomNumbers");
            SCustomNumbers.InnerText = GetCustomFields();
            SettingsXML.AppendChild(SCustomNumbers);

            XmlElement SBingoNumbers = saveArchive.CreateElement("BingoNumbers");
            SBingoNumbers.InnerText = GetNumbers();
            SettingsXML.AppendChild(SBingoNumbers);

            saveArchive.AppendChild(SettingsXML);
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
                        SetRowCount(int.Parse(Setting.InnerText));
                    }
                    else if (Setting.Name == "CellSize")
                    {
                        SetCellSize(float.Parse(Setting.InnerText));
                    }
                    else if (Setting.Name == "FontSize")
                    {
                        SetFontSize(int.Parse(Setting.InnerText));
                    }
                    else if (Setting.Name == "Margin")
                    {
                        string[] MarginSplit = Setting.InnerText.Split(',');
                        SetMargin(new Vector2(float.Parse(MarginSplit[0]), float.Parse(MarginSplit[1])));
                    }
                    else if (Setting.Name == "IsBold")
                    {
                        SetIsBold(Setting.InnerText == "1");
                    }
                    else if (Setting.Name == "CustomNumbers")
                    {
                        SetCustomFields(Setting.InnerText);
                    }
                    else if (Setting.Name == "BingoNumbers")
                    {
                        SetNumbers(Setting.InnerText);
                    }
                }
            }
        }
    }
}
