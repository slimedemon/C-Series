using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace ConnectedLayer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            WriteLine("***** Fun with Data Readers *****\n");

            // Create and open a connection.
            using (SqlConnection connection = new SqlConnection())
            {
                // Configurate connection
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["AutoLotSqlProvider"].ConnectionString;
                connection.Open();

                // Configurate command
                string sql = "Select * from Inventory";
                SqlCommand command = new SqlCommand(sql, connection);

                // Obtain a data reader 
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    // Loop over the results.
                    while(dataReader.Read())
                    {
                        WriteLine($"-> Make: {dataReader["Make"]}, PetName: {dataReader["PetName"]}, Color: {dataReader["Color"]}.");
                    }
                }

            }
        }
    }
}
