using System;

namespace Covinfo.Action
{
    abstract class Action
    {
        public void Bot()
        {
            Model model = new Model();
            Console.WriteLine("CovBot (Bot informasi Covid-19)");
            Console.WriteLine("(ketik 'keluar' untuk kembali ke halaman utama)");
            
            while (true)
            {
                Console.Write("Anda: ");
                string keyword = Console.ReadLine();

                if (keyword == "keluar") break;

                FAQ data = model.Bot(keyword);
                Console.Write("Sistem: ");
                
                if (data != null) { 
                    Console.WriteLine(data.head);
                    Console.WriteLine(data.body);
                    Console.WriteLine();
                } else {
                    Console.WriteLine("Tidak ada informasi yang ditemukan");
                    Console.WriteLine();
                }


            }
        }
    }
}
