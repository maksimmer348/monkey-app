using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using MonkeyApp.Model;
using MonkeyApp.Services;
using MonkeyApp.View;

namespace MonkeyApp.VM
{
    public partial class MonkeysVM : BaseVM
    {
        MonkeyService monkeyService;
        public ObservableCollection<Monkey> Monkeys { get; } = new();

        //public Command GetMonkeyCommand { get; }

        IConnectivity connectivity;
        IGeolocation geolocation;


        public MonkeysVM(MonkeyService monkeyService, IConnectivity connectivity, IGeolocation geolocation)
        {
            Title = "Monkey Finder";
            this.monkeyService = monkeyService;
            this.connectivity = connectivity;
            this.geolocation = geolocation;
        }

        [RelayCommand]
        async Task GetCloseMonkeyAsync()
        {
            if (IsBusy || !Monkeys.Any()) return;

            try
            {
                //var ch = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
                //var status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();

                var location = await geolocation.GetLastKnownLocationAsync();
                location ??= await geolocation.GetLocationAsync(
                       new GeolocationRequest
                       {
                           //точность
                           DesiredAccuracy = GeolocationAccuracy.Medium,
                           Timeout = TimeSpan.FromSeconds(30)
                       });
                if (location is null) return;

                var firstMonkeyLocation = Monkeys.OrderBy(x => location.CalculateDistance(x.Latitude, x.Longitude, DistanceUnits.Kilometers)).FirstOrDefault();

                if (firstMonkeyLocation is null) return;

                await Shell.Current.DisplayAlert("Close monkey", $"Name: {firstMonkeyLocation.Name}, in {firstMonkeyLocation.Location}", "Ok");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error", $"Unable to get monkeys: {ex.Message}", "Ok");
            }
        }

        [RelayCommand]
        async Task GetMonkeyAsync()
        {
            if (IsBusy) return;

            try
            {
                if (connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await Shell.Current.DisplayAlert("internet error", $"Not internet! Check you internet and try again", "Ok");
                    return;
                }

                IsBusy = true;
                var monkeys = await monkeyService.GetMonkeys();

                if (monkeys.Any()) Monkeys.Clear();

                foreach (var monkey in monkeys)
                {
                    Monkeys.Add(monkey);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error", $"Unable to get monтрkeys: {ex.Message}", "Ok");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        async Task GetToDetailsAsync(Monkey monkey)
        {
            if (monkey is null) return;
            await Shell.Current.GoToAsync($"{nameof(DetailsPage)}?id={monkey.Name}", true, new Dictionary<string, object>
            {
                {"Monkey", monkey}
            });
        }
    }
}
