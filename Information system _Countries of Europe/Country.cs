﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Information_system__Countries_of_Europe
{
    [Serializable]
    public class Country 
    {
        
        public string Name { get; set; }
        public string Capital { get; set; }       /*Пока что string, а потом может быть capital, мб лишнее*/
        public string Language { get; set; }
        public float Square { get; set; }
        public float Population { get; set; }
        public string Flag { get; set; }
        public List<Sight> LSight { get; set; }

        /*public Sight Sight { get; set; }*/

        /*public string Neighbor { get; set; }*/

        public Country(string name, string cap, string lang, float sq, float pop,string flag, List<Sight> lsight)
        {
            Name = name;
            Capital = cap;
            Language = lang;
            Square = sq;
            Population = pop;
            Flag = flag;
            LSight = lsight;


            
        }
        public Country()
        {

        }


        public string ShowName()
        {
            return string.Format("{0}", Name);
        }

    }
}
