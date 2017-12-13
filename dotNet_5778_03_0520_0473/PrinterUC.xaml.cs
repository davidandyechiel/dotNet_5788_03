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
    /// Interaction logic for PrinterUC.xaml
    /// </summary>
    /// 
    
    public partial class PrinterUC : UserControl
    {
        const double MAX_INK = 100;
        const double MIN_ADD_INK = MAX_INK / 10.0;
        const double MAX_PRINT_INK = MAX_INK / 5.0;
        const int MAX_PAGES = 400;
        const int MIN_ADD_PAGES = MAX_PAGES / 10;
       const int MAX_PRINT_PAGES = MAX_PAGES / 5;
        int pages;
        double ink;
        string name;

        public event EventHandler<PrinterEventArgs> PageMissing;
        public event EventHandler<PrinterEventArgs> InkMissing;
        static Random rnd = new Random();
        static  int count = 1;
        public PrinterUC()
        {
            name = "Printer " + count.ToString(); // set the name
            printerNameLabel = new Label();
            printerNameLabel.ContentStringFormat = name;
            count++;

            ink = (rnd.Next(10) / 10) + rnd.Next(0, (int)MAX_INK - 1); // set the ink
            inkCountProgressBar = new ProgressBar();
            inkCountProgressBar.Value = ink;

            pages = PageCount+ rnd.Next(-1, MAX_PAGES);
            
            pageCountSlider = new Slider();
            pageCountSlider.Value = pages;

            InitializeComponent();
        }


        #region property
        public string PrinterName
        {
            get
            {
                return name;
            }
            
        }

        public double InkCount
        {
            get
            {
                return ink;
            }
            set
            {
                ink = value;
                if (InkCount <= 15.0 && InkCount >= 10.0)
                {
                    ink = 13.0;
                   this.inkLabel.Foreground = new SolidColorBrush(Colors.Yellow);
                   
                    PrinterEventArgs LowInk = new PrinterEventArgs(true, InkCount.ToString(), PrinterName);
                    PageMissing(this, LowInk);
                }
                if (InkCount <10.0 && InkCount >1.0)
                {
                    ink = 9.0;
                        this.inkLabel.Foreground = new SolidColorBrush(Colors.Orange);
                     PrinterEventArgs LowInk = new PrinterEventArgs(true, InkCount.ToString(), PrinterName);
                      InkMissing(this, LowInk);
                }
                if (InkCount <=1.0)
                    this.inkLabel.Foreground = new SolidColorBrush(Colors.Red);
                PrinterEventArgs NoInk = new PrinterEventArgs(false, InkCount.ToString(), PrinterName);
                InkMissing(this, NoInk);
                if (InkCount > 15)
                {
                    ink = value;
                    this.pageLabel.Foreground = new SolidColorBrush(Colors.Black);

                }
                //            InkMissing();
            }
            

        }

        public int PageCount
        {
            get
            {
                return pages;
            }
            set
            {
                pages =  value;
                if (PageCount <= 0)
                {
                    pages = 0;
                    this.pageLabel.Foreground = new SolidColorBrush(Colors.Red);
                    PrinterEventArgs noPages = new PrinterEventArgs(true,  MIN_ADD_PAGES.ToString(), PrinterName); 
                    PageMissing(this,noPages);
                }
                if (PageCount > 0)
                {
                    pages = value;
                    this.pageLabel.Foreground = new SolidColorBrush(Colors.Black);
                 
                }
            }

        }
        #endregion

        public void print()
        {
            this.Background = new SolidColorBrush(Colors.White);
            //   ink -= ((rnd.Next(10) / 10) + rnd.Next(0, (int)MAX_INK - 1));
            //   pages -= (rnd.Next(0, MAX_PAGES));
            // inkCountProgressBar.Value = ink;
            //pageCountSlider.Value = pages;
        }
        public void addInk()
        {
            if ((ink + MAX_PRINT_INK) > MAX_INK)
            {
                double tempInk = rnd.Next((int)((MAX_INK - ink))) + rnd.Next(10) / 10; // pick random number between (0 to (diffrence)) and add value between 0 to 1
                ink += tempInk;
            }
            else
                ink += MAX_PRINT_INK;
          }

        public void addPages()
        {
            if ((pages + MAX_PRINT_PAGES) > MAX_PAGES)
            {
                int tempPages = rnd.Next(MAX_PAGES - pages) ; // pick random number between (0 to (diffrence))
                pages += tempPages;
            }
            else
                pages += MAX_PRINT_PAGES;
         }


        private void printerNameLabel_MouseEnter(object sender, MouseEventArgs e)
        {
            printerNameLabel.FontSize = 20;
        }

        private void printerNameLabel_MouseLeave(object sender, MouseEventArgs e)
        {
            printerNameLabel.FontSize = 16;
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Background = new SolidColorBrush(Colors.LightBlue);
        }
    }
}
