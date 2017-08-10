using System;
using Microsoft.Owin.Hosting;

namespace ClaimService
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseAddress = "http://localhost:9001/";

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
