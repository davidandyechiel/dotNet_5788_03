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
            InkManager inker = new InkManager();
            inker.label.Content = "Time: " + e.Date.ToString() + "\n" + e.Name + e.Error;
            inker.Show();
        }

        private void button_Click(object sender, PrinterEventArgs e)
        {
           // ((PrinterUC)sender).PageCount += int.Parse(e.Error);
            Close();
        }

    }
}
