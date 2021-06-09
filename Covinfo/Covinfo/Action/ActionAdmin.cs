using System;
using System.Collections.Generic;

namespace Covinfo.Action
{
    class ActionAdmin : Action
    {
        public static bool AdminStatus { get; set; }
        public void LoginAdmin()
        {
            Model model = new Model();
            bool loop = true;

            while (!AdminStatus && loop)
            {
                Console.Clear();
                Console.WriteLine("Masuk sebagai admin");
                Console.Write("username: ");
                string username = Console.ReadLine();
                
                Console.Write("password: ");
                string password = Console.ReadLine();

                AdminStatus = model.Login(username, password);
                if (!AdminStatus)
                {
                    while (true)
                    {
                        Console.Write("Incorrect, try again? (Y/N): ");
                        string option = Console.ReadLine().ToUpper();

                        if (option == "N")
                        {
                            goto exit;
                        }
                        else if (option == "Y")
                        {
                            break;
                        }
                    }
                }
                else
                {
                    Program.MenuAdmin();
                    break;
                }
            }

        exit:;
        }

        public void LogoutAdmin()
        {
            AdminStatus = false;
        }

        public void GetInformation()
        {
            try
            {
                Model model = new Model();
                List<FAQ> listData = model.GetAllFAQ();

                foreach (FAQ data in listData)
                {
                    Console.WriteLine("id: " + data.Id);
                    Console.WriteLine("keyword: " + data.Keyword);
                    Console.WriteLine("head: " + data.Head);
                    Console.WriteLine("body: \n" + data.Body);
                    Console.WriteLine();
                }
                Console.WriteLine("\n\nSelesai");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            _ = Console.ReadLine();
        }

        public void NewInformation()
        {
            try
            {
                Console.Write("keyword: ");
                string keyword = Console.ReadLine();

                Console.Write("head: ");
                string head = Console.ReadLine();

                Console.Write("body: ");
                string body = Console.ReadLine();

                Model model = new Model();
                model.CreateFAQ(keyword, head, body);
                Console.WriteLine("\n\n Success");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            _ = Console.ReadLine();
        }

        public void EditInformation()
        {
            try
            {
                Console.Write("id: ");
                int id = int.Parse(Console.ReadLine());

                Model model = new Model();
                FAQ data = model.GetFAQbyId(id);

                if (data != null)
                {
                    Console.Clear();
                    Console.WriteLine("id: " + data.Id);
                    Console.WriteLine("keyword: " + data.Keyword);
                    Console.WriteLine("head: " + data.Head);
                    Console.WriteLine("body: \n" + data.Body + "\n\n");

                    Console.WriteLine("New Data: ");

                    Console.Write("keyword: ");
                    string keyword = Console.ReadLine();
                    Console.Write("head: ");
                    string head = Console.ReadLine();
                    Console.Write("body: ");
                    string body = Console.ReadLine();

                    model.EditFAQ(keyword, head, body, data.Id);
                    Console.WriteLine("\n\n Success");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            _ = Console.ReadLine();
        }

        public void DeleteInformation()
        {
            try
            {
                Console.Write("id: ");
                int id = int.Parse(Console.ReadLine());

                Model model = new Model();
                model.DisableFAQ(id);
                Console.WriteLine("\n\n Success");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            _ = Console.ReadLine();
        }
    }
}
