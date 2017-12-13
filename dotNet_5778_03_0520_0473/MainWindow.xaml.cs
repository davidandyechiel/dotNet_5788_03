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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace dotNet_5778_03_0520_0473
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>


    
    public partial class MainWindow : Window
    {

        PrinterUC printer;
        Queue<PrinterUC> queue;
        SolidColorBrush brush = new SolidColorBrush();
        public MainWindow()
        {
            printer = Printer1;
            queue = new Queue<PrinterUC>();
            InitializeComponent();
        }

        private void PrinterUC_Loaded_2(object sender, RoutedEventArgs e)
        {

        }

        private void PrinterUC_Loaded_1(object sender, RoutedEventArgs e)
        {
            
        }

        private void PrinterUC_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void printButton_Click(object sender, RoutedEventArgs e)
        {
            
            printer.print();
        }

        private void Printer1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
          //  queue.Enqueue(Printer1);
        }

        private void Printer2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Foreground = new SolidColorBrush(Colors.LightBlue);
            queue.Enqueue(Printer1);
        }

        private void Printer3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {



         //   queue.Enqueue(Printer1);
        }
        
    }
}
