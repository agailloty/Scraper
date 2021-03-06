using System;
using System.Linq;

namespace Persistence
{
    public class JobOffer
    {
        public int Id { get; set; }
        private string _title;
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value.Replace("nouveau", "");
            }
        }
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