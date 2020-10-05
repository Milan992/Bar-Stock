using System.Windows;
using WpfBarStock.Model;
using WpfBarStock.ViewModels;

namespace WpfBarStock.Views
{
    /// <summary>
    /// Interaction logic for Employee.xaml
    /// </summary>
    public partial class Employee : Window
    {
        public Employee()
        {
            InitializeComponent();
            this.DataContext = new EmployeeViewModel(this);
        }

        public Employee(tblEmployee employee)
        {
            InitializeComponent();
            this.DataContext = new EmployeeViewModel(this, employee);
        }
    }
}
