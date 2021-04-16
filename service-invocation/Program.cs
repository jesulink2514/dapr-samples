using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace service_invocation
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var dapperPort = Environment.GetEnvironmentVariable("DAPR_HTTP_PORT") ?? "3500";
            var httpClient = new HttpClient();

            while (true)
            {
                var response = await httpClient.PostAsJsonAsync($"http://localhost:{dapperPort}/v1.0/invoke/orders/method/order", new
                {
                    id = new Random().Next(),
                    amount = 1000 * new Random().NextDouble()
                });

                Console.WriteLine(await response.Content.ReadAsStringAsync());

                await Task.Delay(2000);
            }
        }
    }
}
