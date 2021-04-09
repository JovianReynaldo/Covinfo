using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Timers;

namespace Covinfo.ApiClient
{
    abstract class ApiClient
    {
        public HttpClient client = new HttpClient();
        public int count = 0;
        public ApiClient()
        {
            client.BaseAddress = new Uri("https://covid19.mathdro.id/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public virtual void Loading(object sender, ElapsedEventArgs e)
        {
            if (count % 3 == 0)
            {
                Console.Clear();
                Console.Write("Menghubungkan.");
            } else
            {
                Console.Write(".");
            }
            count++;
        }
    }

    interface DataCovid
    {
        Task<Response> GetData(string country);
    }
}