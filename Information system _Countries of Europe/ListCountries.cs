using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Information_system__Countries_of_Europe
{
   [Serializable]
   public class ListCountries
    {

        public List<Country> ListCou { get; set; }

        public ListCountries(List<Country> listCou)
        {
            
            ListCou = listCou;
        }
        public ListCountries()
        {

        }
    }
}
