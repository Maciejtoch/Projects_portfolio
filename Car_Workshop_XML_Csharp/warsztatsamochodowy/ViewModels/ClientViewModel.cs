using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using warsztatsamochodowy.Commands;
using warsztatsamochodowy.Data;
using warsztatsamochodowy.Models;

namespace warsztatsamochodowy.ViewModels
{
    public class ClientViewModel : INotifyPropertyChanged
    {
        public ICommand AddClientCommand { get; set; }
        private readonly AppDbContext _context;
        

        public ClientViewModel()
        {
            _context = new AppDbContext();
            Clients = new ObservableCollection<Client>(_context.Clients.ToList());
            AddClientCommand = new RelayCommand(AddClient);
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

        private string _firstname;
        public string FirstName
        {
            get => _firstname;
            set
            {
                _firstname = value;
                OnPropertyChanged();
            }
        }

        private string _lastname;
        public string LastName
        {
            get => _lastname;
            set
            {
                _lastname = value;
                OnPropertyChanged();
            }
        }

        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        private string _phonenumber;
        public string PhoneNumber
        {
            get => _phonenumber;
            set
            {
                _phonenumber = value;
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
            if (string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName) ||
                string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(PhoneNumber))
            {
                MessageBox.Show("First Name, Last Name, Email, and Phone Number cannot be empty.");
                return false;
            }

            if (!IsValidEmail(Email))
            {
                MessageBox.Show("Invalid email format. Please enter a valid email.");
                return false;
            }

            if (!IsValidPhoneNumber(PhoneNumber))
            {
                MessageBox.Show("Phone number must contain at least 9 digits.");
                return false;
            }

            return true;
        }

        
        private bool IsValidEmail(string email)
        {
            var emailRegex = new Regex(@"^[^@]+@[^@]+\.[^@]+$");
            return emailRegex.IsMatch(email);
        }

        
        private bool IsValidPhoneNumber(string phoneNumber)
        {
            var phoneRegex = new Regex(@"^\d{9,}$");
            return phoneRegex.IsMatch(phoneNumber);
        }

        public void AddClient()
        {
            if (!IsValid())
                return;

            var newClient = new Client
            {
                FirstName = this.FirstName,
                LastName = this.LastName,
                Email = this.Email,
                PhoneNumber = this.PhoneNumber
            };

            _context.Clients.Add(newClient);
            _context.SaveChanges();
            Clients.Add(newClient);

            MessageBox.Show("Client added successfully ");
            ClearFields();
        }

        public void RemoveClient(Client client)
        {
            _context.Clients.Remove(client);
            _context.SaveChanges();
            Clients.Remove(client);
        }

        public void ClearFields()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
            PhoneNumber = string.Empty;
        }
    }
}
