using KursNBP.Lib;

using System;
using System.Linq;
using System.Windows;

namespace KursNBP.Dekstop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var args = new string[]
            {
                CurrencyForm.Text,
                StartDateForm.Text,
                EndDateForm.Text
            };
            try
            {
                ValidateInput(args);
            }
            catch (Exception)
            {
                MessageBox.Show("The entered data is incorrect", "Input error", MessageBoxButton.OK);
                return;
            }

            try
            {
                NBPExchangeRate exchange = new NBPExchangeRate(args[0].ToUpper(), DateTime.Parse(args[1]), DateTime.Parse(args[2]));
                TextToDisplay.Text = exchange.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("The system encountered an error. Please try again with other data." + System.Environment.NewLine + System.Environment.NewLine + "Error: " + ex.Message, "System error", MessageBoxButton.OK);
                return;
            }
        }

        private void ValidateInput(string[] args)
        {
            if (args == null || args.Length == 0)
                throw new ArgumentNullException();

            if (!AvailableCurrency.AvailabeCurrency.Any(x => x == args[0].ToUpper()))
                throw new ArgumentException("Not available or bad currency!");

            if (!DateTime.TryParse(args[1], out var startDate))
                throw new ArgumentException("Start date is in bad date format!");

            if (!DateTime.TryParse(args[2], out var endDate))
                throw new ArgumentException("End date is in bad date format!");

            if (startDate > endDate)
                throw new ArgumentException("End date cannot be later than start date!");

            if (startDate > DateTime.Now || endDate > DateTime.Now)
                throw new ArgumentOutOfRangeException("Unable to give a future date!");
        }
    }
}
