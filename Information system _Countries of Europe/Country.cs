using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Information_system__Countries_of_Europe
{
    [Serializable]
    public class Country 
    {

        private string _name;
        public string Name
        {
            get { return _name; }
            set { if (!String.IsNullOrWhiteSpace(value))
                    _name = value;
                else
                    throw new Exception();
                 }
        }

        private string _capital;
        public string Capital
        {
            get { return _capital; }
            set {
                if (!String.IsNullOrWhiteSpace(value))
                    _capital = value;
                else
                    throw new Exception();
            }
        }

        private string _language;

        public string Language
        {
            get { return _language; }
            set {
                if (!String.IsNullOrWhiteSpace(value))
                    _language = value;
                else
                    throw new Exception();
            }
        }

        private float _square;

        public float Square
        {
            get { return _square; }
            set {
                if (value>=0)
                    _square = value;
                else
                    throw new Exception();
            }
        }

        private float _population;

        public float Population
        {
            get { return _population; }
            set {if(value>=0)
                _population = value;
            else
              throw new Exception();
            }
        }

        private string _flag;
        public string Flag
        {
            get { return _flag; }
            set {
                if (!String.IsNullOrWhiteSpace(value))
                    _flag = value;
                else
                    throw new Exception();
            }
        }

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
