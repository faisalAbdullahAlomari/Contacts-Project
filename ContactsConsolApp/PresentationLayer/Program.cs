using System;
using System.Data;
using BusinessLayer;
using Core;

namespace PresentationLayer
{
    internal class Program
    {
        static clsContacts FindContact()
        {
            clsContacts Contact = clsContacts.Find(clsInput.ReadInteger("Enter Contact ID: "));
            
            if (Contact != null)
            {
                Console.Clear();
                Console.WriteLine("Contact Found:");
                Console.WriteLine($"ID: {Contact.ID}");
                Console.WriteLine($"Name: {Contact.FirstName} {Contact.LastName}");
                Console.WriteLine($"Email: {Contact.Email}");
                Console.WriteLine($"Phone: {Contact.Phone}");
                Console.WriteLine($"Address: {Contact.Address}");
                Console.WriteLine($"Date of Birth: {Contact.DateOfBirth.ToShortDateString()}");
                Console.WriteLine($"Country ID: {Contact.CountryID}");
                Console.WriteLine($"Image Path: {Contact.ImagePath}");
            }
            else
            {
                Console.WriteLine("Contact Not Found.");
            }

            return Contact;
        }

        static void AddNewContact()
        {

            clsContacts Contact = new clsContacts();

            Contact.FirstName = clsInput.ReadString("Enter Your First Name: ");
            Contact.LastName = clsInput.ReadString("Enter Your Last Name: ");
            Contact.Email = clsInput.ReadString("Enter Your Email: ");
            Contact.Phone = clsInput.ReadString("Enter Your Phone Number: ");
            Contact.Address = clsInput.ReadString("Enter Your Address: ");
            Contact.DateOfBirth = new DateTime(
                clsInput.ReadInteger("Enter The Year Of Birth: ")
                ,clsInput.ReadInteger("Enter The Month Of Birth: ")
                ,clsInput.ReadInteger("Enter The Day Of Birth: ") ,0 ,0 ,0);
            Contact.CountryID = clsInput.ReadInteger("Enter The Country ID: ");
            Contact.ImagePath = clsInput.ReadString("Enter Image Path: ");

            if (Contact.Save())
            {

                Console.WriteLine("Contact Added Successfully With ID = " + Contact.ID);
            }
        }

        static void UpdateContact()
        {

            clsContacts Contact = FindContact();

            if (Contact != null)
            {
                Contact.FirstName = clsInput.ReadString("\n\nEnter Your First Name: ");
                Contact.LastName = clsInput.ReadString("Enter Your Last Name: ");
                Contact.Email = clsInput.ReadString("Enter Your Email: ");
                Contact.Phone = clsInput.ReadString("Enter Your Phone Number: ");
                Contact.Address = clsInput.ReadString("Enter Your Address: ");
                Contact.DateOfBirth = new DateTime(
                    clsInput.ReadInteger("Enter The Year Of Birth: ")
                    , clsInput.ReadInteger("Enter The Month Of Birth: ")
                    , clsInput.ReadInteger("Enter The Day Of Birth: "), 0, 0, 0);
                Contact.CountryID = clsInput.ReadInteger("Enter The Country ID: ");
                Contact.ImagePath = clsInput.ReadString("Enter Image Path: ");

                if (Contact.Save())
                {

                    Console.WriteLine("Contact Updated Successfully With ID = " + Contact.ID);
                }
            }
            else
            {
                Console.WriteLine("There Is No Contact With This ID!");
            }
        }

        static void DeleteContact()
        {
            int ContactIDToDelete = clsInput.ReadInteger("Enter The Contact ID To Delete: ");

            if (clsContacts.DeleteContact(ContactIDToDelete))
            {
                Console.WriteLine("Contact Deleted Successfully :)");
            }
            else
            {
                Console.WriteLine("Contact Deletion Failed!");
            }
        }

        static void ListContacts()
        {

            DataTable dataTable = clsContacts.GetAllContacts();

            Console.WriteLine("Contacts Data:");

            foreach(DataRow row in dataTable.Rows)
            {
                Console.WriteLine($"{row["ContactID"]}, {row["FirstName"]} {row["LastName"]}, {row["Email"]}, {row["Phone"]}");
            }
        }

        static void IsContactExist()
        {

            if(clsContacts.IsContactExist(clsInput.ReadInteger("Enter Contact ID To Check If Exist: "))){
                
                Console.WriteLine("Contact Exists");
            }
            else
            {
                Console.WriteLine("Contact Is Not Found!");
            }
        }

        static clsCountries FindCountry()
        {

            clsCountries Country = clsCountries.Find(clsInput.ReadInteger("Enter Country ID: "));

            if(Country != null)
            {
                Console.WriteLine($"Country ID: {Country.CountryID}");
                Console.WriteLine($"Country Name: {Country.CountryName}");
            }
            else
            {
                Console.WriteLine("Country Is Not Found!");
            }

            return Country;
        }

        static void AddNewCountry()
        {

            clsCountries Country = new clsCountries();

            Country.CountryName = clsInput.ReadString("Enter The New Country Name: ");

            if (Country.Save())
            {

                Console.WriteLine($"The New Country Is Added Successfully With ID: {Country.CountryID}");
            }
        }

        static void UpdateCountry()
        {

            clsCountries Country = FindCountry();

            if(Country != null)
            {

                Country.CountryName = clsInput.ReadString("Enter The New Country Name: ");

                if (Country.Save())
                {
                    Console.WriteLine($"Contact Updated Successfully With Country ID: {Country.CountryID}");
                }
            }
        }

        static void Main(string[] args)
        {

            AddNewCountry();
            Console.ReadKey();
        }
    }
}
