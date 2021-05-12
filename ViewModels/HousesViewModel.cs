using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;

namespace KP.ViewModels
{
    class HousesViewModel : BaseViewModel
    {
        private ObservableCollection<House> _housesList;
        public ObservableCollection<House> HousesList
        {
            get
            {
                return _housesList;
            }

            set
            {
                _housesList = value;
                OnPropertyChanged(nameof(HousesList));
            }

        }

        public HousesViewModel()
        {
            using (var context = new MyDbContext())
            {
                context.Houses.Where(x => x.User.Id == UserId).Load();
                HousesList = context.Houses.Local;
            }
        }
    }
}
