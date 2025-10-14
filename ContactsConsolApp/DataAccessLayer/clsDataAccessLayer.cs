using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace DataAccessLayer
{
    public static class clsDataAccessLayer
    {

        public static bool FindContactById(int ID, ref string FirstName, ref string LastName, ref string Email,
            ref string Phone, ref string Address, ref DateTime DateOfBirth, ref int CountryID, ref string ImagePath)
        {
            bool IsFound = false;

            string query = "SELECT * FROM Contacts WHERE ContactID = @ID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString2))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();

                        command.Parameters.Add("@ID", SqlDbType.Int).Value = ID;

                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            IsFound = true;
                            FirstName = (string)reader["FirstName"];
                            LastName = (string)reader["LastName"];
                            Email = (string)reader["Email"];
                            Phone = (string)reader["Phone"];
                            Address = (string)reader["Address"];
                            DateOfBirth = (DateTime)reader["DateOfBirth"];
                            CountryID = (int)reader["CountryID"];

                            if (reader["ImagePath"] != DBNull.Value)
                            {

                                ImagePath = (string)reader["ImagePath"];
                            }
                            else
                            {
                                ImagePath = "";
                            }
                        }
                        else
                        {

                            IsFound = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("An error occurred: " + ex.Message);

                        IsFound = false;
                    }
                }
            }

            return IsFound;
        }

        public static int AddNewContact(string FirstName, string LastName, string Email, string Phone, string Address, DateTime DateOfBirth, int CountryID, string ImagePath)
        {

            int ContactID = -1;

            string query = @"INSERT INTO Contacts (FirstName, LastName, Email, Phone, Address, DateOfBirth, CountryID, ImagePath)
                            VALUES(@FirstName, @LastName, @Email, @Phone, @Address, @DateOfBirth, @CountryID, @ImagePath);
                            SELECT SCOPE_IDENTITY();";

            using (SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    command.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = FirstName;
                    command.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = LastName;
                    command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = Email;
                    command.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = Phone;
                    command.Parameters.Add("@Address", SqlDbType.NVarChar).Value = Address;
                    command.Parameters.Add("@DateOfBirth", SqlDbType.DateTime).Value = DateOfBirth;
                    command.Parameters.Add("@CountryID", SqlDbType.Int).Value = CountryID;

                    if (!string.IsNullOrWhiteSpace(ImagePath))
                    {
                        command.Parameters.Add("@ImagePath", SqlDbType.NVarChar).Value = ImagePath;
                    }
                    else
                    {
                        command.Parameters.Add("@ImagePath", SqlDbType.NVarChar).Value = DBNull.Value;
                    }

                    try
                    {
                        connection.Open();
                        object ID = command.ExecuteScalar();

                        if (ID != null && int.TryParse(ID.ToString(), out int InsertedID))
                        {

                            ContactID = InsertedID;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }

            return ContactID;
        }

        public static bool UpdateContact(int ContactID, string FirstName, string LastName, string Email, string Phone, string Address, DateTime DateOfBirth, int CountryID, string ImagePath)
        {

            int RowsAffected = 0;

            string query = $@"UPDATE Contacts 
                            SET FirstName = @FirstName,
                                LastName = @LastName,
                                Email = @Email,
                                Phone = @Phone,
                                Address = @Address,
                                DateOfBirth = @DateOfBirth,
                                CountryID = @CountryID,
                                ImagePath = @ImagePath
                            WHERE ContactID = {ContactID};";

            using (SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString))
            {
                using(SqlCommand command = new SqlCommand(query, connection))
                {

                    command.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = FirstName;
                    command.Parameters.Add("@LastName", SqlDbType.NVarChar).Value=LastName;
                    command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = Email;
                    command.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = Phone;
                    command.Parameters.Add("@Address", SqlDbType.NVarChar).Value = Address;
                    command.Parameters.Add("@DateOfBirth", SqlDbType.NVarChar).Value = DateOfBirth;
                    command.Parameters.Add("@CountryID", SqlDbType.Int).Value = CountryID;
                    
                    if(!string.IsNullOrWhiteSpace(ImagePath))
                    {
                        command.Parameters.Add("@ImagePath", SqlDbType.NVarChar).Value = ImagePath;
                    }
                    else
                    {
                        command.Parameters.Add("@ImagePath", SqlDbType.NVarChar).Value = DBNull.Value;
                    }

                    try
                    {
                        connection.Open();
                        RowsAffected = command.ExecuteNonQuery();
                    }catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }

            return (RowsAffected > 0);
        }
    
        public static bool DeleteContact(int ID)
        {

            bool Deleted = false;

            string query = "DELETE Contacts WHERE ContactID = @ContactID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString))
            {
                using(SqlCommand command = new SqlCommand(query, connection))
                {

                    command.Parameters.Add("@ContactID", SqlDbType.Int).Value = ID;


                    try
                    {
                        connection.Open();

                        int result = command.ExecuteNonQuery();

                        if (result > 0)
                        {
                            Deleted = true;
                        }
                    }catch(Exception ex)
                    {
                        Deleted = false;
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }

            return Deleted;
        }

        public static DataTable GetAllContacts()
        {

            DataTable DT = new DataTable();

            string query = "SELECT * FROM Contacts";

            using(SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    try
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {

                                DT.Load(reader);
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }

            return DT;
        }

        public static bool IsContactExist(int ID)
        {

            bool IsFound = false;

            string query = "SELECT Found = 1 FROM Contacts WHERE ContactID = @ContactID";

            using(SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString))
            {
                using(SqlCommand command = new SqlCommand(query, connection))
                {

                    command.Parameters.Add("@ContactID", SqlDbType.Int).Value = ID;

                    try
                    {

                        connection.Open();

                        using(SqlDataReader reader = command.ExecuteReader())
                        {

                            IsFound = reader.HasRows;
                        }
                    }catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        IsFound = false;
                    }
                }
            }

            return IsFound;
        }

        public static bool FindCountryByID(int CountryID, ref string CountryName)
        {
            bool IsFound = false;

            string query = "SELECT * FROM Countries WHERE CountryID = @CountryID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    command.Parameters.Add("@CountryID", SqlDbType.Int).Value = CountryID;

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            if (reader.Read())
                            {
                                IsFound = true;
                                CountryName = (string)reader["CountryName"];
                            }
                            else
                            {

                                IsFound = false;
                            }
                        }
                    }catch(Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }

            return IsFound;
        }

        public static int AddNewCountry(string CountryName)
        {

            int CountryID = -1;

            string query = @"INSERT INTO Countries (CountryName) VALUES (@CountryName);
                            SELECT SCOPE_IDENTITY();";

            using(SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    command.Parameters.Add("@CountryName", SqlDbType.NVarChar, 50).Value = CountryName;

                    try
                    {
                        connection.Open();
                        object ID = command.ExecuteScalar();

                        if (ID != null && int.TryParse(ID.ToString(), out int InsertedID))
                        {

                            CountryID = InsertedID;
                        }
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }

            return CountryID;
        }

        public static bool UpdateCountry(int CountryID, string CountryName)
        {

            int RowsAffected = 0;

            string query = @"UPDATE Countries 
                            SET CountryName = @CountryName
                            WHERE CountryID = @CountryID;";

            using(SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString))
            {
                using(SqlCommand command = new SqlCommand(query, connection))
                {

                    command.Parameters.Add("@CountryName", SqlDbType.NVarChar, 50).Value = CountryName;
                    command.Parameters.Add("@CountryID", SqlDbType.Int).Value = CountryID;

                    try
                    {

                        connection.Open();
                        RowsAffected = command.ExecuteNonQuery();

                    }catch(Exception ex)
                    {

                        Console.WriteLine("Error: "+ ex.Message);
                    }                    
                }
            }

            return (RowsAffected > 0);
        }

        public static bool DeleteCountry(int CountryID)
        {

            int RowsAffected = 0;

            bool Deleted = false;

            string query = "DELETE FROM Countries WHERE CountryID = @CountryID;";

            using(SqlConnection connection = new SqlConnection(clsDataAccessLayerSettings.connectionString))
            {
                using(SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add(@"CountryID", SqlDbType.Int).Value = CountryID;

                    try
                    {
                        connection.Open();
                        RowsAffected = command.ExecuteNonQuery();
                        if (RowsAffected > 0)
                        {
                            Deleted = true;
                        }
                        else
                        {
                            Deleted = false;
                        }

                    }catch(Exception ex)
                    {
                        Deleted = false;
                        Console.WriteLine("Error" + ex.Message);
                    }
                }
            }

            return Deleted;
        }
    }
}
