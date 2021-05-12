using KP.ViewModels;
using System.Windows.Controls;

namespace KP.Views
{
    /// <summary>
    /// Interaction logic for UserAccount.xaml
    /// </summary>
    public partial class UserAccount : Page
    {
        public UserAccount()
        {
            InitializeComponent();
            DataContext = new UserAccountViewModel();
        }
    }
}
