using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace Information_system__Countries_of_Europe
{
     public class Ser
    {

        static XmlSerializer serializer = new XmlSerializer(typeof(ListCountries));
        public static void Serialize(ListCountries listcou)
        {
            using (FileStream fs = new FileStream("../../allcountries.xml", FileMode.Create))
            {
                StreamWriter sw = new StreamWriter(fs, Encoding.Default);
              
                serializer.Serialize(sw, listcou);

                sw.Close();
                fs.Close();
            };
          
        }


       public static ListCountries DeSerialize(ListCountries listcou)
        {
            ListCountries AllCou = null;
            using (FileStream fs = new FileStream("../../allcountries.xml", FileMode.Open))
            {
                StreamReader sr = new StreamReader(fs, Encoding.Default);
                AllCou = (ListCountries) serializer.Deserialize(sr);


                sr.Close();
                fs.Close();
            };

            return AllCou;
        }




    }
}
