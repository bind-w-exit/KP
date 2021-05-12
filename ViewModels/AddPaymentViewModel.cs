using System;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;

namespace KP.ViewModels
{
    class AddPaymentViewModel : BaseViewModel
    {
        private string _cost;
        public string Cost
        {
            get
            {
                return _cost;
            }

            set
            {
                _cost = value;
                OnPropertyChanged(nameof(Cost));

                ClearErrors(nameof(Cost));

                if (value.Length > 0)
                    if (!DataValidation.Numeric(value))
                        AddError(nameof(Cost), "Invalid number format");
            }
        }

        private string _oldHeatMeter;
        public string OldHeatMeter
        {
            get
            {
                return _oldHeatMeter;
            }

            set
            {
                _oldHeatMeter = value;
                OnPropertyChanged(nameof(OldHeatMeter));
            }
        }

        private string _heatMeter;
        public string HeatMeter
        {
            get
            {
                return _heatMeter;
            }

            set
            {
                _heatMeter = value;
                OnPropertyChanged(nameof(HeatMeter));

                ClearErrors(nameof(HeatMeter));

                if (value.Length > 0)
                    if (!DataValidation.Numeric(value))
                        AddError(nameof(HeatMeter), "Invalid number format");
                    else
                        if (Convert.ToInt32(HeatMeter) < Convert.ToInt32(OldHeatMeter))
                            AddError(nameof(HeatMeter), "The indicator can be less than the old rate");
            }
        }

        private ObservableCollection<House> _houses;
        public ObservableCollection<House> Houses
        {
            get
            {
                return _houses;
            }

            set
            {
                _houses = value;
                OnPropertyChanged(nameof(Houses));
            }
                
        }

        private House _selectedHouse;
        public House SelectedHouse
        {
            get
            {
                return _selectedHouse;
            }

            set
            {
                _selectedHouse = value;
                OldHeatMeter = Convert.ToString(_selectedHouse?.HeatMeter);
                OnPropertyChanged(nameof(SelectedHouse));
            }
        }


        public ICommand BtnClickAddPayment { get; private set; }

        public ICommand BtnClickBack { get; private set; }      

        public AddPaymentViewModel()
        {
            BtnClickAddPayment = new DelegateCommand(BtnClickAddPaymentCommand);
            BtnClickBack = new DelegateCommand(BtnClickBackCommand);

            using (var context = new MyDbContext())
            {
                context.Houses.Load();
                Houses = context.Houses.Local;
            }

            SelectedHouse = Houses?.FirstOrDefault();

        }

        private void BtnClickAddPaymentCommand(object obj)
        {
            if (DataValidation.Empty(HeatMeter))
                AddError(nameof(HeatMeter), "Field cannot be empty");
            if (DataValidation.Empty(Cost))
                AddError(nameof(Cost), "Field cannot be empty");

            if (!DataValidation.Empty(HeatMeter))
                if (!DataValidation.Empty(HeatMeter))
                    if (DataValidation.Numeric(HeatMeter))
                        if (DataValidation.Numeric(HeatMeter))
                            if (Convert.ToInt32(HeatMeter) >= Convert.ToInt32(OldHeatMeter))
                            {
                                using (var context = new MyDbContext())
                                {
                                    var payment = new Payment
                                    {
                                        Date = DateTime.Now,
                                        Cost = Convert.ToInt32(Cost)
                                    };

                                    var user = context.Users.FirstOrDefault(u => u.Id == UserId);
                                    var house = context.Houses.FirstOrDefault(u => u.Id == SelectedHouse.Id);

                                    house.HeatMeter = Convert.ToInt32(HeatMeter);

                                    payment.User = user;
                                    payment.House = house;
                                    context.Payments.Add(payment);
                                    context.SaveChanges();
                                }

                                HeatMeter = "";
                                Cost = "";
                            }

        }

        private void BtnClickBackCommand(object obj)
        {
            User.MainFrame.NavigationService.GoBack();
        }
    }
}

