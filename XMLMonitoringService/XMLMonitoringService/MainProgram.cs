using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace XMLMonitoringService
{
    class MainProgram
    {
        public static void CheckStuff()
        {
            SqlConnection connection = SqlServer.GetConnection();
            SqlCommand cmd = connection.CreateCommand();

            cmd.CommandText = "Insert into Job(JobId,JobTitle,Employer,Description) values('101','Developer', 'Cherry','Hey its one hello of a thing')";
            cmd.ExecuteNonQuery();
            Console.WriteLine("Query executed successfully...");

        }

        public static void CheckString()
        {
            Dictionary<string, string> EmployeeList = new Dictionary<string, string>();

            EmployeeList.Add("Mahesh Chand", "Programmer");
            EmployeeList.Add("Praveen Kumar", "Project Manager");
            EmployeeList.Add("Raj Kumar", "Architect");
            EmployeeList.Add("Nipun Tomar", "Asst. Project Manager");
            EmployeeList.Add("Dinesh Beniwal", "Manager");

            Console.WriteLine(EmployeeList["Mahesh Chand"]);

        }

        public static void Main()
        {
            Console.WriteLine("XMLMonitoringService is Started...!");

            Config.InitializeConfig();

            Scheduler.Start();

            //CheckString();
            
            Console.ReadLine();
        }
        
    }
}