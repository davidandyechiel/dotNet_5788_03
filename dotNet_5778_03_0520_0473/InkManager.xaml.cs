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
        private static PrinterUC printer;
        public InkManager()
        {
            InitializeComponent();
        }

        public InkManager(PrinterUC _printer)
        {
            _printer.InkMissing += missingInk; // add missing int to the event of the current printer 
            InitializeComponent();
        }


        private void missingInk(object sender, PrinterEventArgs e)
        {
            printer = ((PrinterUC)sender); // set the printer of this class. 
            InkManager inker = new InkManager();
            
            if (e.Critical) // add image of Error or warning depending on the critical.
                inker.Critical_Img.Source = new BitmapImage(new Uri("images/Error.jpeg", UriKind.Relative));
            else inker.Critical_Img.Source = new BitmapImage(new Uri("images/warning.jpg", UriKind.Relative));

            inker.label.Content = "Time: " + e.Date.ToString() + "\n" + e.Name + "missing " + e.Error + "% of ink"; // print the label
            inker.Show();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
          //  if (printer.InkCount <= 0) // if the container is empty then add more ink.
                printer.addInk();
            Close();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
