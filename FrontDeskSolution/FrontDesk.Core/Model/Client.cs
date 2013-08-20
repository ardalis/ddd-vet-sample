using System;
using System.Linq;

namespace FrontDesk.Core.Model
{
    public class Client
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Salutation { get; set; }
        public string PreferredName { get; set; }

        public override string ToString()
        {
            string result = String.Empty;
            if (!String.IsNullOrWhiteSpace(this.Salutation))
            {
                result += this.Salutation;
            }
            if (!String.IsNullOrWhiteSpace(this.FirstName))
            {
                result = string.Format("{0} {1}", result, this.FirstName).Trim();
            }

            if (!String.IsNullOrWhiteSpace(this.PreferredName))
            {
                result = string.Format("{0} \"{1}\"", result, this.PreferredName).Trim();
            }

            if (!String.IsNullOrWhiteSpace(this.LastName))
            {
                result = string.Format("{0} {1}", result, this.LastName).Trim();
            }

            return result;
        }
    }
}