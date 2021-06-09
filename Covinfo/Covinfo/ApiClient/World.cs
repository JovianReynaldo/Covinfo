using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Timers;

namespace Covinfo.ApiClient
{
    class World : ApiClient, DataCovid
    {
        public async Task<Response> GetData(string dummy = "")
        {
            Response result = null;
            HttpResponseMessage response = await client.GetAsync("/api");
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    HttpContent content = response.Content;
                    string json = await content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<Response>(json);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Tidak dapat menemukan data");
                Console.WriteLine();
            }
            return result;
        }

        public override void Loading(object sender, ElapsedEventArgs e)
        {
            if (count % 3 == 0)
            {
                Console.Clear();
                Console.Write("Mengambil data global.");
            }
            else
            {
                Console.Write(".");
            }
            count++;
        }
    }
}
