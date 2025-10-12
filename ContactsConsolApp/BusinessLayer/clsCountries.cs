using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class clsCountries
    {

        public enum enMode { Add = 0, Update = 1 }

        public enMode Mode = enMode.Add;

        public int CountryID { set; get; }

        public string CountryName { set; get; }
        
        public clsCountries()
        {

            this.CountryID = -1;
            this.CountryName = "";
        }

        private clsCountries(int CountryID, string CountryName)
        {

            this.CountryID = CountryID;
            this.CountryName = CountryName;

            Mode = enMode.Update;
        }

        public static clsCountries Find(int CountryID)
        {

            string CountryName = "";

            if (clsDataAccessLayer.FindCountryByID(CountryID, ref CountryName))
                return new clsCountries(CountryID, CountryName);
            else
                return null;
        }
    }
}
