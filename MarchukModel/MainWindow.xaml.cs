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

        public MainWindow()
        {
            model = new ModelCalculator();
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            model.v0 = Double.Parse(vTextBox.Text);
            model.c0 = Double.Parse(cTextBox.Text);
            model.f0 = Double.Parse(fTextBox.Text);
            Console.WriteLine(model.v0 + " " + model.c0 + " " + model.f0);
        }
    }
}
