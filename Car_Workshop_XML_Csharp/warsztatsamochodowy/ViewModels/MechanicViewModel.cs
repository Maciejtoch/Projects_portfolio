using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using warsztatsamochodowy.Commands;
using warsztatsamochodowy.Data;
using warsztatsamochodowy.Models;

namespace warsztatsamochodowy.ViewModels
{
    public class MechanicViewModel : INotifyPropertyChanged
    {
        public ICommand AddMechanicCommand { get; set; }
        private readonly AppDbContext _context;

        public MechanicViewModel()
        {
            _context = new AppDbContext();
            Mechanics = new ObservableCollection<Mechanic>(_context.Mechanics.ToList());
            AddMechanicCommand = new RelayCommand(AddMechanic);
        }

        private ObservableCollection<Mechanic> _mechanics;
        public ObservableCollection<Mechanic> Mechanics
        {
            get => _mechanics;
            set
            {
                _mechanics = value;
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

        private string _specialization;
        public string Specialization
        {
            get => _specialization;
            set
            {
                _specialization = value;
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
            if (string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName) || string.IsNullOrWhiteSpace(Specialization))
            {
                MessageBox.Show("First Name, Last Name, and Specialization cannot be empty.");
                return false;
            }
            return true;
        }

        public void AddMechanic()
        {
            if (!IsValid())
                return;

            var newMechanic = new Mechanic
            {
                FirstName = this.FirstName,
                LastName = this.LastName,
                Specialization = this.Specialization
            };

            _context.Mechanics.Add(newMechanic);
            _context.SaveChanges();
            Mechanics.Add(newMechanic);

            MessageBox.Show("Mechanic added successfully ");
            ClearFields();
        }

        public void RemoveMechanic(Mechanic mechanic)
        {
            _context.Mechanics.Remove(mechanic);
            _context.SaveChanges();
            Mechanics.Remove(mechanic);
        }

        public void ClearFields()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            Specialization = string.Empty;
        }
    }
}
