using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet_5778_03_0520_0473
{
    public class PrinterEventArgs:EventArgs    
    {
        private bool critical;
        private DateTime date;
        private string error;
        private string name;

        public PrinterEventArgs()
        {
            critical = false;
            error = " error" ;
            name = "printer0";
            date = DateTime.Now;
        }
        public PrinterEventArgs(bool critical, string error, string name)
        {
            this.critical = critical;
            this.error = error;
            this.name = name;
            date = DateTime.Now;
        }
        #region property
        public bool Critical
        {
            get
            {
               return critical;
            }

            set
            {
                critical = value;
            }
        }

        public System.DateTime Date
        {
            get
            {
               return date;
            }

            set
            {
                date = value;
            }
        }

        public string Error
        {
            get
            {
                return error;
            }

            set
            {
                error = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }
        #endregion
    }
}
