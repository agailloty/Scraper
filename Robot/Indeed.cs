using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Persistence;
using HtmlAgilityPack;
using System.Linq;

namespace Robot
{
    public class Indeed
    {
        public Indeed(string url, int maxValue, string location = "France")
        {
            response = GetUrl(url).Result;
            htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(response);
        }
        public string response { get; set; }
        public HtmlDocument htmlDoc { get; set; }
        public static async Task<string> GetUrl(string url)
        {
            HttpClient client = new HttpClient();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls13;
            client.DefaultRequestHeaders.Accept.Clear();
            var response = client.GetStringAsync(url);
            return await response;
        }

        public List<JobOffer> ParseHtml(string url)
        {
            var response = GetUrl(url);
            var htmlDoc = new HtmlDocument();
            var res = htmlDoc.DocumentNode.Descendants("h2").
                Where(node => node.GetAttributeValue("class", "").
                Contains("jobTitle") || node.GetAttributeValue("class", "").
                Contains("new"));

            int nbJobs = GetByClass("div", "class", "job_seen_beacon").Count;
            var jobTitles = GetByClass("h2", "class", "jobTitle");
            var companyNames = GetByClass("span", "class", "companyName");
            var companyLocations = GetByClass("div", "class", "companyLocation");
            var jobSnippets = GetByClass("div", "class", "job-snippet");
            var daysAgo = GetByClass("span", "class", "date");

            var Jobs = new List<JobOffer>();

            for (int i = 0; i < nbJobs; i++)
            {
                var job = new JobOffer();
                job.Title = jobTitles.ElementAt(i);
                job.Company = companyNames.ElementAt(i);
                job.City = companyLocations.ElementAt(i);
                job.Description = jobSnippets.ElementAt(i);
                job.DaysAgo = daysAgo.ElementAt(i);
                Jobs.Add(job);
            }
            return Jobs;
        }

        public List<string> GetByClass(string descendant, string attribute, string className)
        {
            var res = htmlDoc.DocumentNode.Descendants(descendant).
            Where(node => node.GetAttributeValue(attribute, "").
                  Contains(className));
            return res.Select(val => val.InnerText).ToList();
        }

    }
}