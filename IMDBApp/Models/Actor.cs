using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMDBApp.Models
{
    public class Actor
    {
        public string ID { get; set; }
        public string Name { get; set; }

        public string Sex { get; set; }

        public string DOB { get; set; }

        public string BIO { get; set; }
    }
}