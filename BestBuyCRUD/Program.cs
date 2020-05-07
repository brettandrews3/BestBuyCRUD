using System;
using System.Collections;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace BestBuyCRUD
{
    class Program
    {
        static void Main(string[] args)
        {
            ListProducts();

            DeleteProduct();
        }

        public static void DeleteProduct()
        {
            ListProducts();

            var prodRepo = new ProductRepository(conn);
            Console.WriteLine($"Please enter the Product ID of the product you would like to delete:");
            var productID = Convert.ToInt32(Console.ReadLine());

            prodRepo.DeleteProduct(productID);

            ListProducts();
        }

        {
            var departments = GetAllDepartments();

            foreach (var dept in departments)
            {
                Console.WriteLine(dept);
            }
        }

        static IEnumerable GetAllDepartments()
        {
            //initialize instance of MySqlConnection class
            MySqlConnection conn = new MySqlConnection(); 
            conn.ConnectionString = System.IO.File.ReadAllText("ConnectionString.txt");
            /*Set our ConnectionString to connect to MySQL by reading the .txt file
            * This creates, returns a MySqlComm object assoc. w/ the connection
            * Now, we can give and execute a query against the MySql database */

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT Name FROM Departments;";
            /* Here ^, we give the command for the SQL query we want to execute:
             * select allDepartments from the Departments table */

            using (conn) //using statement begins, utilizes our MySqlConnection
            {
                conn.Open(); //open the connection
                List<string> allDepartments = new List<string>();
                //creates a list to hold our depts from the depts table

                MySqlDataReader reader = cmd.ExecuteReader();
                /* Create a MySqlDataReader to allow us to read the forward-
                 * only stream of rows from the MySql database, reading 1 row
                 * at a time from the query results. */
                
                while (reader.Read() == true) //loop advances while there's more lines to read
                {
                    var currentDepartment = reader.GetString("Name");
                    allDepartments.Add(currentDepartment); /* Adds the value from
                    the Name column of the Depts table, adding to our allDepts list */
                }
                //Connection to MySql auto-closes at end of the using statement scope
                return allDepartments;
            }
        }
    }
}
