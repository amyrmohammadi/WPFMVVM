using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;
using Reservoom.Commands;
using Reservoom.DTOs;
using Reservoom.Services;
using Reservoom.Stores;

namespace Reservoom.ViewModels
{
    public class PersonListViewModel : ViewModelBase
    {

        #region Commands
        public ICommand AddPersonCommand { get; }
        public ICommand LoadPersonCommand { get; }
        public ICommand GoToReservationListCommand { get; }
        #endregion

        #region Stores
        private PeopleStore _peopleStore;
        #endregion

        #region ShowData
        private readonly ObservableCollection<PersonViewModel> people;
        public IEnumerable<PersonViewModel> People => people;
        #endregion

        #region ErrorHandling
        private string errorMessages;

        public string ErrorMessage
        {
            get { return errorMessages; }
            set { 
                errorMessages = value;
                OnPropertyChanged(nameof(ErrorMessage));
                OnPropertyChanged(nameof(HasError));
            }
        }

        public bool HasError => !string.IsNullOrEmpty(ErrorMessage);

        #endregion


        public PersonListViewModel(NavigationService<AddPersonViewModel> addPersonNavigationService
            , PeopleStore peopleStore,
            NavigationService<ReservationListingViewModel> reservationListNavigationService)
        {
            AddPersonCommand = new NavigateCommand<AddPersonViewModel>(addPersonNavigationService);
            LoadPersonCommand = new LoadPeopleCommand(peopleStore, this);
            GoToReservationListCommand = new NavigateCommand<ReservationListingViewModel>(reservationListNavigationService);
            people = new ObservableCollection<PersonViewModel>();
            _peopleStore = peopleStore;
            _peopleStore.PersonMade += OnPeopleMode;
            people.CollectionChanged += OnPeopleChanged;

        }

        public static PersonListViewModel LoadViewModel(
            NavigationService<AddPersonViewModel> addPersonNavigation,
            PeopleStore peopleStore,
            NavigationService<ReservationListingViewModel> goToReservationListCommand
            )
        {
            PersonListViewModel viewModel =
               new PersonListViewModel(addPersonNavigation, peopleStore,goToReservationListCommand);

            viewModel.LoadPersonCommand.Execute(null);
            return viewModel;
        }

        public void UpdatePeople(List<PersonDTO> p)
        {
            people.Clear();
            foreach (var item in p)
            {
                var person = new PersonViewModel()
                {
                    Age = item.Age,
                    Id = item.Id,
                    UserName = item.UserName
                };
                people.Add(person);
            }
        }

        public override void Dispose()
        {
            _peopleStore.PersonMade -= OnPeopleMode;
            base.Dispose();
        }

        #region EventHandler
        private void OnPeopleMode(PersonDTO person)
        {
            PersonViewModel viewModel = new PersonViewModel()
            {
                Age = person.Age,
                Id = person.Id,
                UserName = person.UserName
            };
            people.Add(viewModel);
        }

        private void OnPeopleChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // OnPropertyChanged(nameof());
        }
        #endregion
    }
}
