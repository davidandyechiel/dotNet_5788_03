﻿using System;
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
    /// Interaction logic for PageManager.xaml
    /// </summary>
    public partial class PageManager : Window
    {
        private static PrinterUC  printer;
        public PageManager()
        {
            InitializeComponent();
        }

        public PageManager(PrinterUC _printer)
        {
            _printer.PageMissing += missingPages; // add missing int to the event of the current printer 
            InitializeComponent();
        }

        private void missingPages(object sender, PrinterEventArgs e)
        {
            printer = ((PrinterUC)sender);
            PageManager pager = new PageManager();
            pager.label.Content = "Time: " + e.Date.ToString() + "\n" + e.Name + " missing: " +  e.Error + " pages"; // set the lable
            pager.Show();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
           printer.addPages();
            Close();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
