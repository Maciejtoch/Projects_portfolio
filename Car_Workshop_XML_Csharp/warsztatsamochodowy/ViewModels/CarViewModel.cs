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
    public class CarViewModel : INotifyPropertyChanged
    {
        public ICommand AddCarCommand { get; set; }
        private readonly AppDbContext _context;

        public CarViewModel()
        {
            _context = new AppDbContext();
            Cars = new ObservableCollection<Car>(_context.Cars.ToList());
            AddCarCommand = new RelayCommand(AddCar);
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

        private string _brand;
        public string Brand
        {
            get => _brand;
            set
            {
                _brand = value;
                OnPropertyChanged();
            }
        }

        private string _model;
        public string Model
        {
            get => _model;
            set
            {
                _model = value;
                OnPropertyChanged();
            }
        }

        private int _year;
        public int Year
        {
            get => _year;
            set
            {
                _year = value;
                OnPropertyChanged();
            }
        }

        private string _registrationNumber;
        public string RegistrationNumber
        {
            get => _registrationNumber;
            set
            {
                _registrationNumber = value;
                OnPropertyChanged();
            }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
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
            if (string.IsNullOrWhiteSpace(Brand) || string.IsNullOrWhiteSpace(Model) ||
                string.IsNullOrWhiteSpace(RegistrationNumber))
            {
                ErrorMessage = "Brand, Model, and Registration Number cannot be empty.";
                return false;
            }

            if (!IsValidRegistrationNumber(RegistrationNumber))
            {
                ErrorMessage = "Invalid registration number format.";
                return false;
            }

            ErrorMessage = string.Empty; 
            return true;
        }

        
        private bool IsValidRegistrationNumber(string registrationNumber)
        {
            var regNumberRegex = new Regex(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]+$");
            return regNumberRegex.IsMatch(registrationNumber);
        }

        public void AddCar()
        {
            if (!IsValid())
            {
                MessageBox.Show(ErrorMessage);  
                return;
            }

            var newCar = new Car
            {
                Brand = this.Brand,
                Model = this.Model,
                Year = this.Year,
                RegistrationNumber = this.RegistrationNumber
            };

            _context.Cars.Add(newCar);
            _context.SaveChanges();
            Cars.Add(newCar);

            MessageBox.Show("Car added to database successfully ");

            ClearFields();
        }

        public void ClearFields()
        {
            Brand = string.Empty;
            Model = string.Empty;
            Year = 0;
            RegistrationNumber = string.Empty;
        }

    }
}
