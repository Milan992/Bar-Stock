using System;
using WpfBarStock.Views;

namespace WpfBarStock.ViewModels
{
    class AdminViewModel : ViewModelBase
    {
        Service service = new Service();
        Admin a;

        #region Constructors

        public AdminViewModel(Admin adminOpen)
        {
            a = adminOpen;
        }

        #endregion
    }
}
