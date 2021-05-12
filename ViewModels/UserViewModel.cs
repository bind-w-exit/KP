using System.Windows.Controls;
using System.Windows.Input;

namespace KP.ViewModels
{
    class UserViewModel : BaseViewModel
    {
        private Page Houses;
        private Page Payments;
        private Page Complains;
        private Page UserAccount;

        private Page _currentPage;
        public Page CurrentPage
        {
            get { return _currentPage; }
            set
            {
                if (_currentPage == value)
                    return;

                _currentPage = value;
                OnPropertyChanged("CurrentPage");
            }
        }

        public ICommand BtnClickHouses { get; private set; }

        public ICommand BtnClickPayments { get; private set; }

        public ICommand BtnClickComplains { get; private set; }

        public ICommand BtnClickUserAccount { get; private set; }

        public ICommand BtnClickLogout { get; private set; }

        public UserViewModel()
        {
            BtnClickHouses = new DelegateCommand(BtnClickHousesCommand);
            BtnClickPayments = new DelegateCommand(BtnClickPaymentsCommand);
            BtnClickComplains = new DelegateCommand(BtnClickComplainsCommand);
            BtnClickUserAccount = new DelegateCommand(BtnClickUserAccountCommand);
            BtnClickLogout = new DelegateCommand(BtnClickLogoutCommand);

            Houses = new Views.Houses();
            Payments = new Views.Payments();
            Complains = new Views.Complaints();
            UserAccount = new Views.UserAccount();


            CurrentPage = new Views.Houses();
        }

        private void BtnClickLogoutCommand(object obj)
        {
            MainWindow.MainFrame.Navigate(new Views.Login());
        }

        private void BtnClickUserAccountCommand(object obj)
        {
            CurrentPage = UserAccount;
        }

        private void BtnClickComplainsCommand(object obj)
        {
            CurrentPage = Complains;
        }

        private void BtnClickPaymentsCommand(object obj)
        {
            CurrentPage = Payments;
        }

        private void BtnClickHousesCommand(object obj)
        {
            CurrentPage = Houses;
        }
    }
}
