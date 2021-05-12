using KP.ViewModels;
using System.Windows.Controls;

namespace KP.Views
{
    /// <summary>
    /// Interaction logic for SignIn.xaml
    /// </summary>
    public partial class SignIn : Page
    {
        public SignIn()
        {
            InitializeComponent();
            DataContext = new SignInViewModel();
        }
    }
}
