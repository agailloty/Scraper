using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Persistence;
using HtmlAgilityPack;
using System.Linq;
using System.Web;

namespace Robot
{
    public class Indeed
    {
        public string response { get; set; }
        public HtmlDocument htmlDoc { get; set; }
        public int MaxValue { get; }
        public string Location { get; }
        public string Query { get; }
        public int NbPages { get; set; }
        public Indeed(string query, int nbPages, string location = "France")
        {
            NbPages = nbPages;
            Query = query;
            Location = location;
        }

        public List<string> MakeUrls()
        {
            var urls = new List<string>();
            string baseUrl = "http://fr.indeed.com";
            string query = HttpUtility.UrlEncode(Query);
            string location = HttpUtility.UrlEncode(Location);
            urls.Add(baseUrl + "/emplois?q=" + query + "&l=" + location + "&limit=50");

            if (NbPages == 1)
                return urls;
            int currStart = 50;
            for (int i = 1; i < NbPages; i++)
            {
                string url = $"https://fr.indeed.com/emplois?as_and={query}&limit={50}&start={currStart}&l={location}";
                currStart += 50;
                urls.Add(url);
            }

            return urls;
        }

        public List<JobOffer> GetJobs()
        {
            var urls = MakeUrls();
            var allJobs = new List<List<JobOffer>>();
            foreach (var url in urls)
            {
                var job = ParseHtml(url);
                allJobs.Add(job);
            }

            return allJobs.SelectMany(i => i).ToList();
        }
        public static string GetUrl(string url)
        {
            HttpClient client = new HttpClient();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls13;
            client.DefaultRequestHeaders.Accept.Clear();
            var response = client.GetStringAsync(url).Result;
            return response;
        }



        List<JobOffer> ParseHtml(string url)
        {
            var response = GetUrl(url);
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(response);
            var res = htmlDoc.DocumentNode.Descendants("h2").
                Where(node => node.GetAttributeValue("class", "").
                Contains("jobTitle") || node.GetAttributeValue("class", "").
                Contains("new"));

            int nbJobs = GetByClass(htmlDoc, "div", "class", "job_seen_beacon").Count;
            var jobTitles = GetByClass(htmlDoc, "h2", "class", "jobTitle");
            var companyNames = GetByClass(htmlDoc, "span", "class", "companyName");
            var companyLocations = GetByClass(htmlDoc, "div", "class", "companyLocation");
            var jobSnippets = GetByClass(htmlDoc, "div", "class", "job-snippet");
            var daysAgo = GetByClass(htmlDoc, "span", "class", "date");

            var Jobs = new List<JobOffer>();

            for (int i = 0; i < nbJobs; i++)
            {
                var job = new JobOffer();
                job.Title = jobTitles.ElementAt(i).Replace("nouveau", "");
                job.Company = companyNames.ElementAt(i);
                job.City = companyLocations.ElementAt(i);
                job.Description = jobSnippets.ElementAt(i);
                job.DaysAgo = daysAgo.ElementAt(i);
                Jobs.Add(job);
            }
            return Jobs;
        }

        static List<string> GetByClass(HtmlDocument htmlDoc, string descendant, string attribute, string className)
        {
            var res = htmlDoc.DocumentNode.Descendants(descendant).
            Where(node => node.GetAttributeValue(attribute, "").
                  Contains(className));
            return res.Select(val => val.InnerText).ToList();
        }

    }
}