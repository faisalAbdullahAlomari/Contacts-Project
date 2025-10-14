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

        private bool _AddNewCountry()
        {
            this.CountryID = clsDataAccessLayer.AddNewCountry(this.CountryName);

            return (this.CountryID != -1);
        }

        private bool _UpdateCountry()
        {

            return clsDataAccessLayer.UpdateCountry(this.CountryID, this.CountryName);
        }

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

        public static bool DeleteCountry(int CountryID)
        {

            return clsDataAccessLayer.DeleteCountry(CountryID);
        }

        public bool Save()
        {

            switch (Mode)
            {
                case enMode.Add:
                    if (_AddNewCountry())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateCountry();

                default:

                return false;
            }
        }
    }
}
