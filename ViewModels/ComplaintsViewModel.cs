using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows.Input;

namespace KP.ViewModels
{
    class ComplaintsViewModel : BaseViewModel
    {
        MyDbContext context = new MyDbContext();

        private ObservableCollection<Complaint> _complaintsList;
        public ObservableCollection<Complaint> ComplaintsList
        {
            get
            {
                return _complaintsList;
            }

            set
            {
                _complaintsList = value;
                OnPropertyChanged(nameof(ComplaintsList));
            }

        }

        private Complaint _selectedComplaint;
        public Complaint SelectedComplaint
        {
            get
            {
                return _selectedComplaint;
            }

            set
            {
                _selectedComplaint = value;
                OnPropertyChanged(nameof(SelectedComplaint));
            }
        }

        private ObservableCollection<District> _districts;
        public ObservableCollection<District> Districts
        {
            get
            {
                return _districts;
            }

            set
            {
                _districts = value;
                OnPropertyChanged(nameof(Districts));
            }

        }

        private District _selectedDistrict;
        public District SelectedDistrict
        {
            get
            {
                return _selectedDistrict;
            }

            set
            {
                _selectedDistrict = value;
                ClearErrors(nameof(SelectedDistrict));
                OnPropertyChanged(nameof(SelectedDistrict));
            }
        }

        private string _briefDescription;
        public string BriefDescription
        {
            get
            {
                return _briefDescription;
            }

            set
            {
                _briefDescription = value;
                ClearErrors(nameof(BriefDescription));
                OnPropertyChanged(nameof(BriefDescription));
            }
        }   

        public ComplaintsViewModel()
        {
            //Dialog window
            OpenEditDialogCommand = new DelegateCommand(OpenEditDialog);
            OpenAddDialogCommand = new DelegateCommand(OpenAddDialog);
            AcceptDialogCommand = new DelegateCommand(AcceptDialog);
            CancelDialogCommand = new DelegateCommand(CancelDialog);

            BtnClickDeleteComplain = new DelegateCommand(BtnClickDeleteComplainCommand);           

            context.Complaints.Where(x => x.User.Id == UserId).Load();
            context.Districts.Load();
            ComplaintsList = context.Complaints.Local;
            Districts = context.Districts.Local;
          
        }

        public ICommand BtnClickDeleteComplain { get; }

        private void BtnClickDeleteComplainCommand(object obj)
        {           
            ComplaintsList.Remove(SelectedComplaint);
            context.SaveChanges();
        }

        #region DialogWindow

        public ICommand OpenAddDialogCommand { get; }
        public ICommand OpenEditDialogCommand { get; }
        public ICommand AcceptDialogCommand { get; }
        public ICommand CancelDialogCommand { get; }
        
        private bool _isDialogOpen;
        public bool IsDialogOpen
        {
            get
            {
                return _isDialogOpen;
            }
            set
            {
                _isDialogOpen = value;
                OnPropertyChanged(nameof(IsDialogOpen));
            }
        }

        private bool _isAddDialog;
        public bool IsAddDialog
        {
            get
            {
                return _isAddDialog;
            }
            set
            {
                _isAddDialog = value;
                OnPropertyChanged(nameof(IsAddDialog));
            }
        }

        private object _dialogContent;
        public object DialogContent
        {
            get
            {
                return _dialogContent;
            }
            set
            {
                _dialogContent = value;
                OnPropertyChanged(nameof(DialogContent));
            }
        }

        private string _dialogHeader;
        public string DialogHeader
        {
            get
            {
                return _dialogHeader;
            }
            set
            {
                _dialogHeader = value;
                OnPropertyChanged(nameof(DialogHeader));
            }
        }

        private void OpenAddDialog(object obj)
        {
            DialogContent = new ComplaintsDialog();
            DialogHeader = "Add complaint";
            BriefDescription = "";
            SelectedDistrict = null;
            IsAddDialog = true;
            IsDialogOpen = true;          
        }

        private void OpenEditDialog(object obj)
        {
            if (SelectedComplaint != null)
            {
                DialogContent = new ComplaintsDialog();
                DialogHeader = "Editing complaint";
                BriefDescription = SelectedComplaint.Description;
                SelectedDistrict = SelectedComplaint.District;
                IsAddDialog = false;
                IsDialogOpen = true;               
            }
        }

        private void CancelDialog(object obj) => IsDialogOpen = false;

        private void AcceptDialog(object obj)
        {
            if (IsAddDialog)
            {
                if (string.IsNullOrEmpty(BriefDescription))
                {
                    AddError(nameof(BriefDescription), "Field cannot be empty");
                }
                if (SelectedDistrict == null)
                {
                    AddError(nameof(SelectedDistrict), "Field cannot be empty");
                }
                if (!HasErrors)
                {
                    var complain = new Complaint
                    {
                        Date = DateTime.Now,
                        Description = BriefDescription,
                        Status = "Not"
                    };

                    var user = context.Users.FirstOrDefault(u => u.Id == UserId);
                    var district = context.Districts.FirstOrDefault(u => u.Id == SelectedDistrict.Id);

                    complain.User = user;
                    complain.District = district;

                    ComplaintsList.Add(complain);
                    context.SaveChanges();

                    IsDialogOpen = false;
                }
            }

            else
            {
                var complain = new Complaint
                {
                    Date = SelectedComplaint.Date,
                    Description = BriefDescription,
                    Status = "Not"
                };

                var user = context.Users.FirstOrDefault(u => u.Id == UserId);
                var district = context.Districts.FirstOrDefault(u => u.Id == SelectedDistrict.Id);

                complain.User = user;
                complain.District = district;

                ComplaintsList.Remove(SelectedComplaint);
                ComplaintsList.Add(complain);
                context.SaveChanges();

                IsDialogOpen = false;
            }   
            
        }

        #endregion

    }
}
