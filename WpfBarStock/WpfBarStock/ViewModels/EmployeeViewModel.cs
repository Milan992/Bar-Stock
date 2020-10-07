using System;
using System.Collections.Generic;
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
        }

        public EmployeeViewModel(Employee employeeOpen, tblEmployee employeeToView)
        {
            e = employeeOpen;
            employee = employeeToView;
            articles = service.GetAllArticles();
            checks = new List<Check>();

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
                OnPropertyChanged("Emoloyee");
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

        #endregion
    }
}
