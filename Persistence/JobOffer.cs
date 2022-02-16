using System;
using System.Linq;

namespace Persistence
{
    public class JobOffer
    {
        public string Title { get; set; }
        public string Company { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        private DateTime _published;
        public DateTime Published
        {
            get => _published;
            set
            {
                string daysAgo = value.ToString();
                var nbDays = -Int32.Parse(string.Join("", daysAgo.ToCharArray().Where(Char.IsDigit)));
                _published = DateTime.Now.AddDays(nbDays);
            }
        }
    }
}