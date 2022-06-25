using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Reservoom.Services;
using Reservoom.Stores;
using Reservoom.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservoom.HostBuilders
{
    public static class AddViewModelsHostBuilderExtensions
    {
        public static IHostBuilder AddViewModels(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(services =>
            {


                services.AddTransient((s) => CreateReservationListingViewModel(s));
                services.AddSingleton<Func<ReservationListingViewModel>>((s) => () => s.GetRequiredService<ReservationListingViewModel>());
                services.AddSingleton<NavigationService<ReservationListingViewModel>>();

                services.AddTransient<MakeReservationViewModel>();
                services.AddSingleton<Func<MakeReservationViewModel>>((s) => () => s.GetRequiredService<MakeReservationViewModel>());
                services.AddSingleton<NavigationService<MakeReservationViewModel>>();

                services.AddTransient((s)=>CreatePersonListViewModel(s));
                services.AddSingleton<Func<PersonListViewModel>>((s) => () => s.GetRequiredService<PersonListViewModel>());
                services.AddSingleton<NavigationService<PersonListViewModel>>();

                services.AddTransient<AddPersonViewModel>();
                services.AddSingleton<Func<AddPersonViewModel>>((s) => () => s.GetRequiredService<AddPersonViewModel>());
                services.AddSingleton<NavigationService<AddPersonViewModel>>();

                services.AddSingleton<MainViewModel>();
            });

            return hostBuilder;
        }

        private static ReservationListingViewModel CreateReservationListingViewModel(IServiceProvider services)
        {
            return ReservationListingViewModel.LoadViewModel(
                services.GetRequiredService<HotelStore>(),
                services.GetRequiredService<NavigationService<MakeReservationViewModel>>(),
                services.GetRequiredService<NavigationService<PersonListViewModel>>());
        }

        public static PersonListViewModel CreatePersonListViewModel(IServiceProvider serviceProvider)
        {
            return PersonListViewModel.LoadViewModel(
                serviceProvider.GetRequiredService<NavigationService<AddPersonViewModel>>(),
                serviceProvider.GetRequiredService<PeopleStore>(),
                serviceProvider.GetRequiredService<NavigationService<ReservationListingViewModel>>()
                );
        }
    }
}
