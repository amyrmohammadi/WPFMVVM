using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reservoom.DbContexts;
using Reservoom.DTOs;

namespace Reservoom.Services.PersonAdder
{
    public interface IPersonAdder
    {
        Task AddPerson(PersonDTO person);
    }
    public class DatabasePersonAdder : IPersonAdder
    {
        private ReservoomDbContextFactory _dbContextFactory;
        public DatabasePersonAdder(ReservoomDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task AddPerson(PersonDTO person )
        {
            using(var context = _dbContextFactory.CreateDbContext())
            {
                await context.People.AddAsync(person);
                await context.SaveChangesAsync();
            }
        }
    }
}
