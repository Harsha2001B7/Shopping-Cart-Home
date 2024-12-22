using System;
using System.Data;
using System.Data.SqlClient;

namespace MyStore
{
    class UserManagement
    {
        private SqlConnection conn;
        public UserManagement(SqlConnection connection)
        {
            conn = connection;
        }
        public bool UserValidate()
        {
            Console.Write("Enter Username: ");
            string username = Console.ReadLine();
            Console.Write("Enter Password: ");
            string password = Console.ReadLine();

            SqlCommand cmd = new SqlCommand("[dbo].[UserLogin]", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Username", username);
            cmd.Parameters.AddWithValue("@Password", password);

            try
            {
                conn.Open();
                int result = (int)cmd.ExecuteScalar(); 
                if (result == 1)
                {
                    Console.WriteLine("Login successful.\n");
                    Session.Username = username;
                    return true;
                }
                else
                {
                    Console.WriteLine("Invalid username or password.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
            finally
            {
                conn.Close();
            }
        }
        public bool RegisterUser()
        {
            Console.Write("Enter Full Name: ");
            string fullName = Console.ReadLine();
            Console.Write("Enter Username: ");
            string username = Console.ReadLine();
            Console.Write("Enter Password: ");
            string password = Console.ReadLine();
            Console.Write("Enter Mobile Number: ");
            string mobileNumber = Console.ReadLine();

            SqlCommand cmd = new SqlCommand("[dbo].[RegisterUser]", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@FullName", fullName);
            cmd.Parameters.AddWithValue("@Username", username);
            cmd.Parameters.AddWithValue("@Password", password);
            cmd.Parameters.AddWithValue("@MobileNumber", mobileNumber);

            try
            {
                conn.Open();                
                int result = (int)cmd.ExecuteScalar();
                if (result == 1)
                {
                    Console.WriteLine("User registered successfully!");
                    Session.Username = username;
                    return true;
                }
                else
                {
                    Console.WriteLine("Error: The username already exists. Please choose a different username.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
