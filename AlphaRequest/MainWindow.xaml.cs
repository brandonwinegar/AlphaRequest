using ClassLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClassLib
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Label> YAxixLabels = new List<Label>();
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            /* Valid values for Series:
             * TIME_SERIES_INTRADAY
             * TIME_SERIES_DAILY
             * TIME_SERIES_WEEKLY
             * TIME_SERIES_MONTHLY
             * GLOBAL_QUOTE*/

            /*DataService GoogData = new DataService("GOOG");
            GoogData.BuildStandardSets("TIME_SERIES_DAILY");
            GoogData.BuildCurrentSet();
            UpdateDisplayBox(GoogData);*/
            YAxixLabels.Add(YAxis0);
            YAxixLabels.Add(YAxis1);
            YAxixLabels.Add(YAxis2);
            YAxixLabels.Add(YAxis3);
            YAxixLabels.Add(YAxis4);
            YAxixLabels.Add(YAxis5);
            YAxixLabels.Add(YAxis6);
            YAxixLabels.Add(YAxis7);
        }
        private void UpdateDisplayBox(DataService DataSet)
        {
            SymbolDisplay.Content = DataSet.Symbol;
            String[] Price = DataSet.CurrentData["03. high"].Split('.');
            DetailsDisplay.Content = $"${Price[0]}.{Price[1].Substring(2)}";
            //HundredDayHigh.Content = "100 Day High: " + DataSet.HundredDayHigh() + " USD";
        }
        private void RequestButton_Click(object sender, RoutedEventArgs e)
        {
            DataService StockData = new DataService(SymbolEntry.Text);
            try
            {
                StockData.BuildCurrentSet();
                UpdateDisplayBox(StockData);
                List<double> DataSet = StockData.GetDataSet(90);
                ChartRender TestChart = new ChartRender(ChartCanvas, YAxixLabels, DataSet);
                TestChart.Display();
            }
            catch (System.Collections.Generic.KeyNotFoundException)
            {
                SymbolDisplay.Content = "N/A";
                DetailsDisplay.Content = "";
            }
        }
        private void ClearGraph_Click(object sender, RoutedEventArgs e)
        {
            ChartCanvas.Children.Clear();
        }
    }
}
