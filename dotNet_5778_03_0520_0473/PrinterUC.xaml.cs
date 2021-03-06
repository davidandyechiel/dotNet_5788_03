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
        private static int minPagesToAdd = 0;
        private static double minInkToAdd = 0;

        public event EventHandler<PrinterEventArgs> PageMissing;
        public event EventHandler<PrinterEventArgs> InkMissing;
        static Random rnd = new Random();
        static int count = 1;
        public PrinterUC()
        {
            name = "Printer " + count.ToString(); // set the name
            printerNameLabel = new Label();
            printerNameLabel.ContentStringFormat = name;
            count++;

            ink = (rnd.Next(10) / 10) + rnd.Next(0, (int)MAX_INK - 1); // set the ink
            inkCountProgressBar = new ProgressBar();
            inkCountProgressBar.Value = ink;


            pages = PageCount + rnd.Next(-1, MAX_PAGES);
            pageCountSlider = new Slider();
            pageCountSlider.Value = pages;

            InitializeComponent();
            setInkLabelColor(); // sets the color of the ink label
        }

        private void setInkLabelColor()
        {
            this.inkLabel.Foreground = new SolidColorBrush(Colors.Black);  //defult black
            if (ink <= 15)
            {
                if (ink >= 10.0)
                    this.inkLabel.Foreground = new SolidColorBrush(Colors.Yellow); // if ink < 15 %
                else if (InkCount > 1.0)
                    this.inkLabel.Foreground = new SolidColorBrush(Colors.Orange); // if ink < 10 %
                else this.inkLabel.Foreground = new SolidColorBrush(Colors.Red); // if ink < 1%
            }
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
                setInkLabelColor(); // sets the lable color.
                if (ink <= 15.0)
                {
                    minInkToAdd = -value;
                    PrinterEventArgs InkStatus;
                    if (ink >= 10.0)  // 15 >= ink >= 10
                    {

                        InkStatus = new PrinterEventArgs(false, minInkToAdd.ToString(), PrinterName); // (critical, minimum ink to fill, printer name)
                        ink = value;
                        setInkLabelColor();
                    }
                    else if (InkCount > 1.0)  // 10 >= ink >= 1
                    {
                        InkStatus = new PrinterEventArgs(false, minInkToAdd.ToString(), PrinterName);
                        // 1 > ink
                        ink = value;
                        setInkLabelColor();
                    }
                    else
                    {
                        InkStatus = new PrinterEventArgs(true, (MIN_ADD_INK - InkCount).ToString(), PrinterName);

                        ink = value;
                        setInkLabelColor();
                    }
                    InkMissing?.Invoke(this, InkStatus);// if InkMissing isnt empty ,invoke it
                }
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
                pages = value;
                if (PageCount <= 0)
                {
                    minPagesToAdd = -value;
                    PrinterEventArgs noPages = new PrinterEventArgs(true, minPagesToAdd.ToString(), PrinterName);
                    pages = 0;
                    this.pageLabel.Foreground = new SolidColorBrush(Colors.Red);
                    PageMissing?.Invoke(this, noPages); // if page missing isnt empty invoke it
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
            inkCountProgressBar.Value -= ((rnd.Next(10) / 10) + rnd.Next(0, (int)MAX_INK - 1)); // decrase random amount from the ink
            pageCountSlider.Value -= (rnd.Next(0, MAX_PAGES)); // decrase random amount from the pages if the number is bigger than the number so the missingpage event will acure.
        }

        public void addInk()
        {

            if ((ink + MAX_PRINT_INK) > MAX_INK)
            {
                double tempInk = rnd.Next((int)((MAX_INK - ink))) + rnd.Next(10) / 10; // pick random number between (0 to (diffrence)) and add value between 0 to 1
                ink += tempInk;
                inkCountProgressBar.Value += tempInk;
            }
            else
            {
                ink += MAX_PRINT_INK;
                inkCountProgressBar.Value += MAX_PRINT_INK;
            }
            setInkLabelColor();

        }

        public void addPages()
        {
            pageCountSlider.Value += rnd.Next(minPagesToAdd, MAX_PAGES); // pick random number between (min pages to add and max capacity of pages)

        }


        private void printerNameLabel_MouseEnter(object sender, MouseEventArgs e)
        {
            printerNameLabel.FontSize = 20; // enlarge the font of print name label
        }

        private void printerNameLabel_MouseLeave(object sender, MouseEventArgs e)
        {
            printerNameLabel.FontSize = 16; // minimize the font of print name label to defult
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Background = new SolidColorBrush(Colors.LightBlue);
        }
    }
}
