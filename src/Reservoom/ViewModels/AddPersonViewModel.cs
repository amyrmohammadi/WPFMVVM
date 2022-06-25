using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Reservoom.Commands;
using Reservoom.Services;
using Reservoom.Stores;

namespace Reservoom.ViewModels
{
    public class AddPersonViewModel : ViewModelBase, INotifyDataErrorInfo
    {
        private int age;

        public int Age
        {
            get { return age; }
            set
            {
                age = value;
                OnPropertyChanged(nameof(age));
            }
        }

        private string userName;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public string UserName
        {
            get { return userName; }
            set
            {
                userName = value;
                if(userName == "amir")
                {
                    AddError("user name can not be amir",nameof(UserName));  
                }
                else
                {
                    ClearErrors(nameof(UserName));
                }
                OnPropertyChanged(nameof(UserName));
                OnPropertyChanged(nameof(CanCreateUser));
            }
        }

        public bool HasUserName => !string.IsNullOrWhiteSpace(UserName);

        public bool CanCreateUser
        {
           get{ return !string.IsNullOrWhiteSpace(userName); }
        }

        public ICommand AddUserCommand { get; }



        private readonly Dictionary<string, List<string>> _propertyNameToErrorsDictionary;

        public bool HasErrors => _propertyNameToErrorsDictionary.Any();

        public AddPersonViewModel(NavigationService<PersonListViewModel> personListNavigationService,
            PeopleStore peopleStore )
        {
            AddUserCommand = new AddUserCommand(personListNavigationService,this,peopleStore);
            _propertyNameToErrorsDictionary = new Dictionary<string, List<string>>();
        }


   
        public IEnumerable GetErrors(string propertyName)
        {
            return _propertyNameToErrorsDictionary.GetValueOrDefault(propertyName, new List<string>());
        }

        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        private void AddError(string errorMessage, string propertyName)
        {
            if (!_propertyNameToErrorsDictionary.ContainsKey(propertyName))
            {
                _propertyNameToErrorsDictionary.Add(propertyName, new List<string>());
            }

            _propertyNameToErrorsDictionary[propertyName].Add(errorMessage);

            OnErrorsChanged(propertyName);
        }

        private void ClearErrors(string propertyName)
        {
            _propertyNameToErrorsDictionary.Remove(propertyName);

            OnErrorsChanged(propertyName);
        }
    }
}
