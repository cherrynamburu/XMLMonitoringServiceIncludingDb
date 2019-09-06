using System;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace XMLMonitoringService
{
    class SqlServer
    {
        internal static SqlConnection GetConnection()
        {
            SqlConnection sqlConnection = new SqlConnection(Config.sqlServerConnectionString);
            try
            {
                sqlConnection.Open();
                return sqlConnection;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            return null;
        }

        internal static void InsertToEmployementHistory(String from, String to)
        {
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "Insert into EmployementHistory values('" + from + "','" + to + "')";
                cmd.ExecuteNonQuery();
                Console.WriteLine("inserting into Employement history is completed..!");
            }               
        }

        internal static void InsertJob(Dictionary<string, string> jobElements)
        {
            int id = 0;
            InsertToEmployementHistory(jobElements["from"], jobElements["to"]);
            SqlConnection connection = GetConnection();
            SqlCommand cmd = connection.CreateCommand();

            cmd.CommandText = "SELECT MAX(EmployementHistoryId) from EmployementHistory";

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                id = dr.GetInt32(0);
                
            }
            dr.Close();

            cmd.CommandText = "Insert into Job values('" + jobElements["jobid"] + "','" + jobElements["jobtitle"] + "','" + jobElements["employer"] + "','" + jobElements["description"] + "','" + id + "')";
            cmd.ExecuteNonQuery();
            Console.WriteLine("Inserting into Job is Completed");
        }

        internal static int CheckJobExists(int jobid)
        {
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "select * from Job where jobid = '" + jobid + "' ";
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        Console.WriteLine("     ---     exists!");
                        //Console.WriteLine(dr["EmpHistoryId"]);
                        return Convert.ToInt32(dr["EmpHistoryId"]);
                        
                    }
                    else
                    {
                        Console.WriteLine("    ---    Doesn't exist!");
                        return 0;
                    }
                    
                }
            }
        }

        internal static void UpdateJob(Dictionary<string, string> jobElements, int empHistoryId)
        {
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE Job SET jobtitle = '" + jobElements["jobtitle"] + "',employer = '" + jobElements["employer"] + "',description = '" + jobElements["description"] + "' WHERE jobid = '" + jobElements["jobid"] + "'";
                command.ExecuteNonQuery();
                Console.WriteLine("Updating job completed..!");
                UpadteEmployementHistory(jobElements["from"], jobElements["to"], empHistoryId);
            }
                
        }

        internal static void UpadteEmployementHistory(string from, string to ,int empHistoryId)
        {
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE EmployementHistory SET EmployementStart = '" + from + "', EmployementEnd = '" + to + "' WHERE EmployementHistoryId = '" + empHistoryId + "' ";
                command.ExecuteNonQuery();
                Console.WriteLine("Updating Employement history is completed..!");
            }
        }
    }
}


