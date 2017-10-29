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
using LiveCharts;
using LiveCharts.Wpf;

namespace MarchukModel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ModelCalculator model;
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }

        public MainWindow()
        {
            model = new ModelCalculator();
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            DataContext = null;
            SeriesCollection = null;
            Labels = null;
            model.v0 = Double.Parse(vTextBox.Text);
            model.c0 = Double.Parse(cTextBox.Text);
            model.f0 = Double.Parse(fTextBox.Text);
            if (model.Simulate())
            {
                SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "V(t)",
                    Values = new ChartValues<double>(model.vt),
                    PointGeometry = null,
                    PointGeometrySize = 15
                },
                new LineSeries
                {
                    Title = "C(t)",
                    Values = new ChartValues<double>(model.ct),
                    PointGeometry = null,
                    PointGeometrySize = 15
                },
                new LineSeries
                {
                    Title = "F(t)",
                    Values = new ChartValues<double>(model.ft),
                    PointGeometry = null,
                    PointGeometrySize = 15
                }
            };
                string[] lbl = new string[model.vt.Count];
                for (int i = 0; i < lbl.Length; i++) lbl[i] = i.ToString();

                Labels = lbl;
                //YFormatter = value => value.ToString("C");
                
                DataContext = this;
            }
        }
    }
}
