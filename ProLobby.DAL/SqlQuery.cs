using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoteIt.DAL
{
    public class SqlQuery
    {
        public delegate void SetDataReader_delegate(SqlDataReader reader);
        public delegate Dictionary<int, object> SetResulrDataReader_delegate(SqlDataReader reader);
        public delegate int SetResulrDataReader_delegateInt(SqlDataReader reader);
        private static string connectionString = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=PromoteIt;Data Source=MSI\\SQLEXPRESS";
        public static void RunCommand(string sqlQuery, SetDataReader_delegate func)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = sqlQuery;

                // Adapter
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    connection.Open();
                    //Reader
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        func(reader);
                    }
                }
            }

        }
        public static string RunCommandCheck(string sqlQuery, string Email)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string answer = "";
                    string queryString = sqlQuery;


                    // Adapter

                    connection.Open();

                    using (SqlCommand command = new SqlCommand(queryString, connection))
                    {
                        try
                        {
                            command.Parameters.AddWithValue("@email", Email);
                            answer = command.ExecuteScalar().ToString();
                            command.ExecuteNonQuery();
                        }catch(Exception ex)
                        {

                        }
                    }
                    return answer;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static Dictionary<int, object> RunCommandResult(string sqlQuery, SetResulrDataReader_delegate func)
        {
            Dictionary<int, object> ret = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    string queryString = sqlQuery;

                    // Adapter
                    using (SqlCommand command = new SqlCommand(queryString, connection))
                    {
                        connection.Open();
                        //Reader
                        try
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                ret = func(reader);
                            }
                        }catch(Exception ex)
                        {

                        }
                        
                    }

                }
            }catch(Exception ex)
            {

            }
            return ret;
        }
        public static Dictionary<int, object> RunCommandResultTweet(string sqlQuery, SetResulrDataReader_delegate func)
        {
            Dictionary<int, object> ret = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    string queryString = sqlQuery;

                    // Adapter
                    using (SqlCommand command = new SqlCommand(queryString, connection))
                    {
                        connection.Open();
                        //Reader
                        try
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                ret = func(reader);
                            }
                        }
                        catch (Exception ex)
                        {

                        }

                    }

                }
            }
            catch (Exception ex)
            {

            }
            return ret;
        }
        public static int RunCommandResultInt(string sqlQuery, SetResulrDataReader_delegateInt func)
        {
            int ret = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    string queryString = sqlQuery;

                    // Adapter
                    using (SqlCommand command = new SqlCommand(queryString, connection))
                    {
                        connection.Open();
                        //Reader
                        try
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                ret = func(reader);
                            }
                        }
                        catch (Exception ex)
                        {

                        }

                    }

                }
            }
            catch (Exception ex)
            {

            }
            return ret;
        }
        public static void RunNonQuery(string sqlQuery)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    string queryString = sqlQuery;

                    // Adapter
                    using (SqlCommand command = new SqlCommand(queryString, connection))
                    {
                        connection.Open();
                        try
                        {
                            command.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {

                        }

                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
