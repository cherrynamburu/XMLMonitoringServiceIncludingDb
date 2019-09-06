using System;
using System.Timers;

namespace XMLMonitoringService
{
    static class Scheduler
    {
        private static Timer _timer;

        public static void Start()
        {
            _timer = new Timer(1)
            {
                AutoReset = false,
                Interval = 1
            };
            _timer.Elapsed += _timer_Elapsed;         
            _timer.Enabled = true;
        }

        private static void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            XMLParser parser = new XMLParser();

            Console.Write("Parsing XML files Started......");
            Console.WriteLine(DateTime.Now.ToString());

            if (SqlServer.GetConnection() != null)
            {
                parser.XmlDataParser();
                Console.WriteLine("Press enter to exit...!");
            }
            else
            {
                parser.XmlDataParserToLog();
                Console.WriteLine("Press enter to exit...!");
            }
            _timer.Interval = Config.TimeInterval;
        }

        //public static void PrintTimes()
        //{
        //    // Print all the recorded times from the timer.
        //    if (_results.Count > 0)
        //    {
        //        Console.WriteLine("TIMES:");
        //        foreach (var time in _results)
        //        {
        //            Console.Write(time);
        //        }
        //        Console.WriteLine();
        //    }
        //}
        // Add DateTime for each timer event.
        //File.AppendAllText(outputSource, "\n\n" + DateTime.Now + "\n");


        //while (true)
        //{
        //    Console.WriteLine("Running...");
        //    System.Threading.Thread.Sleep(2000);
        //}
    }
}


