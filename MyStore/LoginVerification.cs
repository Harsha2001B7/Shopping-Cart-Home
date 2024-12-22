using System;
using System.Data.SqlClient;

namespace MyStore
{
    class LoginVerification
    {
        private SqlConnection conn;
        private UserManagement userManagement;
        public LoginVerification(SqlConnection connection)
        {
            conn = connection;
            userManagement = new UserManagement(conn);
        }
        public bool VerifyUser()
        {
            Console.WriteLine("Welcome To MyStore.com");
            Console.WriteLine("\nDo you have login credentials? (Yes/No)");
            string input = Console.ReadLine().ToLower();

            if (input == "yes")
            {
                return userManagement.UserValidate();
            }
            else if (input == "no")
            {
                return userManagement.RegisterUser();
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter 'Yes' or 'No'.");
                return false;
            }
        }
    }
}
