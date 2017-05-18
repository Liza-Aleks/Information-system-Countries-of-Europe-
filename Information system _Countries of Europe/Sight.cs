using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Information_system__Countries_of_Europe
{
    [Serializable]
   public  class Sight
    {
        public string Name { get; set; }

        public int Year { get; set; }

        public string Information { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string CountryE { get; set; }

        public Sight(string name,int year, string inf, string city,string cou, string coue)
        {
            Name = name;
            Year = year;
            Information = inf;
            City = city;
            Country = cou;
            CountryE = coue;


        }
        public Sight()
        {

        }


        public string Show()
        {
            return string.Format("{0},{1}",Name,City);
        }


    }
}
