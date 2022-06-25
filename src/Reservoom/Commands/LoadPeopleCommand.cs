using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reservoom.Stores;
using Reservoom.ViewModels;

namespace Reservoom.Commands
{
    public class LoadPeopleCommand : AsyncCommandBase
    {
        private readonly PeopleStore _peopleStore;
        private readonly PersonListViewModel _personListViewModel;

        public LoadPeopleCommand(PeopleStore peopleStore, PersonListViewModel personListViewModel)
        {
            _peopleStore = peopleStore;
            _personListViewModel = personListViewModel;
        }

        public override async  Task ExecuteAsync(object parameter)
        {
            _personListViewModel.ErrorMessage = string.Empty;
            try
            {
                _peopleStore.Load();

                _personListViewModel.UpdatePeople(_peopleStore._people);              
            }
            catch (Exception e)
            {
                _personListViewModel.ErrorMessage = "Failed to load reservations.";               
                throw;
            }
        }
    }
}
