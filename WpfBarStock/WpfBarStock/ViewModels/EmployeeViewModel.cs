using Aspose.Pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfBarStock.Model;
using WpfBarStock.Views;

namespace WpfBarStock.ViewModels
{
    class EmployeeViewModel : ViewModelBase
    {
        Service service = new Service();
        Employee e;

        #region Constructors

        public EmployeeViewModel(Employee employeeOpen)
        {
            e = employeeOpen;
            articles = service.GetAllArticles();
            checks = new List<Check>();
            shifts = new List<string>() { "1", "2" };
            cash = "0";
            date = DateTime.Now;
        }

        public EmployeeViewModel(Employee employeeOpen, tblEmployee employeeToView)
        {
            e = employeeOpen;
            employee = employeeToView;
            articles = service.GetAllArticles();
            checks = new List<Check>();
            shifts = new List<string>() { "1", "2" };
            cash = "0";
            date = DateTime.Now;
        }

        #endregion

        #region Properties

        private tblEmployee employee;

        public tblEmployee Employee
        {
            get { return employee; }
            set
            {
                employee = value;
                OnPropertyChanged("Employee");
            }
        }

        private List<string> shifts;

        public List<string> Shifts
        {
            get { return shifts; }
            set
            {
                shifts = value;
                OnPropertyChanged("Shifts");
            }
        }

        private List<vwArticle> articles;

        public List<vwArticle> Articles
        {
            get { return articles; }
            set
            {
                articles = value;
                OnPropertyChanged("Articles");
            }
        }

        private int cashbox;

        public int Cashbox
        {
            get { return cashbox; }
            set
            {
                cashbox = value;
                OnPropertyChanged("Cashbox");
            }
        }

        private int kitchen;

        public int Kitchen
        {
            get { return kitchen; }
            set
            {
                kitchen = value;
                OnPropertyChanged("Kitchen");
            }
        }

        private int card;

        public int Card
        {
            get { return card; }
            set
            {
                card = value;
                OnPropertyChanged("Card");
            }
        }

        private int paycheck;

        public int Paycheck
        {
            get { return paycheck; }
            set
            {
                paycheck = value;
                OnPropertyChanged("Paycheck");
            }
        }

        private int owner;

        public int Owner
        {
            get { return owner; }
            set
            {
                owner = value;
                OnPropertyChanged("Owner");
            }
        }

        private int newspaper;

        public int Newspaper
        {
            get { return newspaper; }
            set
            {
                newspaper = value;
                OnPropertyChanged("Newspaper");
            }
        }

        private int plus;

        public int Plus
        {
            get { return plus; }
            set
            {
                plus = value;
                OnPropertyChanged("Plus");
            }
        }

        private int minus;

        public int Minus
        {
            get { return minus; }
            set
            {
                minus = value;
                OnPropertyChanged("Minus");
            }
        }

        private List<Check> checks;

        public List<Check> Checks
        {
            get { return checks; }
            set
            {
                checks = value;
                OnPropertyChanged("Checks");
            }
        }

        private string shift;

        public string Shift
        {
            get { return shift; }
            set
            {
                shift = value;
                OnPropertyChanged("Shift");
            }
        }

        private string waiters;

        public string Waiters
        {
            get { return waiters; }
            set
            {
                waiters = value;
                OnPropertyChanged("Waiters");
            }
        }

        private DateTime date;

        public DateTime Date
        {
            get { return date; }
            set
            {
                date = value;
                OnPropertyChanged("Date");
            }
        }

        private string paycheckDescription;

        public string PaycheckDescription
        {
            get { return paycheckDescription; }
            set
            {
                paycheckDescription = value;
                OnPropertyChanged("PaycheckDescription");
            }
        }

        private string plusDescription;

        public string PlusDescription
        {
            get { return plusDescription; }
            set
            {
                plusDescription = value;
                OnPropertyChanged("PlusDescription");
            }
        }

        private string minusDescription;

        public string MinusDescription
        {
            get { return minusDescription; }
            set
            {
                minusDescription = value;
                OnPropertyChanged("MinusDescription");
            }
        }

        private int bar;

        public int Bar
        {
            get { return bar; }
            set
            {
                bar = value;
                OnPropertyChanged("Bar");
            }
        }

        private string cash;

        public string Cash
        {
            get { return cash; }
            set
            {
                cash = value;
                OnPropertyChanged("Cash");
            }
        }

        #endregion

        #region Commands

        //print window
        private ICommand print;

        public ICommand Print
        {
            get
            {
                if (print == null)
                {
                    print = new RelayCommand(param => PrintExecute(), param => CanPrintExecute());
                }

                return print;
            }
        }

        private void PrintExecute()
        {
            try
            {
                //CODE AFTER CREATING HTML FILE:

                //convert html to pdf
                //HtmlLoadOptions htmloptions = new HtmlLoadOptions(@"D:\filedirectory"); // enter realative file directory adres
                //Document doc = new Document(@"D:\CV\filename.html", htmloptions); // enter realative file adres
                //doc.Save("output1.pdf", Aspose.Pdf.SaveFormat.Pdf);

                //print pdf file
                //var pd = new PrintDialog();
                //pd.ShowDialog();
                //var info = new ProcessStartInfo()
                //{
                //    Verb = "print",
                //    CreateNoWindow = true,
                //    FileName = @"", //add file adress
                //    WindowStyle = ProcessWindowStyle.Hidden
                //};
                //Process.Start(info);

                PrintDialog printDialog = new PrintDialog();
                if (printDialog.ShowDialog() == true)
                {
                    printDialog.PrintVisual(e.grid, "Print");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanPrintExecute()
        {
            return true;
        }

        private ICommand calculate;

        public ICommand Calculate
        {
            get
            {
                if (calculate == null)
                {
                    calculate = new RelayCommand(param => CalculateExecute(), param => CanCalculateExecute());
                }

                return calculate;
            }
        }

        private void CalculateExecute()
        {
            try
            {
                service.CalculateSoldArticles(Articles);
                service.CalculatePriceSold(Articles);
                Bar = service.CalculateBar(Articles);
                e.ArticlesDataGrid.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanCalculateExecute()
        {
            return true;
        }

        #endregion
    }
}
