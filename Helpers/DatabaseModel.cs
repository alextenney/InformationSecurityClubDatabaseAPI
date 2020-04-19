  
/* 
Contributers: Alexandra Tenney, Martha Ibarra, Jeremy Stuart
 Date Updated: April 17, 2020
 Description: These are the helpers, which are called by different controller methods and call the stored procedures which fetch the 
 correct data from the database.
*/

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Web;



namespace ProjectTemp.Helpers
{
    public class DatabaseModel
    {

        #region Query Methods
        //the following methods are taken from the template uploaded to d2l, and not programmed by our team
        public SqlConnection GetSQLConnection(string connectionstring)
        {
            if (connectionstring == null)
                return null;
            return new SqlConnection(connectionstring);
        }

        public string Get_PuBConnectionString()
        {
            try
            {
                return "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            }
            catch { return null; }
        }

        public SqlConnection GetSQLConnection()
        {
            if (Get_PuBConnectionString() == null)
                return null;
            return new SqlConnection(Get_PuBConnectionString());
        }

        /// <summary>
        /// This method is responisble to to execute a query in your RDBMS and return for you an output value. 
        /// For instance, in some cases when you insert a new records you need to return the id of that record to do other actions
        /// </summary>
        /// <returns></returns>

        public int Execute_Non_Query_Store_Procedure(string procedureName, SqlParameter[] parameters, string returnValue)
        {
            // if there's no connection, return -2...probably the value of a failed query
            if (GetSQLConnection() == null)
                return -2;

            // default the query to unseccesful 
            int successfulQuery = -2;
            // new SQL command, takes procedure name and the connection string
            SqlCommand sqlCommand = new SqlCommand(procedureName, GetSQLConnection());
            // the command type is the stored procedure
            sqlCommand.CommandType = CommandType.StoredProcedure;

            // try catch
            try
            {   // adds "elements" to the end of the "SQL Parameter Collection" (not 100% what is exactly)
                sqlCommand.Parameters.AddRange(parameters);
                // open connection with the property settings in the ConnectionString 
                sqlCommand.Connection.Open();
                // Returns number of rows affected!
                successfulQuery = sqlCommand.ExecuteNonQuery();
                // Immediately sets it to something else?  AMERICAN EXPLAIN!
                successfulQuery = (int)sqlCommand.Parameters["@" + returnValue].Value;  // WHHHHHHHHHHHHHHHHHHHHYYYYYYYYYYYYYYYYYYYYYYY!?!!?!?

            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }

            if (sqlCommand.Connection != null && sqlCommand.Connection.State == ConnectionState.Open)
                sqlCommand.Connection.Close();

            // This is returning a value from the stored procedure...not sure if it's just that != -2 means it was
            // successful or if it's returning the number of values queried/affected!
            return successfulQuery;
        }


        /// <summary>
        /// This method is responisble to to execute a query in your RDBMS and return for you if it was successult executed. Minay used for insert,update, and delete
        /// </summary>
        /// <returns></returns>
        public int Execute_Non_Query_Store_Procedure(string procedureName, SqlParameter[] parameters)
        {
            // Not clear why these return values are different...debugging?
            if (GetSQLConnection() == null)
                return -1;

            // Why default to 1 now?
            int successfulQuery = 1;
            SqlCommand sqlCommand = new SqlCommand(procedureName, GetSQLConnection());
            sqlCommand.CommandType = CommandType.StoredProcedure;

            try
            {
                sqlCommand.Parameters.AddRange(parameters);
                sqlCommand.Connection.Open();
                // Returns number of rows affected!
                successfulQuery = sqlCommand.ExecuteNonQuery();
                // successfulQuery =1 (his comment, not mine...don't know why that's here as it's the default value!)

            }
            catch (Exception ex)
            {
                string s = ex.Message;
                successfulQuery = -2;
            }

            if (sqlCommand.Connection != null && sqlCommand.Connection.State == ConnectionState.Open)
                sqlCommand.Connection.Close();

            return successfulQuery;
        }


        /// <summary>
        /// This method is responisble to to execute to rertieve data from your RDBSM by executing a stored procedure. Mainly used when using one select statment
        /// </summary>
        /// <returns></returns>
        public DataTable Execute_Data_Query_Store_Procedure(string procedureName, SqlParameter[] parameters)
        {
            if (GetSQLConnection() == null)
                return null;

            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlAdapter = new SqlDataAdapter(procedureName, GetSQLConnection());
            sqlAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            try
            {
                // fills the dataTable with the table from the stored procedure
                sqlAdapter.SelectCommand.Parameters.AddRange(parameters);
                sqlAdapter.SelectCommand.Connection.Open();
                sqlAdapter.Fill(dataTable);
            }
            catch (Exception er)
            {
                string ee = er.ToString();
                dataTable = null;
            }
            // close the connection if things worked
            if (sqlAdapter.SelectCommand.Connection != null && sqlAdapter.SelectCommand.Connection.State == ConnectionState.Open)
                sqlAdapter.SelectCommand.Connection.Close();

            // return the data table
            return dataTable;
        }

        /// <summary>
        /// This method is responisble to to execute to rertieve data from your RDBSM by executing a stored procedure. Mainly used when more than one table is being returned.
        /// </summary>
        /// <returns></returns>
        /// 

        public DataSet Execute_Data_Dataset_Store_Procedure(string procedureName, SqlParameter[] parameters)
        {
            if (GetSQLConnection() == null)
                return null;

            DataSet dataset = new DataSet();
            SqlDataAdapter sqlAdapter = new SqlDataAdapter(procedureName, GetSQLConnection());
            sqlAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            try
            {
                sqlAdapter.SelectCommand.Parameters.AddRange(parameters);
                sqlAdapter.SelectCommand.Connection.Open();
                sqlAdapter.Fill(dataset);
            }
            catch (Exception er)
            {
                string ee = er.ToString();
                dataset = null;
            }

            if (sqlAdapter.SelectCommand.Connection != null && sqlAdapter.SelectCommand.Connection.State == ConnectionState.Open)
                sqlAdapter.SelectCommand.Connection.Close();

            return dataset;
        }

        /// <summary>
        /// This method check if the connection string is valid or not
        /// </summary>
        /// <returns></returns>

        public bool CheckDatabaseConnectionString(string ConnectionString)
        {
            try
            {

                SqlConnection con = new SqlConnection(ConnectionString);
                con.Open();
                con.Close();
                return true;
            }
            catch (Exception er)
            {
                return false;
            }


        }
        #endregion
    // the following methods are added by our team, not taken from the templates
        public int adminAddsTeamMember(string participantName, string teamName)
        {
            // Specifc number of parametrs for this stored procedure
            SqlParameter[] Parameters = new SqlParameter[2];
            // Load the parameters into the list
            Parameters[0] = new SqlParameter("@participantName", participantName);
            Parameters[1] = new SqlParameter("@teamName", teamName);
    //executes the stored procedure adminAddsTeam
            return Execute_Non_Query_Store_Procedure("adminAddsTeamMember", Parameters);
        }

        public int adminCreatesTeam(string teamName)
        {
            // Specifc number of parametrs for this stored procedure
            SqlParameter[] Parameters = new SqlParameter[1];
            // Load the parameters into the list
            Parameters[0] = new SqlParameter("@teamName", teamName);
            //executes the stored procedure adminCreatesTeam 
            return Execute_Non_Query_Store_Procedure("adminCreatesTeam", Parameters);
        }


        public DataTable GetChallengeInfo()
        {
            // Specifc number of parametrs for this stored procedure
            SqlParameter[] Parameters = new SqlParameter[0];

            //executes the stored procedure adminSelectsChallenges
            return Execute_Data_Query_Store_Procedure("adminSelectsChallenges", Parameters);

        }

        public DataTable adminSelectsParticipant(string pName)
        {
            
            SqlParameter[] Parameters = new SqlParameter[1];
            // Load the parameters into the list
            Parameters[0] = new SqlParameter("@pName", pName);
             //executes the stored procedure adminSelectParticipant
            return Execute_Data_Query_Store_Procedure("adminSelectParticipant", Parameters);

        }

        public DataTable GetTeam(string teamName)
        {
            SqlParameter[] Parameters = new SqlParameter[1];
            // Load the parameters into the list
            Parameters[0] = new SqlParameter("@teamName", teamName);
            //executes the stored procedure selectTeam 
            return Execute_Data_Query_Store_Procedure("selectTeam", Parameters);
        }

        public DataTable CTFTeam(string teamName)
        {
            SqlParameter[] Parameters = new SqlParameter[1];
            // Load the parameters into the list
            Parameters[0] = new SqlParameter("@teamName", teamName);
            //executes the stored procedure selectCTFSTeamParticipatedIn 
            return Execute_Data_Query_Store_Procedure("selectCTFSTeamParticipatedIn", Parameters);
        }

        public int participantAddsAttendance(string participantName, string meeetingDate)
        {
            // Specifc number of parametrs for this stored procedure
            SqlParameter[] Parameters = new SqlParameter[2];
            // Load the parameters into the list
            Parameters[0] = new SqlParameter("@participantName", participantName);
            Parameters[1] = new SqlParameter("@meetingDate", meeetingDate);
            //executes the stored procedure participantAddsAttendance 
            return Execute_Non_Query_Store_Procedure("participantAddsAttendance", Parameters);
        }

        public int participantAddsThemselvesToTeam(string participantName, string teamName)
        {
            // Specifc number of parametrs for this stored procedure
            SqlParameter[] Parameters = new SqlParameter[2];
            // Load the parameters into the list
            Parameters[0] = new SqlParameter("@participantName", participantName);
            Parameters[1] = new SqlParameter("@teamName", teamName);
            //executes the stored procedure participantAddsThemselvesToTeam
            return Execute_Non_Query_Store_Procedure("participantAddsThemselvesToTeam", Parameters);
        }


        public DataTable SelectCTF(string ctfName)
        {
            // Specifc number of parametrs for this stored procedure
            SqlParameter[] Parameters = new SqlParameter[1];
            // Load the parameters into the list
            Parameters[0] = new SqlParameter("@ctfName", ctfName);
            //executes the stored procedure selectCTF 
            return Execute_Data_Query_Store_Procedure("selectCTF", Parameters);
        }

        public DataTable selectNotInTeam()
        {
            // Specifc number of parametrs for this stored procedure
            SqlParameter[] Parameters = new SqlParameter[0];
            //executes the stored procedure selectNotInTeam 
            return Execute_Data_Query_Store_Procedure("selectNotInTeam", Parameters);

            
        }

        public DataTable selectWhoFoundFlag(string flag)
        {
            // Specifc number of parametrs for this stored procedure
            SqlParameter[] Parameters = new SqlParameter[1];
            // Load the parameters into the list
            Parameters[0] = new SqlParameter("@flag", flag);
            //executes the stored procedure selectWhoCapturedFlag 
            return Execute_Data_Query_Store_Procedure("selectWhoCapturedFlag", Parameters);

        }
        #endregion
    }

}
