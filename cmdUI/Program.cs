using System;
using System.Threading.Tasks;
using Robot;

namespace cmdUI
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var query = Input("Enter a query : ");
            var nbPages = Input("How many pages of 50 items : ");
            var location = Input("Area : ");
            var scraper = new Indeed(query, Int16.Parse(nbPages), location);

            var urls = scraper.MakeUrls();

            foreach (var url in urls)
            {
                Console.WriteLine(url);
            }

            var allJobs = await scraper.GetJobsAsync();
            foreach (var job in allJobs)
            {
                Console.WriteLine(job.Title);
            }
        }

        public static string Input(string msg)
        {
            Console.Write(msg);
            return Console.ReadLine();
        }
    }
}
