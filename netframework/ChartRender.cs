using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ClassLib
{
    public class ChartRender
    {
        Canvas Chart;
        List<Line> ChartLines = new List<Line>();
        List<Ellipse> EndPoints = new List<Ellipse>();
        List<Label> YAxisLabels = new List<Label>();
        List<double> Dataset;

        public ChartRender(Canvas canvas, List<Label> YAxisLabels, List<double> dataset)
        {
            this.Chart = canvas;
            this.Dataset = dataset;
            this.YAxisLabels = YAxisLabels;
        }
        public void Display()
        {
            double XInterval = Chart.ActualWidth / (Dataset.Count - 1);
            double MaxHeight = Chart.ActualHeight;
            double YMax = MaxVal(Dataset);
            double YMin = MinVal(Dataset);
            double YInterval = Chart.ActualHeight / (YMax - YMin);

            for (int i = 0; i < Dataset.Count - 1; i++)
            {
                ChartLines.Add(new Line());
                ChartLines[i].Stroke = System.Windows.Media.Brushes.Red;
                ChartLines[i].StrokeThickness = 3;
                ChartLines[i].X1 = i * XInterval;
                ChartLines[i].Y1 = (MaxHeight - (Dataset[i] - YMin) * YInterval);
                ChartLines[i].X2 = (i +1 ) * XInterval;
                ChartLines[i].Y2 = (MaxHeight - (Dataset[i + 1] - YMin) * YInterval);
                Chart.Children.Add(ChartLines[i]);
            }
            PopulateYAxisLabels();
        }
        private void PopulateYAxisLabels()
        {
            double YMax = MaxVal(Dataset);
            double YMin = MinVal(Dataset);
            double YRange = YMax - YMin;
            for (int i = 0; i < YAxisLabels.Count; i++)
            {
                YAxisLabels[i].Content = YMin + (double)i / 8 * YRange;
            }
        }
        public double MaxVal(List<double> MyList)
        {
            double MaxVal = 0;
            foreach(double Val in MyList)
            {
                if(Val > MaxVal)
                {
                    MaxVal = Val;
                }
            }
            return MaxVal;
        }
        public double MinVal(List<double> MyList)
        {
            double MinVal = MyList[0];
            foreach(double Val in MyList)
            {
                if(Val < MinVal)
                {
                    MinVal = Val;
                }
            }
            return MinVal;
        }
    }
}
