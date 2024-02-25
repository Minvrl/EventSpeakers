using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSpeakers.Models
{
    internal class Eventt
    {
        public Eventt()
        {
                
        }

        public Eventt(string name,string description,string adress, DateTime startdate, string startsat, string endsat)
        {
            Name = name;
            Description = description;
            Adress = adress;
            StartDate = startdate;  
            StartsAt = startsat;
            EndsAt = endsat;

        }
        public int Id { get; set; } 
        public string Name { get; set; }    
        public string Description { get; set; }
        public string Adress { get; set; }
        public DateTime StartDate { get; set; }
        public string StartsAt { get; set; }  
        public string EndsAt { get; set; }

        public override string ToString()
        {
            return $"{Id}: {Name} - {Description} // {Adress},{StartDate.ToString("dd.MM.yyyy")},{StartsAt}-{EndsAt}";
        }


    }
}
