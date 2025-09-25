using System;
using System.Data;
using DataAccessLayer;

namespace BusinessLayer
{
    public class clsContacts
    {

        private bool _AddNewContact()
        {

            this.ID = clsDataAccessLayer.AddNewContact(this.FirstName, this.LastName, this.Email, this.Phone, this.Address, this.DateOfBirth, this.CountryID, this.ImagePath);

            return (this.ID != -1);
        }

        private bool _UpdateContact()
        {

            return clsDataAccessLayer.UpdateContact(this.ID, this.FirstName, this.LastName, this.Email, this.Phone, this.Address, this.DateOfBirth, this.CountryID, this.ImagePath);
        }

        public enum enMode { AddNew = 0, Update = 1 };

        public enMode Mode = enMode.AddNew;

        public int ID { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string Email { set; get; }
        public string Phone { set; get; }
        public string Address { set; get; }
        public DateTime DateOfBirth { set; get; }
        public string ImagePath { set; get; }

        public int CountryID { set; get; }
        public clsContacts()
        {
            
            this.ID = -1;
            this.FirstName = "";
            this.LastName = "";
            this.Email = "";
            this.Phone = "";
            this.Address = "";
            this.DateOfBirth = DateTime.Now;
            this.CountryID = -1;
            this.ImagePath = "";
        }

        private clsContacts(int ID, string FirstName, string LastName,
            string Email, string Phone, string Address, DateTime DateOfBirth, int CountryID, string ImagePath)
        {
            
            this.ID = ID;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Email = Email;
            this.Phone = Phone;
            this.Address = Address;
            this.DateOfBirth = DateOfBirth;
            this.CountryID = CountryID;
            this.ImagePath = ImagePath;

            Mode = enMode.Update;
        }
        public static clsContacts Find(int ID)
        {
            string FirstName = "";
            string LastName = "";
            string Email = "";
            string Phone = "";
            string Address = "";
            string ImagePath = "";

            DateTime DateOfBirth = DateTime.Now;

            int CountryID = -1;

            if(clsDataAccessLayer.FindContactById(ID, ref FirstName, ref LastName, ref Email, ref Phone, ref Address, ref DateOfBirth, ref CountryID, ref ImagePath))
                return new clsContacts(ID, FirstName, LastName, Email, Phone, Address, DateOfBirth, CountryID, ImagePath);
            else
                return null;
        }

        public static bool DeleteContact(int ID)
        {
            return clsDataAccessLayer.DeleteContact(ID);
        }

        public static DataTable GetAllContacts()
        {

            return clsDataAccessLayer.GetAllContacts();
        }

        public static bool IsContactExist(int ID)
        {

            return clsDataAccessLayer.IsContactExist(ID);
        }

        public bool Save()
        {

            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewContact())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateContact();

                default:

                return false;
            }
        }
    }
}
