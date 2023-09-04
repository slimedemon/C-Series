using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Need these to get definitions of common interfaces,
// and various connection objects fro our test.
using System.Data;
using System.Data.SqlClient;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Configuration;
using System.Data.Common;

namespace AbstractingDataProviders
{
    // A list of possible providers.
    enum DataProvider
    { 
        SqlServer, OleDb, Odbc, None
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Very Simple Connection Factory *****\n");

            //// Read the provider key.
            //String dataProviderString = ConfigurationManager.AppSettings["provider"];
            //// Transform string to enum.
            //DataProvider dataProvider = DataProvider.None;
            //if (Enum.IsDefined(typeof(DataProvider), dataProviderString))
            //{
            //    dataProvider = (DataProvider)Enum.Parse(typeof(DataProvider), dataProviderString);
            //}
            //else
            //{
            //    Console.WriteLine("Sorry, no provider exists");
            //    Console.ReadLine();
            //    return;
            //}
            //// Get a specific connection.
            //IDbConnection myConnection = GetConnection(dataProvider);
            //Console.WriteLine($"Your connection is a {myConnection.GetType().Name}");
            //// Open, use and close connection...
            //Console.ReadLine();

            // Get Connection string/ provider from *.config.
            string dataProvider = ConfigurationManager.AppSettings["provider"];
            string connectionString = ConfigurationManager.ConnectionStrings["AutoLotSqlProvider"].ConnectionString;

            // Get the factory provider.
            DbProviderFactory factory = DbProviderFactories.GetFactory(dataProvider);

            // Now get the connection object.
            using (DbConnection connection = factory.CreateConnection())
            {
                if (connection == null)
                {
                    showError("Connection");
                    return;
                }

                Console.WriteLine($"Your connection object is a: {connection.GetType().Name}");
                connection.ConnectionString = connectionString;
                connection.Open();

                // Make command object.
                DbCommand command = factory.CreateCommand();
                if (command == null)
                {
                    showError("Command");
                    return;
                }
                Console.WriteLine($"Your command object is a: {command.GetType().Name}");
                command.Connection = connection;
                command.CommandText = "Select * From Inventory";

                // Print out data with data reader.
                using (DbDataReader dataReader = command.ExecuteReader())
                {
                    Console.WriteLine($"Your data reader object is a: {dataReader.GetType().Name}");
                    Console.WriteLine("\n***** Current Inventory *****");
                    while (dataReader.Read())
                        Console.WriteLine($"-> Car #{dataReader["carId"]} is a {dataReader["Make"]}");
                }

                Console.ReadLine();
            }
        }

        // This method returns a specific connection object
        // based on the value of a DataProvider enum.
        private static IDbConnection GetConnection(DataProvider dataProvider)
        { 
            IDbConnection connection = null;
            switch(dataProvider)
            {
                case DataProvider.SqlServer: connection = new SqlConnection(); break;
                case DataProvider.OleDb: connection = new OleDbConnection(); break;
                case DataProvider.Odbc: connection = new OdbcConnection(); break;
                default: connection = null; break;
            }
            return connection;
        }

        private static void showError(string objectName)
        {
            Console.WriteLine($"There is an issue creating the {objectName}");
            Console.ReadLine();
        }
    }
}
