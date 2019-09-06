using System;
using System.Collections.Generic;
using System.Configuration;

namespace XMLMonitoringService
{
    internal class Config
    {
        private const int INTERVAL_DEFAULT = 1000;

        internal static string InputSource;

        internal static string OutputSource;

        internal static int TimeInterval;
                
        internal static List<string> RequiredTags;

        internal static String sqlServerConnectionString;

        internal static void InitializeConfig()
        {
            InputSource = ConfigurationManager.AppSettings.Get("inputSource");
            OutputSource = ConfigurationManager.AppSettings.Get("outputSource");

            if (!int.TryParse(ConfigurationManager.AppSettings.Get("timeInterval"), out TimeInterval))
                TimeInterval = INTERVAL_DEFAULT;

            RequiredTags = new List<String>()
            {
                   "jobid",
                   "jobtitle",
                   "employer",
                   "period",    
                   "description"
            };

            sqlServerConnectionString = ConfigurationManager.ConnectionStrings["SqlServerConnectionString"].ConnectionString;
        }
    }
}
