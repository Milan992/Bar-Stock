using System;
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
        }

        public EmployeeViewModel(Employee employeeOpen, tblEmployee employeeToView)
        {
            e = employeeOpen;
            employee = employeeToView;
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

        #endregion
    }
}
