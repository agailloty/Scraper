using System;
using System.Linq;

namespace Persistence
{
    using System;
    using System.Linq;
    public class JobOffer
    {
        public string Title { get; set; }
        public string Company { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        private string _nbDays { get; set; }
        public string DaysAgo
        {
            get => _nbDays;
            set
            {
                if (value.Contains("Il y a"))
                {
                    _nbDays = string.Join("", value.ToCharArray().Where(Char.IsDigit));
                }
                else
                {
                    _nbDays = "0";
                }
            }
        }
        public DateTime Published
        { get => DateTime.Now.AddDays(-Int32.Parse(DaysAgo)); }
    }
}