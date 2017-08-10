using System;
using Microsoft.Owin.Hosting;

namespace ProductService
{
    public class Program
    {
        static void Main()
        {
            string baseAddress = "http://localhost:9000/";

            // Start OWIN host 
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                Console.WriteLine("Hosting services at {0}", baseAddress);
                Console.WriteLine("Press any key to exit");
                Console.ReadLine();
            }
        }
    }
}