using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MonkeyApp.Model;

namespace MonkeyApp.Services
{
    public class MonkeyService
    {
        HttpClient httpClient;

        List<Monkey> monkeys = new List<Monkey>();

        public MonkeyService()
        {
            httpClient = new HttpClient();
        }

        public async Task<List<Monkey>> GetMonkeys()
        {
            if (monkeys.Any())
            {
                return monkeys;
            }

            var url = "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/MonkeysApp/monkeydata.json";

            var responce = await httpClient.GetAsync(url);

            if (responce.IsSuccessStatusCode)
            {
                monkeys = await responce.Content.ReadFromJsonAsync<List<Monkey>>();
            }

            //using var stream = await FileSystem.OpenAppPackageFileAsync("monkeydata.json");
            //using var reader = new StreamReader(stream);
            //var contents = await reader.ReadToEndAsync();
            //monkeys = JsonSerializer.Deserialize<List<Monkey>>(contents);



            return monkeys;
        }
    }
}
