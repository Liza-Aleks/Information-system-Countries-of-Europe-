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

        public string Year { get; set; }

        public string Information { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string SightIm { get; set; }
        public Sight(string name,string year, string inf, string city,string cou, string img)
        {
            Name = name;
            Year = year;
            Information = inf;
            City = city;
            Country = cou;
            SightIm = img;


        }
        public Sight()
        {

        }


        public string Show()
        {
            return string.Format("{0},{1}",Name,City);
        }

        public string ShowC()
        {
            return string.Format("{0},{1},{2}", Name, City,Country);
        }
    }
}
