using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Input;
using warsztatsamochodowy.Commands;
using warsztatsamochodowy.Data;
using warsztatsamochodowy.Models;

namespace warsztatsamochodowy.ViewModels
{
    public class JobViewModel : INotifyPropertyChanged
    {
        public ICommand AddJobCommand { get; set; }
        private readonly AppDbContext _context;

        public JobViewModel()
        {
            _context = new AppDbContext();
            Jobs = new ObservableCollection<Job>(_context.Jobs.ToList());
            AddJobCommand = new RelayCommand(AddJob);
        }

        private ObservableCollection<Job> _jobs;
        public ObservableCollection<Job> Jobs
        {
            get => _jobs;
            set
            {
                _jobs = value;
                OnPropertyChanged();
            }
        }

        private string _type;
        public string Type
        {
            get => _type;
            set
            {
                _type = value;
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
            if (string.IsNullOrWhiteSpace(Type))
            {
                MessageBox.Show("Job type cannot be empty.");
                return false;
            }
            return true;
        }

        public void AddJob()
        {
            if (!IsValid())
                return;

            var newJob = new Job
            {
                Type = this.Type
            };

            _context.Jobs.Add(newJob);
            _context.SaveChanges();
            Jobs.Add(newJob);

            MessageBox.Show("Job added successfully ");
            ClearFields();
        }

        public void RemoveJob(Job job)
        {
            _context.Jobs.Remove(job);
            _context.SaveChanges();
            Jobs.Remove(job);
        }

        public void ClearFields()
        {
            Type = string.Empty;
        }
    }
}
