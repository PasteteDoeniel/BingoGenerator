using System.Printing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Numerics;

namespace BingoGenerator
{
    /// <summary>
    /// Interaction logic for BingoPreview.xaml
    /// </summary>
    public partial class BingoPreview : Window
    {
        Dictionary<Vector2, string> CustomContentPairs;
        public BingoPreview()
        {
            InitializeComponent();
        }

        public void Generate(string[] bingoContent, float cellSize, int numCells, int fontSize, bool bIsBold, Vector2 marginGeneratedBoard, string customContent)
        {
            PreviewWindow.Children.Clear();

            int middleField = numCells/2;

            CustomContentPairs = GetCustomContent(customContent);

            for (int i = 0; i < numCells; i++)
            {
                DrawBingoHeader(i, marginGeneratedBoard, cellSize, fontSize);
                string[] MinMax = bingoContent[i].Split("-");
                int[] PossibleNumbers = RandNumbersGenerator(int.Parse(MinMax[0]), int.Parse(MinMax[1]), numCells);
                
                for (int j = 0; j < numCells; j++)
                {
                    string customField = FindCustomContent(new Vector2(i +1, j +1));
                    DrawBingoBox(new Vector2(marginGeneratedBoard.X + i * cellSize,
                        marginGeneratedBoard.Y + (j + 1) * cellSize),
                        new string(Convert.ToString(customField != null ? customField : PossibleNumbers[j])),
                        cellSize,
                        fontSize,
                        bIsBold);
                }
            }
        }

        private string FindCustomContent(Vector2 coords)
        {
            string ReturnContent;

            CustomContentPairs.TryGetValue(coords, out ReturnContent);

            return ReturnContent;
        }

        private Dictionary<Vector2, string> GetCustomContent(string customContent)
        {
            Dictionary<Vector2, string> ContentMap = new Dictionary<Vector2, string>();

            if (customContent.Length <= 0) { return ContentMap; }

            string[] Contents = customContent.Split(",");
            foreach (string Content in Contents)
            {
                string[] ContentSplit = Content.Split(":");
                string[] CoordString = ContentSplit[0].Split("/");
                ContentMap.Add(new Vector2(int.Parse(CoordString[0]), int.Parse(CoordString[1])), ContentSplit[1]);

            }
            return ContentMap;
        }

        private int[] RandNumbersGenerator(int min, int max, int num)
        {
            List<int> PossibleNumbers = new List<int>();
            int[] Numbers = new int[num];

            for (int i = min; i <= max; i++)
            {
                PossibleNumbers.Add(i);
            }
            PossibleNumbers = PossibleNumbers.OrderBy(x => Random.Shared.Next()).ToList();

            for (int i = 0; i < num; i++)
            {
                Numbers[i] = PossibleNumbers[i];
            }

            return Numbers;
        }

        private void DrawBingoBox(Vector2 location, string text, float size, int fontSize, bool bIsBold, bool bHasRectangle = true)
        {
            Grid BingoGrid = new Grid() { Width = size, Height = size };

            TextBlock BingoTextBlock = new TextBlock();
            BingoTextBlock.HorizontalAlignment = HorizontalAlignment.Center;
            BingoTextBlock.VerticalAlignment = VerticalAlignment.Center;
            BingoTextBlock.FontSize = fontSize;
            BingoTextBlock.Inlines.Add(bIsBold ? new Bold(new Run(text)) : new Run(text));

            if (bHasRectangle)
            {
                Rectangle BingoRectangle = new Rectangle() { Stretch = Stretch.Fill, Stroke = Brushes.Black };
                BingoGrid.Children.Add(BingoRectangle);
            }

            BingoGrid.Children.Add(BingoTextBlock);
            PreviewWindow.Children.Add(BingoGrid);

            Canvas.SetLeft(BingoGrid, location.X);
            Canvas.SetTop(BingoGrid, location.Y);
        }

        private void DrawBingoHeader(int collumn, Vector2 marginGeneratedBoard, float cellSize, int fontSize)
        {
            string HeaderText = new string("");
            switch(collumn)
            {
                case 0:
                    HeaderText = "B";
                    break;
                case 1:
                    HeaderText = "I";
                    break;
                case 2:
                    HeaderText = "N";
                    break; 
                case 3:
                    HeaderText = "G";
                    break;
                case 4:
                    HeaderText = "O";
                    break;
            }
            DrawBingoBox(new Vector2(marginGeneratedBoard.X + collumn * cellSize, marginGeneratedBoard.Y), HeaderText, cellSize, fontSize, true, false);

        }
        public void PrintBingoBoard()
        {
            PrintDialog pd = new PrintDialog();
            pd.PrintQueue = new PrintQueue(new PrintServer(), "Microsoft Print to PDF");
            pd.PrintTicket.PageOrientation = PageOrientation.Portrait;
            pd.PrintTicket.PageScalingFactor = 100;
            pd.PrintVisual(PreviewWindow, "Nomograph");
        }
    }
}
