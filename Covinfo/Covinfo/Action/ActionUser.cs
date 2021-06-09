using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;
using Covinfo.ApiClient;

namespace Covinfo.Action
{
    class ActionUser : Action
    {
        public async Task GetCovid()
        {
            World world = new World();
            Country country = new Country();

            //set loading
            Timer worldTimer = new Timer();
            Timer countryTimer = new Timer();
            worldTimer.Interval = 1000;
            countryTimer.Interval = 1000;
            worldTimer.Elapsed += new ElapsedEventHandler(world.Loading);
            countryTimer.Elapsed += new ElapsedEventHandler(country.Loading);

            //process
            worldTimer.Start();
            Response worldResult = await world.GetData();
            worldTimer.Stop();
            world.count = 0;

            countryTimer.Start();
            Response countryResult = await country.GetData();
            countryTimer.Stop();

            if (worldResult != null && countryResult != null)
            {
                PrintResult(worldResult, countryResult, "Indonesia");
            }
        }
        public async Task GetSpecificCovid()
        {
            World world = new World();
            Country country = new Country();

            //set loading
            Timer worldTimer = new Timer();
            Timer countryTimer = new Timer();
            worldTimer.Interval = 1000;
            countryTimer.Interval = 1000;
            worldTimer.Elapsed += new ElapsedEventHandler(world.Loading);
            countryTimer.Elapsed += new ElapsedEventHandler(country.Loading);

            //input negara
            Console.Write("\nNegara: ");
            string input = Console.ReadLine();

            //process
            Console.Clear();
            worldTimer.Start();
            Response worldResult = await world.GetData();
            worldTimer.Stop();
            world.count = 0;

            countryTimer.Start();
            Response countryResult = await country.GetData(input);
            countryTimer.Stop();

            if (worldResult != null && countryResult != null)
            {
                PrintResult(worldResult, countryResult, input);
            }
        }
        public async Task CompareCovid()
        {
            World world = new World();
            Country country = new Country();

            //set loading
            Timer worldTimer = new Timer();
            Timer countryTimer = new Timer();
            worldTimer.Interval = 1000;
            countryTimer.Interval = 1000;
            worldTimer.Elapsed += new ElapsedEventHandler(world.Loading);
            countryTimer.Elapsed += new ElapsedEventHandler(country.Loading);
            try
            {
                Console.Write("Jumlah negara (max 5): ");
                int count = int.Parse(Console.ReadLine());

                List<string> listCountry = new List<string>();
                List<Response> listCountryResult = new List<Response>();
                int i = 0;

                if (count > 1 && count <= 5)
                {
                    while (i < count)
                    {
                        Console.Write("Negara " + (i + 1) + ": ");
                        listCountry.Add(Console.ReadLine());
                        i++;
                    }


                    worldTimer.Start();
                    Response worldResult = await world.GetData(); ;
                    worldTimer.Stop();
                    world.count = 0;

                    countryTimer.Start();
                    foreach (string data in listCountry)
                    {
                        listCountryResult.Add(await country.GetData(data));
                    }
                    countryTimer.Stop();


                    Console.Clear();
                    Console.WriteLine("Kasus Covid-19 Global: ");
                    Console.WriteLine("- Terkonfirmasi: " + worldResult.Confirmed.Value);
                    Console.WriteLine("- Sembuh: " + worldResult.Recovered.Value);
                    Console.WriteLine("- Meninggal: " + worldResult.Deaths.Value);
                    Console.WriteLine();

                    int index = 0;
                    foreach (Response countryResult in listCountryResult)
                    {
                        if (countryResult != null)
                        {
                            Console.WriteLine("Kasus Covid-19 " + listCountry[listCountryResult.IndexOf(countryResult)] + ": ");
                            Console.WriteLine("- Terkonfirmasi: " + countryResult.Confirmed.Value);
                            Console.WriteLine("- Sembuh: " + countryResult.Recovered.Value);
                            Console.WriteLine("- Meninggal: " + countryResult.Deaths.Value);
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.WriteLine("Tidak dapat menemukan negara " + listCountry[index]);
                            Console.WriteLine();
                        }
                        index++;
                    }
                }
                else if (count <= 1)
                {
                    Console.Write("Minimal 2 negara");
                    Console.ReadLine();
                    Console.Clear();
                }
                else
                {
                    Console.Write("Jumlah melebihi batas yang ditentukan");
                    Console.ReadLine();
                    Console.Clear();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void PrintResult(Response worldResult, Response countryResult, String country)
        {
            Console.Clear();
            Console.WriteLine("Kasus Covid-19 Global: ");
            Console.WriteLine("- Terkonfirmasi: " + worldResult.Confirmed.Value);
            Console.WriteLine("- Sembuh: " + worldResult.Recovered.Value);
            Console.WriteLine("- Meninggal: " + worldResult.Deaths.Value);
            Console.WriteLine();
            Console.WriteLine("Kasus Covid-19 " + country + ": ");
            Console.WriteLine("- Terkonfirmasi: " + countryResult.Confirmed.Value);
            Console.WriteLine("- Sembuh: " + countryResult.Recovered.Value);
            Console.WriteLine("- Meninggal: " + countryResult.Deaths.Value);
            Console.WriteLine();
        }

    }
}
