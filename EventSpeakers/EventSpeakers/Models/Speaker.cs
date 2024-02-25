using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSpeakers.Models
{
    internal class Speaker
    {
        public Speaker()
        {

        }

        public Speaker(string fullname, string position, string company, string imageurl)
        {
            Fullname = fullname;
            Position = position;
            Company = company;
            ImageURL = imageurl;
        }
        public int Id { get; set; }
        public string Fullname { get; set; }
        public string Position { get; set; }
        public string Company { get; set; }
        public string ImageURL { get; set; }

        public override string ToString()
        {
            return $"{Id}: {Fullname} - {Position},{Company} / {ImageURL}";
        }

    }
}
