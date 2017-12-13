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
using System.Windows.Shapes;

namespace dotNet_5778_03_0520_0473
{
    /// <summary>
    /// Interaction logic for InkManager.xaml
    /// </summary>
    public partial class InkManager : Window
    {
        PrinterUC printer;
        public InkManager()
        {
            InitializeComponent();
        }

        public InkManager(PrinterUC printer)
        {
            this.printer = printer;
            this.printer.InkMissing += missingInk;
            InitializeComponent();
        }

        private void missingInk(object sender, PrinterEventArgs e)
        {
            printer = ((PrinterUC)sender); // set the printer of this class. 
            InkManager inker = new InkManager();

            if (e.Critical) // add image of Error or warning depending on the critical.
                Critical_Img.Source = new BitmapImage(new Uri("images/Error.jpeg", UriKind.Relative));
            else Critical_Img.Source = new BitmapImage(new Uri("images/warning.jpg", UriKind.Relative));
            inker.label.Content = "Time: " + e.Date.ToString() + "\n" + e.Name + "missing " + e.Error + "% of ink";
            inker.Show();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (printer.InkCount == 0)
                printer.addInk();
            Close();
        }


    }
}
