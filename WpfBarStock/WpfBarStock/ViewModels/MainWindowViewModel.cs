using System;
using System.Windows;
using System.Windows.Input;
using WpfBarStock.Model;
using WpfBarStock.Views;

namespace WpfBarStock.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        Service service = new Service();
        MainWindow mw;

        #region Constructors

        public MainWindowViewModel(MainWindow mainOpen)
        {
            mw = mainOpen;
        }

        #endregion

        #region Properties

        private string userName;

        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                userName = value;
                OnPropertyChanged("UserName");
            }
        }

        private string password;

        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }

        #endregion

        #region Commands

        private ICommand logIn;

        public ICommand LogIn
        {
            get
            {
                if (logIn == null)
                {
                    logIn = new RelayCommand(param => LogInExecute(), param => CanLogInExecute());
                }

                return logIn;
            }
        }

        private void LogInExecute()
        {
            try
            {
                tblEmployee employee;
                if (service.IsEmployee(UserName, Password, out employee))
                {
                    Employee e = new Employee(employee);
                    mw.Close();
                    e.ShowDialog();
                }
                else if (service.IsAdmin(UserName, Password))
                {
                    Admin a = new Admin();
                    mw.Close();
                    a.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Pogresno korisnicko ime ili lozinka.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanLogInExecute()
        {
            if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(Password))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion
    }
}

