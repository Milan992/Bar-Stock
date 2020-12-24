using Aspose.Pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
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
            date = DateTime.Now;
        }

        public EmployeeViewModel(Employee employeeOpen, tblEmployee employeeToView)
        {
            e = employeeOpen;
            employee = employeeToView;
            articles = service.GetAllArticles();
            checks = new List<Check>();
            shifts = new List<string>() { "1", "2" };
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

        private int cash;

        public int Cash
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
                HtmlLoadOptions htmloptions = new HtmlLoadOptions(@"..\..\Stock.html"); // enter realative file directory adres
                Document doc = new Document(@"..\..\Stock.html", htmloptions); // enter realative file adres
                doc.Save("output1.pdf", Aspose.Pdf.SaveFormat.Pdf);

                //print pdf file
                var pd = new PrintDialog();
                pd.ShowDialog();
                var info = new ProcessStartInfo()
                {
                    Verb = "print",
                    CreateNoWindow = true,
                    FileName = @"", //add file adress
                    WindowStyle = ProcessWindowStyle.Hidden
                };
                Process.Start(info);

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
                MakeStockHtmlFIle();
                service.CalculateSoldArticles(Articles);
                service.CalculatePriceSold(Articles);
                Bar = service.CalculateBar(Articles);
                Cash = service.CalculateCash(cashbox, kitchen, card, paycheck, owner, newspaper, plus, minus, bar, checks);
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

        private ICommand updateAmount;

        public ICommand UpdateAmount
        {
            get
            {
                if (updateAmount == null)
                {
                    updateAmount = new RelayCommand(param => UpdateAmountExecute(), param => CanUpdateAmountExecute());
                }

                return updateAmount;
            }
        }

        private void UpdateAmountExecute()
        {
            try
            {
                if (service.YesNoWarning("Da li ste sigurni da zelite da prepisete? Pocetno stanje ce se promeniti na trenutno, " +
                    "a sva ostala polja ce ostati prazna. Prethodno stanje se ne moze vratiti posle prepisivanja."))
                {
                    service.UpdateAmount(Articles);
                    Articles = service.GetAllArticles();
                    e.ArticlesDataGrid.Items.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanUpdateAmountExecute()
        {
            return true;
        }

        #endregion

        #region Methods

        public void MakeStockHtmlFIle()
        {
            PropertyInfo[] myPropertyInfo;
            // Get the properties of 'Type' class object.
            myPropertyInfo = Type.GetType("WpfBarStock.Model.vwArticle").GetProperties();

            File.WriteAllText(@"..\..\Stock.html", "");
            using (StreamWriter sw = new StreamWriter(@"..\..\Stock.html", true))
            {
                // Begining of the html file is always the same, so we copy it from the file.
                var htmlfileBegining = File.ReadAllText(@"..\..\htmlFileBegining.html");
                sw.Write(htmlfileBegining);

                sw.WriteLine("<table>");

                // Write column names
                sw.WriteLine("<tr>");
                sw.WriteLine("<th>rb</th>");
                sw.WriteLine("<th>naziv</th>");
                sw.WriteLine("<th>poc st</th>");
                sw.WriteLine("<th>tren st</th>");
                sw.WriteLine("<th>stiglo</th>");
                sw.WriteLine("<th>prodato</th>");
                sw.WriteLine("<th>cena</th>");
                sw.WriteLine("</tr>");

                // Write values
                foreach (vwArticle article in Articles)
                {
                    sw.WriteLine("<tr><td>{0}</td>", article.ArticleID);
                    sw.WriteLine("<td>{0}</td>", article.ArticleName);
                    sw.WriteLine("<td>{0}</td>", article.Amount);
                    sw.WriteLine("<td>{0}</td>", article.NewAmount);
                    sw.WriteLine("<td>{0}</td>", article.ProcuredAmount);
                    sw.WriteLine("<td>{0}</td>", article.AmountSold);
                    sw.WriteLine("<td>{0}</td></tr>", article.PriceSold);
                }

                sw.WriteLine("</table>");

                // End of the html file.
                sw.WriteLine("</body>\n</html>");
            }
        }

        #endregion
    }
}
