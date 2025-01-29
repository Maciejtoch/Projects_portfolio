using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using warsztatsamochodowy.Commands;
using warsztatsamochodowy.Data;
using warsztatsamochodowy.Models;

namespace warsztatsamochodowy.ViewModels
{
    public class OrderViewModel : INotifyPropertyChanged
    {
        public ICommand AddOrderCommand { get; set; }
        private readonly AppDbContext _context;

        public OrderViewModel()
        {
            _context = new AppDbContext();
            Orders = new ObservableCollection<Order>(_context.Orders.ToList());
            Cars = new ObservableCollection<Car>(_context.Cars.ToList());
            Clients = new ObservableCollection<Client>(_context.Clients.ToList());
            AddOrderCommand = new RelayCommand(AddOrder);
        }

        private ObservableCollection<Order> _orders;
        public ObservableCollection<Order> Orders
        {
            get => _orders;
            set
            {
                _orders = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Car> _cars;
        public ObservableCollection<Car> Cars
        {
            get => _cars;
            set
            {
                _cars = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Client> _clients;
        public ObservableCollection<Client> Clients
        {
            get => _clients;
            set
            {
                _clients = value;
                OnPropertyChanged();
            }
        }

        private DateTime _creationDate;
        public DateTime CreationDate
        {
            get => _creationDate;
            set
            {
                _creationDate = value;
                OnPropertyChanged();
            }
        }

        private string _status;
        public string Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged();
            }
        }

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        private int _selectedCarId;
        public int SelectedCarId
        {
            get => _selectedCarId;
            set
            {
                _selectedCarId = value;
                OnPropertyChanged();
            }
        }

        private int _selectedClientId;
        public int SelectedClientId
        {
            get => _selectedClientId;
            set
            {
                _selectedClientId = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool IsValid()
        {
            if (string.IsNullOrWhiteSpace(Description) || string.IsNullOrWhiteSpace(Status) ||
                SelectedCarId == 0 || SelectedClientId == 0)
            {
                MessageBox.Show("All fields must be filled.");
                return false;
            }
            return true;
        }

        public void AddOrder()
        {
            if (!IsValid())
                return;

            var newOrder = new Order
            {
                CreationDate = this.CreationDate,
                Status = this.Status,
                Description = this.Description,
                CarId = this.SelectedCarId,
                ClientId = this.SelectedClientId
            };

            _context.Orders.Add(newOrder);
            _context.SaveChanges();
            Orders.Add(newOrder);

            MessageBox.Show("Order added successfully ");
            ClearFields();
        }

        public void RemoveOrder(Order order)
        {
            _context.Orders.Remove(order);
            _context.SaveChanges();
            Orders.Remove(order);
        }

        public void ClearFields()
        {
            SelectedCarId = 0;
            SelectedClientId = 0;
            Status = "choose status";
            Description = string.Empty;
            CreationDate = DateTime.Now;
        }
    }
}
