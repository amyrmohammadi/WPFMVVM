using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reservoom.DTOs;
using Reservoom.Services.PersonAdder;
using Reservoom.Services.PersonProvider;
using Reservoom.ViewModels;

namespace Reservoom.Stores
{
    public class PeopleStore
    {
        public List<PersonDTO> _people;
        private Lazy<Task> _initializeLazy;
        private readonly IPersonAdder _personAdder;
        private readonly IPersonProvider _personProvider;
        public event Action<PersonDTO> PersonMade;

        public PeopleStore(IPersonAdder personAdder, IPersonProvider personProvider)
        {
            _people = new List<PersonDTO>();
            _initializeLazy = new Lazy<Task>(Initialize);
            _personAdder = personAdder;
            _personProvider = personProvider;
        }


        public async Task AddPerson(PersonDTO person)
        {
           await _personAdder.AddPerson(person);
           _people.Add(person);
           OnPeopleMade(person);
        }

        private void OnPeopleMade(PersonDTO person)
        {
            PersonMade?.Invoke(person);
        }

        public async Task Load()
        {
            try
            {
                await _initializeLazy.Value;
            }
            catch (Exception)
            {
                _initializeLazy = new Lazy<Task>(Initialize);
                throw;
            }
        }
        private async Task Initialize()
        {
            List<PersonDTO> people = (await _personProvider.GetAllPerson()).ToList();
            _people.Clear();
            _people.AddRange(people);
        }
    }
}
