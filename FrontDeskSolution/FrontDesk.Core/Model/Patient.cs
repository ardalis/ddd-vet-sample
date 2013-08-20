using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontDesk.Core.Model
{
    public class Client
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Salutation { get; set; }
        public string PreferredName { get; set; }

        private const string LongNameFormat = "{0} {1} \"{2}\" {3}";
        public override string ToString()
        {
            return String.Format(LongNameFormat, Salutation, FirstName, PreferredName, LastName);
        }
    }
}
