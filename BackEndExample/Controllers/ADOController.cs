using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

// Created using Add - Controller - Web API 2 Controller with read/write actions
// WebAPI Controller that uses ADO.NET to Get, Post, Put and Delete data in the database BackEndExample

namespace BackEndExample.Controllers
{
    // We define a public class containing the structure of the data we want to return from the Get function below
    // This definition should be placed in a sperate file in the Models folder of the project, see there for more examples
    // This class should be namned ...ViewModel as a naming convention so we know that this is a class used to send information to a view/page
    public class ReturnData
    {
        public string Column1 { get; set; }
        public int Column2 { get; set; }
        public DateTime Column3 { get; set; }

    }

    public class ADOController : ApiController
    {
        // We define a private variable in out class that can be used in all our functions/methods.
        // This is the best way to do it but as a example it is only used in the Post, Put, and Delete functions
        private string conString = ConfigurationManager.ConnectionStrings["connectionStringWindowsAuthentication"].ConnectionString;

        // GET: api/ADO
        // This function returns a List of ReturnData objects. All rows in the table is returned, the query has no WHERE statement
        public IEnumerable<ReturnData> Get()
        {
            // Connectionstring defined inside function (not good!), these should be placed in the web.config file
            // Three different connection strings all doing the same thing. The spcified user needs to be defined in the database for those to work
            var connectionStringWindowsAuthentication = "Data Source=(local);Initial Catalog=BackEndExample;Integrated Security=true";
            var connectionStringSpecifyUser = "Data Source=(local);Initial Catalog=BackEndExample;User Id=sa;Password=sa;";
            var connectionStringSpecifiedServer = "Data Source=WL10901\\SQLEXPRESS;Initial Catalog=BackEndExample;User Id=sa;Password=sa;";

            // 
            var query = "SELECT Column1, Column2, Column3 FROM ExampleData1"; // Query to get all rows ExampleData1 table, and we define the columns to be returned

            SqlConnection con = new SqlConnection(connectionStringWindowsAuthentication); // Defining a connection using a specific connectionstring
            SqlCommand com = new SqlCommand(query, con); // Defining a command with a specific query(conmmand) and the connection

            con.Open(); // Open the connection
            SqlDataReader red = com.ExecuteReader(); // Get a reader for the data from the command that uses the specific connection just opened

            var ret = new List<ReturnData>(); // Define a list of ReturnData Objects to be the result from the function

            while (red.Read()) // As long as we can read data from connection with the reader we will do so. The red.Read() returns true as long as it can read data
            {
                // Lets find what position (int) each column has in the reader
                var column1 = red.GetOrdinal("Column1");
                var column2 = red.GetOrdinal("Column2");
                var column3 = red.GetOrdinal("Column3");

                // We add a ReturnData object to the list for each row in the database that we get as a result from our query
                ret.Add(new ReturnData {
                    Column1 = red[column1].ToString(), // We must make the result from the reader for Column1 into a string
                    Column2 = int.Parse(red[column2].ToString()), // We need to parse Column2 into a integer
                    Column3 = DateTime.Parse(red[column3].ToString()) // and Column3 needs to be parsed into a datetime
                });
            }

            con.Close(); // We need to close what we open

            return ret; // And return the list with the return objects
        }

        // GET: api/ADO/5
        // This function returns one ReturnData object. The row returned has the matching id sent to the function
        public ReturnData Get(int id)
        {
            // Read the connectionstring from the web.config file, the name specific must match the name attribute in the config-file
            var connectionString = ConfigurationManager.ConnectionStrings["connectionStringWindowsAuthentication"].ConnectionString;

            var query = "SELECT Column1, Column2, Column3 FROM ExampleData1 WHERE id = @id"; // Create our query with a parameter, @id

            // Defining a connection using a specific connectionstring
            // Using a using-statement so that the program close the connection by itself
            using (SqlConnection con = new SqlConnection(connectionString))
            { 
                SqlCommand com = new SqlCommand(query, con); // Defining a command with a specific query(conmmand) and the connection

                com.Parameters.AddWithValue("@id", id); // Set the parameter in the query, @id using Parameters.AddWithValue

                con.Open(); // Open the connection
                SqlDataReader red = com.ExecuteReader(); // Get a reader for the data from the command that uses the specific connection just opened

                var ret = new ReturnData(); // Create the return object

                if (red.Read()) // If we can read atleast once we have our result
                {
                    // Lets find what position (int) each column has in the reader
                    var column1 = red.GetOrdinal("Column1");
                    var column2 = red.GetOrdinal("Column2");
                    var column3 = red.GetOrdinal("Column3");

                    // Fill the return object with the data from the reader
                    ret.Column1 = red[column1].ToString(); // We must make the result from the reader for Column1 into a string
                    ret.Column2 = int.Parse(red[column2].ToString()); // We need to parse Column2 into a integer
                    ret.Column3 = DateTime.Parse(red[column3].ToString()); // and Column3 needs to be parsed into a datetime
                };

                return ret;
            }
        }

