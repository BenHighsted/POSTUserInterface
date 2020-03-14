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

        /// <summary>
        /// Runs when a user clicks the 'Scan' button. This method will run the input through the methods from the PointOfSaleTerminalLibrary.
        /// It will return the output into labels to be displayed on screen back for the user.
        /// </summary>
        /// <param name="sender">Contains a reference to the button 'Scan' that raises this event</param>
        /// <param name="e">Contains the event data</param>
        public void Scan(object sender, RoutedEventArgs e)
        {
            RunMethods run = new RunMethods();
            decimal result = run.RunCode(MyTextBox.Text);
            Label1.Content = MyTextBox.Text + " Totals to: $" + result;
            Label2.Content = "Overall Total: $" + CalculateOverallTotal(result) + "                            " +
                "Overall Savings: $" + CalculateOverallSavings(run.savings);
            MyTextBox.Clear();//clear the input box so the next input can be entered
        }

        /// <summary>
        /// Runs when a user clicks the 'Clear' button. This method will clear all data currently being displayed on screen from any previous inputs.
        /// All data on total and savings is wiped so the app is being used again like it was just started.
        /// </summary>
        /// <param name="sender">Contains a reference to the button 'Clear' that raises this event</param>
        /// <param name="e">Contains the event data</param>
        public void Clear(object sender, RoutedEventArgs e) 
        {
            Label1.Content = "";
            Label2.Content = "";
            overallTotal = 0.0M;
            overallSavings = 0.0M;
        }

        /// <summary>
        /// Keeps track of the total over multiple inputs. Will add the latest input into the total and return it.
        /// </summary>
        /// <param name="valueToAdd">The value to be added to total</param>
        /// <returns>Returns the total over all inputs so far.</returns>
        public decimal CalculateOverallTotal(decimal valueToAdd)
        {
            overallTotal += valueToAdd;
            return overallTotal;
        }

        /// <summary>
        ///  Keeps track of the savings over multiple inputs. Will add the latest input into the savings and return it.
        /// </summary>
        /// <param name="valueToAdd">The value to be added to savings</param>
        /// <returns>Returns the total savings over all inputs so far.</returns>
        public decimal CalculateOverallSavings(decimal valueToAdd)
        {
            overallSavings += valueToAdd;
            return overallSavings;
        }

    }

    public class RunMethods
    {
        public decimal savings = 0.0M;

        /// <summary>
        /// Runs the user input through the methods from the PointOfSaleTerminalLibrary
        /// </summary>
        /// <param name="input">The products to be scanned</param>
        /// <returns>Returns the total cost of the user input</returns>
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
