using System;
using Covinfo.Action;

namespace Covinfo
{
    class Program
    {
        static void Main(string[] args)
        {
            ActionUser actionUser = new ActionUser();
            ActionAdmin actionAdmin = new ActionAdmin();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Selamat Datang di Covinfo, silakan pilih menu");
                Console.WriteLine("\nMenu: ");
                Console.WriteLine("1. Lihat Kasus Covid-19");
                Console.WriteLine("2. CovBot (Bot informasi Covid-19)");
                Console.WriteLine(!ActionAdmin.AdminStatus ? "3. Masuk sebagai admin" : "3. Menu admin");
                Console.WriteLine("4. exit");
                Console.Write("\nPilihan: ");

                try { 
                    int choose = int.Parse(Console.ReadLine());
                    switch (choose)
                    {
                        case 1:
                            Console.Clear();
                            actionUser.GetCovid().Wait();
                            MenuCovid();
                            break;
                        case 2:
                            Console.Clear();
                            actionUser.Bot();
                            break;
                        case 3:
                            if (!ActionAdmin.AdminStatus) actionAdmin.LoginAdmin(); else MenuAdmin();
                            break;
                        case 4:
                            goto exit;
                        default:
                            Console.WriteLine("Harap pilih menu yang tersedia");
                            Console.ReadLine();
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                }
            }
        exit:;
        }

        public static void MenuAdmin()
        {
            ActionAdmin actionAdmin = new ActionAdmin();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Menu Admin");
                Console.WriteLine("1. Lihat semua informasi");
                Console.WriteLine("2. Input informasi baru");
                Console.WriteLine("3. Edit informasi");
                Console.WriteLine("4. Hapus informasi");
                Console.WriteLine("5. CovBot (Bot informasi Covid-19)");
                Console.WriteLine("6. Kembali ke menu utama");
                Console.WriteLine("7. Logout");
                Console.Write("\nPilihan: ");

                try
                {
                    int choose = int.Parse(Console.ReadLine());
                    switch (choose)
                    {
                        case 1:
                            actionAdmin.GetInformation();
                            break;
                        case 2:
                            actionAdmin.NewInformation();
                            break;
                        case 3:
                            actionAdmin.EditInformation();
                            break;
                        case 4:
                            actionAdmin.DeleteInformation();
                            break;
                        case 5:
                            actionAdmin.Bot();
                            break;
                        case 6:
                            goto exit;
                        case 7:
                            actionAdmin.LogoutAdmin();
                            goto exit;
                        default:
                            Console.WriteLine("Harap pilih menu yang tersedia");
                            Console.ReadLine();
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            exit:;
        }

        public static void MenuCovid()
        {
            ActionUser actionUser = new ActionUser();
            while (true)
            {
                Console.WriteLine("Menu: ");
                Console.WriteLine("1. Lihat negara lain");
                Console.WriteLine("2. Bandingkan negara");
                Console.WriteLine("3. Kembali ke menu utama");
                Console.Write("Pilihan: ");

                try
                {
                    int choose = int.Parse(Console.ReadLine());
                    switch (choose)
                    {
                        case 1:
                            actionUser.GetSpecificCovid().Wait();
                            break;
                        case 2:
                            actionUser.CompareCovid().Wait();
                            break;
                        case 3:
                            goto exit;
                        default:
                            Console.WriteLine("Harap pilih menu yang tersedia");
                            _ = Console.ReadLine();
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        exit:;
        }


    }
}