        // POST: api/ADO
        // This function takes a JSON (or XML) representation of the ReturnData object and inserts it to the database
        // Example JSON sent in the message:
        // {
        //  "Column1": "Data15",
        //  "Column2": 15,
        //  "Column3": "2001-01-01 12:00:00"
        // }
    public void Post([FromBody]ReturnData value)
        {
            // Define our INSERT sql statement with parameters that should be inserted
            var query = "INSERT INTO ExampleData1 (Column1, Column2, Column3) VALUES (@col1, @col2, @col3)";

            // Using the connection string defined as a private variable in the class
            using (SqlConnection con = new SqlConnection(conString))
            { 
                SqlCommand com = new SqlCommand(query, con);

                // Give each parameter in the query a value from in indata object
                com.Parameters.AddWithValue("@col1", value.Column1);
                com.Parameters.AddWithValue("@col2", value.Column2);
                com.Parameters.AddWithValue("@col3", value.Column3);

                con.Open();
                com.ExecuteNonQuery(); // Execute the query and we don't expect a result from it
            }
        }

        // PUT: api/ADO/5
        // This function updates a already existing row in the database using the UPDATE statement
        // The function takes both a id (sent in the url) and a object (in the message body)
        public void Put(int id, [FromBody]ReturnData value)
        {
            // Define our UPDATE sql statement with parameters that should be inserted and the id for the row to updated
            var query = "UPDATE ExampleData1 SET Column1 = @col1, Column2 = @col2, Column3 = @col3 WHERE id = @id";

            // Using the connection string defined as a private variable in the class
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand com = new SqlCommand(query, con);

                // Give each parameter in the query a value from in indata object
                com.Parameters.AddWithValue("@col1", value.Column1);
                com.Parameters.AddWithValue("@col2", value.Column2);
                com.Parameters.AddWithValue("@col3", value.Column3);
                com.Parameters.AddWithValue("@id", id);

                con.Open();
                com.ExecuteNonQuery(); // Execute the query and we don't expect a result from it
            }
        }

        // DELETE: api/ADO/5
        // This function deletes a row in the given table withe the id
        public void Delete(int id)
        {
            // Define our DELETE sql statement with a parameter for the id that is deleted
            var query = "DELETE ExampleData1 WHERE id = @id";

            // Using the connection string defined as a private variable in the class
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand com = new SqlCommand(query, con);

                com.Parameters.AddWithValue("@id", id); // Set the parameter for the row to be deleted

                con.Open();
                com.ExecuteNonQuery(); // Execute the query and we don't expect a result from it
            }
        }

        // Extra functions

        // This function returns a string of the data in the row with the given id
        // This shows how to define a function with it's own routing, call this by ex: http://localhost:53465/api/ado/getcolumnwithid/4
        [HttpGet]
        [Route("api/ado/getcolumnwithid/{id:int}")]
        
        public string Column1 (int id)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["connectionStringWindowsAuthentication"].ConnectionString;

            var query = "SELECT Column1 FROM ExampleData1 WHERE id = @id"; // Only get the data that you need

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand com = new SqlCommand(query, con);

                com.Parameters.AddWithValue("@id", id);

                con.Open();
                SqlDataReader red = com.ExecuteReader();

                if (red.Read())
                {
                    // Lets find what position (int) each column has in the reader
                    return (red[red.GetOrdinal("Column1")].ToString();
                } else
                {
                    // Error
                    return string.Empty; // Return empty string if the id was not found
                }
            }
        }
    }
}
