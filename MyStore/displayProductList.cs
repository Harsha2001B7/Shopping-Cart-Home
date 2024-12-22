using System;
using System.Data;
using System.Data.SqlClient;

namespace MyStore
{
    class DisplayProductList
    {
        private SqlConnection conn;

        public DisplayProductList(SqlConnection connection)
        {
            conn = connection;
        }
        public void ShowProductList()
        {
            SqlCommand cmd = new SqlCommand("[dbo].[GetProductList]", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                Console.WriteLine("Product List: ");
                Utility.DisplayTable(reader);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
