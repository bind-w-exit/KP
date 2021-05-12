using KP.ViewModels;
using System.Windows.Controls;

namespace KP.Views
{
    /// <summary>
    /// Interaction logic for Complains.xaml
    /// </summary>
    public partial class Complaints : Page
    {
        public Complaints()
        {
            InitializeComponent();
            DataContext = new ComplaintsViewModel();
        }
    }
}
