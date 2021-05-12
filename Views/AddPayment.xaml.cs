using KP.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;

namespace KP.Views
{
    /// <summary>
    /// Interaction logic for AddPayment.xaml
    /// </summary>
    public partial class AddPayment : Page
    {
        public AddPayment()
        {
            InitializeComponent();
            DataContext = new AddPaymentViewModel();
        }
    }
}
