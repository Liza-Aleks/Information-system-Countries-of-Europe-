using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Information_system__Countries_of_Europe
{
    class Sort
    {
       
        public static void C(List<Country> cou)
        {
            foreach (var item in cou)
            {
                cou.Sort((a, b) => a.Name.CompareTo(b.Name));
            }
        }
        public static void S(List<Sight> si)
        {
            foreach (var item in si)
            {
                si.Sort((a, b) => a.Name.CompareTo(b.Name));
            }
        }

    }
}
