using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reservoom.DTOs;
using Reservoom.ViewModels;

namespace Reservoom.Services.PersonProvider
{
    public interface IPersonProvider
    {
        Task<List<PersonDTO>> GetAllPerson();

    }
}
