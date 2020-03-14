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

using PointOfSaleTerminalLibrary;

namespace PointOfSaleTerminalUserInterface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public decimal overallTotal = 0.0M, overallSavings = 0.0M;
        public MainWindow()
        {
            InitializeComponent();
        }

        public void Scan(object sender, RoutedEventArgs e)
        {
            RunMethods run = new RunMethods();
            decimal result = run.RunCode(MyTextBox.Text);
            Label1.Content = MyTextBox.Text + " Totals to: $" + result; //This is where the total should go
            Label2.Content = "Overall Total: $" + CalculateOverallTotal(result) + "                            Overall Savings: $" + CalculateOverallSavings(run.savings);
            MyTextBox.Clear();
        }

        public void Clear(object sender, RoutedEventArgs e) 
        {
            Label1.Content = "";
            Label2.Content = "";
            overallTotal = 0.0M;
            overallSavings = 0.0M;
        }

        public decimal CalculateOverallTotal(decimal valueToAdd)
        {
            overallTotal += valueToAdd;
            return overallTotal;
        }

        public decimal CalculateOverallSavings(decimal valueToAdd)
        {
            overallSavings += valueToAdd;
            return overallSavings;
        }

    }

    public class RunMethods
    {
        public decimal savings = 0.0M;
        public decimal RunCode(String input)
        {
            String[] inputs = input.Split(',');

            PointOfSaleTerminal terminal = new PointOfSaleTerminal();
            terminal.SetPricing(1.25M, 4.25M, 1.00M, 0.75M);
            foreach (string v in inputs)
            {
                terminal.ScanProduct(v);
            }
            decimal result = terminal.CalculateTotal();
            savings = terminal.CalculateSavings();

            return result;
        }

    }
}
