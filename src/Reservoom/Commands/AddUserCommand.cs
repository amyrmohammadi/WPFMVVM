using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using Reservoom.DTOs;
using Reservoom.Services;
using Reservoom.Stores;
using Reservoom.ViewModels;

namespace Reservoom.Commands
{
    public class AddUserCommand : AsyncCommandBase
    {

        private readonly AddPersonViewModel _addPersonViewModel;
        private readonly NavigationService<PersonListViewModel> _personListNavigationService;
        private readonly PeopleStore _peopleStore;
        public AddUserCommand(
            NavigationService<PersonListViewModel> personListNavigationService,
            AddPersonViewModel addPersonViewModel,
            PeopleStore peopleStore)
        {
            _addPersonViewModel = addPersonViewModel;
            _personListNavigationService = personListNavigationService;
            _addPersonViewModel.PropertyChanged += OnViewModelPropertyChanged;
            _peopleStore = peopleStore;
        }

        public override bool CanExecute(object parameter)
        {
            return _addPersonViewModel.CanCreateUser & base.CanExecute(parameter);
        }
        public override async Task ExecuteAsync(object parameter)
        {
            var person = new PersonDTO
            {
                Age = _addPersonViewModel.Age,
                UserName = _addPersonViewModel.UserName
            };
            try
            {
                _peopleStore.AddPerson(person);
                MessageBox.Show("successfuly added person", "Success",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                _personListNavigationService.Navigate();
            }
            catch (Exception)
            {

                throw;
            }


        }
        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(AddPersonViewModel.CanCreateUser))
            {
                OnCanExecutedChanged();
            }
        }
    }
}
