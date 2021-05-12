using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace KP.ViewModels
{
    class LoginViewModel : BaseViewModel
    {
        private Page SignIn;
        private Page SignUp;
     
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

        public ICommand BtnClickSignIn
        {
            get;
            private set;
        }

        public ICommand BtnClickSignUp
        {
            get;
            private set;
        }

        public LoginViewModel()
        {

            SignIn = new Views.SignIn();
            SignUp = new Views.SignUp();

            BtnClickSignIn = new DelegateCommand(BtnClickSignInCommand);
            BtnClickSignUp = new DelegateCommand(BtnClickSignUpCommand);

            CurrentPage = SignIn;
        }

        private void BtnClickSignUpCommand(object obj)
        {
            CurrentPage = SignUp;
        }

        private void BtnClickSignInCommand(object obj)
        {
            CurrentPage = SignIn;
        }
    }
}
