using System.Windows;
using WpfBarStock.ViewModels;

namespace WpfBarStock.Views
{
    /// <summary>
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        public Admin()
        {
            InitializeComponent();
            this.DataContext = new AdminViewModel(this);
        }
    }
}
