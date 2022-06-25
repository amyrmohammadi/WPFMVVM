using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Reservoom.DbContexts;
using Reservoom.DTOs;
using Reservoom.ViewModels;

namespace Reservoom.Services.PersonProvider
{
    public class DataBasePersonProvider : IPersonProvider
    {
        private readonly ReservoomDbContextFactory _reservoomDbContextFactory;
        public DataBasePersonProvider(ReservoomDbContextFactory reservoomDbContextFactory)
        {
            _reservoomDbContextFactory = reservoomDbContextFactory;
        }

        public async Task<List<PersonDTO>> GetAllPerson()
        {
            using (var context = _reservoomDbContextFactory.CreateDbContext())
            {
                var perople =await context.People.ToListAsync();
                return perople;
            }
        }
    }
}
