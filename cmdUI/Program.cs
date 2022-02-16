using System;
using Robot;

namespace cmdUI
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = Input("Entrer a valid URL");
            var response = Indeed.GetUrl(url).Result;
            Console.WriteLine(response);
        }

        public static string Input(string msg)
        {
            Console.WriteLine(msg);
            return Console.ReadLine();
        }
    }
}
