using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Information_system__Countries_of_Europe
{
    public class Country
    {

        public string Name { get; set; }
        public string NameEng { get; set; }
        public string Capital { get; set; }       /*Пока что string, а потом может быть capital, мб лишнее*/
        public string Language { get; set; }
        public int Square { get; set; }
        public int Population { get; set; }
        public string Flag { get; set; }

        /*public Sight Sight { get; set; }*/

        /*public string Neighbor { get; set; }*/

        public Country(string name,string namee, string cap, string lang, int sq, int pop, string flag)
        {
            Name = name;
            NameEng = namee;
            Capital = cap;
            Language = lang;
            Square = sq;
            Population = pop;
            Flag = flag;
            
        }


        public string ShowName()
        {
            return string.Format("{0}", Name);
        }

    }
}
