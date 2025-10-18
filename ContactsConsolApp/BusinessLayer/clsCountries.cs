using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
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

        public string Code { set; get; }

        public string PhoneCode { set; get; }
        
        public clsCountries()
        {

            this.CountryID = -1;
            this.CountryName = "";
            this.Code = "";
            this.PhoneCode = "";
        }

        private clsCountries(int CountryID, string CountryName, string Code, string PhoneCode)
        {

            this.CountryID = CountryID;
            this.CountryName = CountryName;
            this.Code = Code;
            this.PhoneCode = PhoneCode;

            Mode = enMode.Update;
        }

        public static clsCountries Find(int CountryID)
        {

            string CountryName = "";
            string Code = "";
            string PhoneCode = "";

            if (clsDataAccessLayer.FindCountryByID(CountryID, ref CountryName, ref Code, ref PhoneCode))
                return new clsCountries(CountryID, CountryName, Code, PhoneCode);
            else
                return null;
        }

        public static bool DeleteCountry(int CountryID)
        {

            return clsDataAccessLayer.DeleteCountry(CountryID);
        }

        public static clsCountries Find(string CountryName)
        {

            int CountryID = -1;
            string Code = "";
            string PhoneCode = "";

            if (clsDataAccessLayer.FindCountryByName(ref CountryID, CountryName, ref Code, ref PhoneCode))
            {
                return new clsCountries(CountryID, CountryName, Code, PhoneCode);
            }
            else
            {
                return null;
            }
        }

        public static DataTable ListCountries()
        {

            return clsDataAccessLayer.GetAllCountries();
        }

        public static bool IsCountryExistByID(int CountryID)
        {

            return clsDataAccessLayer.IsCountryExistByCountryID(CountryID);
        }

        public static bool IsCountryExistByCountryName(string CountryName)
        {

            return (clsDataAccessLayer.IsCountryExistByCountryName(CountryName));
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
