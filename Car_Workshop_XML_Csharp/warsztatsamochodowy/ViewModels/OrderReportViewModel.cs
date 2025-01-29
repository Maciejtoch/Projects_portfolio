using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using warsztatsamochodowy.Data;
using warsztatsamochodowy.Models;

namespace warsztatsamochodowy.ViewModels
{
    public class OrderReportViewModel : INotifyPropertyChanged
    {
        private readonly AppDbContext _context;

        public OrderReportViewModel()
        {
            _context = new AppDbContext();
            Cars = new ObservableCollection<Car>(_context.Cars.ToList());
            Orders = new ObservableCollection<Order>(_context.Orders.ToList());
        }

        private ObservableCollection<Car> _cars;
        public ObservableCollection<Car> Cars
        {
            get => _cars;
            set
            {
                _cars = value;
                OnPropertyChanged(nameof(Cars));
            }
        }

        private ObservableCollection<Order> _orders;
        public ObservableCollection<Order> Orders
        {
            get => _orders;
            set
            {
                _orders = value;
                OnPropertyChanged(nameof(Orders));
            }
        }

        private int _selectedCarId;
        public int SelectedCarId
        {
            get => _selectedCarId;
            set
            {
                _selectedCarId = value;
                FilterOrders();
                OnPropertyChanged(nameof(SelectedCarId));
            }
        }

        private ObservableCollection<Order> _filteredOrders;
        public ObservableCollection<Order> FilteredOrders
        {
            get => _filteredOrders;
            set
            {
                _filteredOrders = value;
                OnPropertyChanged(nameof(FilteredOrders));
            }
        }

        private void FilterOrders()
        {
            FilteredOrders = new ObservableCollection<Order>(Orders.Where(o => o.CarId == SelectedCarId));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
