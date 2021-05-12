using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;


namespace KP.ViewModels
{
    class SignUpViewModel : BaseViewModel
    {

        public ICommand BtnClickRegister { get; private set; }

        public SignUpViewModel()
        {
            BtnClickRegister = new DelegateCommand(BtnClickRegisterCommand);
        }

        private string _firstName = ""; 
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (_firstName == value)
                    return;

                _firstName = value;
                OnPropertyChanged(nameof(FirstName));

                ClearErrors(nameof(FirstName));

                if (!DataValidation.Name(value))
                {
                    AddError(nameof(FirstName), "Invalid name format");
                }
            }
        }

        private string _lastName = "";
        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (_lastName == value)
                    return;

                _lastName = value;
                OnPropertyChanged(nameof(LastName));

                ClearErrors(nameof(LastName));

                if (!DataValidation.Name(value))
                {
                    AddError(nameof(LastName), "Invalid name format");
                }
            }
        }

        private string _middleName = "";
        public string MiddleName
        {
            get { return _middleName; }
            set
            {
                if (_middleName == value)
                    return;

                _middleName = value;
                OnPropertyChanged(nameof(MiddleName));

                ClearErrors(nameof(MiddleName));

                if (!DataValidation.Name(value))
                {
                    AddError(nameof(MiddleName), "Invalid name format");
                }
            }
        }

        private DateTime _dateOfBirth = DateTime.Today;
        public DateTime DateOfBirth
        {
            get { return _dateOfBirth; }
            set
            {
                if (_dateOfBirth == value)
                    return;

                _dateOfBirth = value;
                OnPropertyChanged(nameof(DateOfBirth));

                ClearErrors(nameof(DateOfBirth));

                if (value.Year > DateTime.Now.Year || value.Year <= 1920)
                {
                    AddError(nameof(DateOfBirth), "Invalid date format");
                }
            }
        }

        private string _phoneNum = "";
        public string PhoneNum
        {
            get { return _phoneNum; }
            set
            {
                if (_phoneNum == value)
                    return;

                _phoneNum = value;
                OnPropertyChanged(nameof(PhoneNum));

                ClearErrors(nameof(PhoneNum));

                if (value.Length > 0)
                {
                    if (value.Length != 13)
                    {
                        AddError(nameof(PhoneNum), "Invalid number format");
                    }
                    if (!value.All(x => x >= '0' && x <= '9' || x == '+'))
                    {
                        AddError(nameof(PhoneNum), "Invalid number format");
                    }
                    if (!(value.FirstOrDefault() == '+'))
                    {
                        AddError(nameof(PhoneNum), "Invalid number format");
                    }
                }
            }
        }

        private string _email = "";
        public string Email
        {
            get { return _email; }
            set
            {
                if (_email == value)
                    return;

                _email = value;
                OnPropertyChanged(nameof(Email));

                ClearErrors(nameof(Email));

                if (value.Length > 0)
                {
                    if (!value.Any(x => x == '@'))
                    {
                        AddError(nameof(Email), "Invalid email format");
                    }
                    if (!value.Any(x => x == '.'))
                    {
                        AddError(nameof(Email), "Invalid email format");
                    }
                }

            }
        }

        private string _login = "";
        public string Login
        {
            get { return _login; }
            set
            {
                if (_login == value)
                    return;

                _login = value;
                OnPropertyChanged(nameof(Login));

                ClearErrors(nameof(Login));
            }
        }

        public string _password = "";
        public string Password
        {
            get { return _password; }
            set
            {
                if (_password == value)
                    return;

                _password = value;
                OnPropertyChanged(nameof(Password));

                ClearErrors(nameof(Password));
            }
        }

        private async void BtnClickRegisterCommand(object obj)
        {
            var passwordBox = obj as PasswordBox;
            if (passwordBox == null)
                return;
            Password = passwordBox.Password;         
            
            await Task.Run(() =>
            {
                using (var context = new MyDbContext())
                {

                    if (FirstName == "")
                    {
                        AddError(nameof(FirstName), "Field cannot be empty");
                    }
                    if (LastName == "")
                    {
                        AddError(nameof(LastName), "Field cannot be empty");
                    }
                    if (MiddleName == "")
                    {
                        AddError(nameof(MiddleName), "Field cannot be empty");
                    }
                    if (DateOfBirth.Year >= DateTime.Now.Year || DateOfBirth.Year <= 1920)
                    {
                        AddError(nameof(DateOfBirth), "Field cannot be empty");
                    }
                    if (PhoneNum == "")
                    {
                        AddError(nameof(PhoneNum), "Field cannot be empty");
                    }
                    else
                    {
                        var user = context.Users.FirstOrDefault(u => u.PhoneNum == PhoneNum);
                        if (user != null)
                        {
                            AddError(nameof(PhoneNum), "Phone number is already exists");
                        }
                    }
                    if (Email == "")
                    {
                        AddError(nameof(Email), "Field cannot be empty");
                    }
                    else
                    {
                        var user = context.Users.FirstOrDefault(u => u.Email == Email);
                        if (user != null)
                        {
                            AddError(nameof(Email), "Email already exists");
                        }
                    }
                    if (Login == "")
                    {
                        AddError(nameof(Login), "Field cannot be empty");
                    }
                    else
                    {
                        var user = context.Users.FirstOrDefault(u => u.Login == Login);
                        if (user != null)
                        {
                            AddError(nameof(Login), "Login already exists");
                        }
                    }
                    if (Password == "")
                    {
                        AddError(nameof(Password), "Field cannot be empty");
                    }

                    if (!HasErrors)
                    {
                        var user = new User
                        {
                            FirstName = FirstName,
                            LastName = LastName,
                            MiddleName = MiddleName,
                            DateOfBirth = DateOfBirth,
                            PhoneNum = PhoneNum,
                            Email = Email,
                            Login = Login,
                            Password = HashPassword(Password)
                        };

                        UserId = user.Id;
                        FirstName = "";
                        LastName = "";
                        MiddleName = "";
                        DateOfBirth = DateTime.Now;
                        PhoneNum = "";
                        Email = "";
                        Login = "";
                        Password = "";

                        context.Users.Add(user);
                        context.SaveChanges();
                    }
                }
            });
        }

        public string HashPassword(string s)
        {
            //переводим строку в байт-массим  
            byte[] bytes = Encoding.Unicode.GetBytes(s);

            //создаем объект для получения средст шифрования  
            MD5CryptoServiceProvider CSP =
                new MD5CryptoServiceProvider();

            //вычисляем хеш-представление в байтах  
            byte[] byteHash = CSP.ComputeHash(bytes);

            string hash = string.Empty;

            //формируем одну цельную строку из массива  
            foreach (byte b in byteHash)
                hash += string.Format("{0:x2}", b);

            return hash;
        }

    }
}
