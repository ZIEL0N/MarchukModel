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

            vTextBox.Text = model.v0.ToString();
            cTextBox.Text = model.c0.ToString();
            fTextBox.Text = model.f0.ToString();
            maxtTextBox.Text = model.maxt.ToString();
            alfaTextBox.Text = model.a.ToString();
            betaTextBox.Text = model.b.ToString();
            gammaTextBox.Text = model.g.ToString();
            tetaTextBox.Text = model.tt.ToString();
            rhoTextBox.Text = model.r.ToString();
            etaTextBox.Text = model.n.ToString();
            mfTextBox.Text = model.mf.ToString();
            mcTextBox.Text = model.mc.ToString();
            mmTextBox.Text = model.mm.ToString();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            DataContext = null;
            SeriesCollection = null;
            Labels = null;
            model.v0 = Double.Parse(vTextBox.Text.Replace('.',','));
            model.c0 = Double.Parse(cTextBox.Text.Replace('.', ','));
            model.f0 = Double.Parse(fTextBox.Text.Replace('.', ','));

            model.maxt = Double.Parse(maxtTextBox.Text.Replace('.', ','));
            model.a = Double.Parse(alfaTextBox.Text.Replace('.', ','));
            model.b = Double.Parse(betaTextBox.Text.Replace('.', ','));
            model.g = Double.Parse(gammaTextBox.Text.Replace('.', ','));
            model.tt = Double.Parse(tetaTextBox.Text.Replace('.', ','));
            model.r = Double.Parse(rhoTextBox.Text.Replace('.', ','));
            model.n = Double.Parse(etaTextBox.Text.Replace('.', ','));
            model.mf = Double.Parse(mfTextBox.Text.Replace('.', ','));
            model.mc = Double.Parse(mcTextBox.Text.Replace('.', ','));
            model.mm = Double.Parse(mmTextBox.Text.Replace('.', ','));


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
