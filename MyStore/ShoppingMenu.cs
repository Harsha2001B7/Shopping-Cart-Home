using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore
{
    class ShoppingMenu
    {
        private SqlConnection conn;
        public ShoppingMenu(SqlConnection connection)
        {
            conn = connection;
        }
        public void Menu()
        {
            Console.WriteLine("\nWhat would you like to do?");
            Console.WriteLine("1. Add to Cart");
            Console.WriteLine("2. View Cart");
            Console.WriteLine("3. Place Order");
            Console.WriteLine("4. Exit");
            Console.Write($"Press any of (1, 2, 3, 4)..!  \n");
            int i = Convert.ToInt32(Console.ReadLine());
            switch (i)
            {
                case 1: AddToCart();
                    break;
                case 2: ViewCart();
                    break;
                case 3: PlaceOrder();
                    break;
                case 4: Console.Write("Thank you for Visiting MyStore.com..!");
                    break;
                default: Console.WriteLine("Please Choose a Valid Option!!");
                    break;
            }
        }
        public void ViewCart()
        {
            SqlCommand cmd = new SqlCommand("[dbo].[ViewCart]", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@Username", Session.Username);
                SqlDataReader reader = cmd.ExecuteReader();
                Utility.DisplayTable(reader);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                conn.Close();
            }
        }
        public void AddToCart()
        {
            Console.Write("Enter ProductID: \n");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter Quantity: \n");
            int q = Convert.ToInt32(Console.ReadLine());
            SqlCommand cmd = new SqlCommand("[dbo].[AddToCart]", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ProductId", id);
            cmd.Parameters.AddWithValue("@Username", Session.Username);
            cmd.Parameters.AddWithValue("@Quantity", q);
            try
            {
                conn.Open();
                string resultMessage = (string)cmd.ExecuteScalar();
                Console.WriteLine(resultMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                conn.Close();
            }
        }
        public void PlaceOrder()
        {
            SqlCommand cmd = new SqlCommand("[dbo].[PlaceOrder]", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Username", Session.Username);
            try
            {
                conn.Open();
                string resultMessage = (string)cmd.ExecuteScalar();
                Console.WriteLine(resultMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
